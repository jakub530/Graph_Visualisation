using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BFS : Algorithm
{
    // Necessary variables for algorithm
    List<BFSNode> allNodes;
    List<BFSNode> Q;
    BFSNode startNode;
    Phase phase;


    public override void algorithmPreInitialization()
    {
        // Need to select start and end node
        nodesToSelect = 1;
        configureGroupVis();
        phase = Phase.search;
        allNodes = AlgorithmUtility.getAllNodes().Select(_ => new BFSNode(_)).ToList();
    }

    private void configureGroupVis()
    {
        visualGroups = new Dictionary<string, GroupVis>()
        {
            { "Queued Nodes",   new GroupVis(Color.blue, new List<Node>() , "Queued Nodes") },
            { "Visited Nodes", new GroupVis(Color.cyan, new List<Node>(), "Visited Nodes") },
            { "Active Node",   new GroupVis(Color.red, new List<Node>() , "Active Node") },
        };
    }

    public override void processNodeClick(int index, GameObject node)
    {
        int nodeId = node.GetComponent<NodeVis>().attachedNode.id;
        switch (index)
        {
            case 0:
                startNode = BFSNode.findNodeById(allNodes, nodeId);
                getGroup(Groups.active).startNewList(startNode.getNode());
                break;
        }
        updateColors();
    }

    private enum Groups
    {
        visited,
        active,
        queued,
    }

    private enum Phase
    {
        search,
        finished
    }

    private GroupVis getGroup(Groups group)
    {
        switch (group)
        {
            case Groups.visited:
                return visualGroups["Visited Nodes"];
            case Groups.queued:
                return visualGroups["Queued Nodes"];
            case Groups.active:
                return visualGroups["Active Node"];
            default:
                return null;
        }
    }

    public override void algorithmInitialization()
    {
        Q = new List<BFSNode>() { startNode };
        startNode.visited = true;
        updateColors();
    }


    public override State runStep()
    {
        Debug.Log("running Step");

        (BFSNode node, bool reachedGoal) = searchAlgorithm();
        if(!reachedGoal)
        {
            getGroup(Groups.visited).addNode(getGroup(Groups.active).getFirstNode());
            getGroup(Groups.active).startNewList(node.getNode());
        }
            else
        {
            phase = Phase.finished;
        }


        updateColors();
        updateQueue(ConvertList(Q));
        return phase == Phase.finished ? State.inactive : State.active;
    }

    public (BFSNode, bool) searchAlgorithm()
    {
        BFSNode u;
        bool endFlag = false;

        if (Q.Count > 0)
        {
            u = Q.First();
            Q.Remove(u);

            foreach (var x in u.node.findDestinationNodes())
            {
                BFSNode v = allNodes.Where(_ => _.node.id == x.id).First();
                if (v.visited == false)
                {
                    Q.Add(v);
                    getGroup(Groups.queued).addNode(v.getNode());
                    v.visited = true;
                }
            }
        }
        else
        {
            u = null;
            endFlag = true;
        }
        return (u, endFlag);
    }

    public void subStep()
    {

    }

    private List<AlgorithmNode> ConvertList(List<BFSNode> nodes)
    {
        return nodes.Select(_ => (AlgorithmNode)_).ToList();
    }
}



public class BFSNode : AlgorithmNode
{
    public bool visited;

    public BFSNode(Node _node)
    {
        node = _node;
        visited = false;
    }



    public override (string nodeName, List<string> propLabels, List<string> propValues) getProps()
    {
        string nodeName = getNodeName();
        List<string> propLabels = new List<string>() { "Visited" };
        List<string> propValues = new List<string>() { visited.ToString()};

        return (nodeName: nodeName, propLabels: propLabels, propValues: propValues);
    }

    public static BFSNode findNodeById(List<BFSNode> nodes, int id)
    {
        return nodes.Where(_ => _.node.id == id).First();
    }
}

