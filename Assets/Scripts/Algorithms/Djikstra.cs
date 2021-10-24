using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Djikstra : Algorithm
{
    // Necessary variables for algorithm
    List<DjikstraNode> allNodes;
    List<DjikstraNode> Q;
    DjikstraNode startNode;
    DjikstraNode endNode;
    DjikstraNode backTrackNode;
    Phase phase;


    public override void algorithmPreInitialization()
    {
        // Need to select start and end node
        nodesToSelect = 2;
        configureGroupVis();
        phase = Phase.search;
        allNodes = AlgorithmUtility.getAllNodes().Select(_ => new DjikstraNode(_)).ToList();
    }

    private void configureGroupVis()
    {
        visualGroups = new Dictionary<string, GroupVis>()
        {
            { "Visited Nodes", new GroupVis(Color.cyan, new List<Node>(), "Visited Nodes") },
            { "Start Node",    new GroupVis(Color.green, new List<Node>() , "Start Node") },
            { "End Node",      new GroupVis(Color.yellow, new List<Node>() , "End Node") },
            { "Active Node",   new GroupVis(Color.red, new List<Node>() , "Active Node") },
            { "Path Nodes",   new GroupVis(Color.blue, new List<Node>() , "Path Nodes") },
        };
    }

    public override void processNodeClick(int index, GameObject node)
    {
        int nodeId = node.GetComponent<NodeVis>().attachedNode.id;
        switch (index)
        {
            case 0:
                startNode = DjikstraNode.findNodeById(allNodes, nodeId);
                getGroup(Groups.start).startNewList(startNode.getNode());
                break;
            case 1:
                endNode =   DjikstraNode.findNodeById(allNodes, nodeId);
                getGroup(Groups.end).startNewList(endNode.getNode());
                break;
        }
        updateColors();
    }

    private enum Groups
    {
        visited,
        active,
        start,
        end,
        path
    }

    private enum Phase
    {
        search,
        backtrack,
        finished
    }

    private GroupVis getGroup(Groups group)
    {
        switch (group)
        {
            case Groups.visited:
                return visualGroups["Visited Nodes"];
            case Groups.active:
                return visualGroups["Active Node"];
            case Groups.start:
                return visualGroups["Start Node"];
            case Groups.end:
                return visualGroups["End Node"];
            case Groups.path:
                return visualGroups["Path Nodes"];
            default:
                return null;
        }
    }

    public override void algorithmInitialization()
    {
        Q = new List<DjikstraNode>() { startNode };
        startNode.distance = 0;
        updateColors();
    }


    public override State runStep()
    {
        Debug.Log("running Step");
        if(phase == Phase.search)
        {
            (DjikstraNode node, bool reachedGoal) = searchAlgorithm();
            getGroup(Groups.visited).addNode(getGroup(Groups.active).getFirstNode());
            getGroup(Groups.active).startNewList(node.getNode());
            if(reachedGoal)
            {
                phase = Phase.backtrack;
                backTrackNode = node;
            }
        }
        else if (phase == Phase.backtrack)
        {
            backTrackAlgorithm();
        }





        updateColors();
        updateQueue(ConvertList(Q));
        return phase == Phase.finished ? State.inactive : State.active;
    }

    public void backTrackAlgorithm()
    {
        getGroup(Groups.path).addNode(backTrackNode.getNode());
        if(backTrackNode != startNode)
        {
            backTrackNode = backTrackNode.prevNode;
        }
        else
        {
            phase = Phase.finished;
        }
    }

    public (DjikstraNode, bool) searchAlgorithm()
    {
        DjikstraNode u;
        bool endFlag = false;

        if (Q.Count > 0)
        {
            Q = Q.OrderBy(_ => _.distance).ToList();
            u = Q.First();
            //Debug.Log(u.node);
            if (u == endNode)
            {
                endFlag = true;
            }
            Q.Remove(u);
            foreach(var x in u.node.findDestinationNodes())
            {
                DjikstraNode v = allNodes.Where(_ => _.node.id == x.id).First();
                double alt = u.distance + u.node.findConnectionByOtherNode(v.node).cost;
                if(alt < v.distance)
                {
                    Q.Add(v);
                    v.distance = alt;
                    v.prevNode = u;
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

    private List<AlgorithmNode> ConvertList(List<DjikstraNode> nodes)
    {
        return nodes.Select(_ => (AlgorithmNode)_).ToList();
    }


}



public class DjikstraNode : AlgorithmNode
{
    public double distance =  Mathf.Infinity;
    public DjikstraNode prevNode = null;

    public DjikstraNode(Node _node)
    {
        node = _node;
    }

   

    public override (string nodeName, List<string> propLabels, List<string> propValues) getProps()
    {
        string nodeName = getNodeName();
        List<string> propLabels = new List<string>() { "Distance", "Previous Node"};
        List<string> propValues = new List<string>() { distance.ToString(), prevNode.getNodeName()};

        return (nodeName: nodeName, propLabels: propLabels, propValues: propValues);
    }

    public static DjikstraNode findNodeById(List<DjikstraNode> nodes, int id)
    {
        return nodes.Where(_ => _.node.id == id).First(); 
    }
}

