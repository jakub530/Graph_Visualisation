using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BFS : Algorithm
{
    // Necessary variables for algorithm
    Dictionary<int,BFSNode> allNodes;
    List<BFSNode> Q;
    Phase phase;

    public override void algorithmPreInitialization()
    {
        // Need to select start and end node
        startNodeGroups = new List<(string visGroupName, string nodeName)>
        {
            ("active", "startNode"),
        };

        configureVisualGroups(
            new List<(Color color, string name, string fullName)>
            {
                (Color.blue,   "queued", "Queued Nodes"),
                (Color.cyan,   "visited","Visited Nodes"),
                (Color.red,    "active", "Active Nodes"),
                (Color.yellow, "added",  "Added Node"),
            }
        );
        phase = Phase.search;
        allNodes = AlgorithmUtility.getAllNodes().ToDictionary(_ => _.id ,_ => new BFSNode(_));
    }

    public override void algorithmInitialization()
    {
        Q = new List<BFSNode>() { findNodeByName("startNode") };
        findNodeByName("startNode").visited = true;
        updateColors();
    }

    public override State runStep()
    {
        Debug.Log("running Step");

        (BFSNode node, bool reachedGoal, bool switchActive) = searchAlgorithm();
        if(!reachedGoal)
        {
            if(switchActive)
            {
                visGroups["visited"].addNode(visGroups["active"].getFirstNode());

                visGroups["added"].startNewList(null);
                if(node != null)
                {
                    visGroups["active"].startNewList(node.getNode());
                }
            }
        }
        else
        {
            phase = Phase.finished;
        }

        updateColors();
        updateQueue(ConvertList(Q));
        return phase == Phase.finished ? State.inactive : State.active;
    }

    public (BFSNode, bool, bool) searchAlgorithm()
    {
        BFSNode u;
        bool endFlag = false;
        bool switchActive = false;

        if (Q.Count > 0)
        {
            u = Q.First();
            BFSNode v = u.node.findDestinationNodes().Select(_ => allNodes[_.id]).Where(_=>_.visited == false).FirstOrDefault();
            if(v != null)
            {
                subStep(v);
            }
            else
            {
                Q.Remove(u);
                switchActive = true;
            }
        }
        else
        {
            u = null;
            endFlag = true;
        }
        return (Q.FirstOrDefault(), endFlag, switchActive);
    }

    public void subStep(BFSNode v)
    {
        Q.Add(v);
        visGroups["queued"].addNode(v.getNode());
        visGroups["added"].startNewList(v.getNode());
        v.visited = true;
    }

    private List<AlgorithmNode> ConvertList(List<BFSNode> nodes)
    {
        return nodes.Select(_ => (AlgorithmNode)_).ToList();
    }

    private BFSNode findNodeByName(string name)
    {
        return allNodes[namedNodes[name]];
    }

    private enum Phase
    {
        search,
        finished
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

