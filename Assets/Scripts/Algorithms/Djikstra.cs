using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Djikstra : Algorithm
{
    // Necessary variables for algorithm
    Dictionary<int, DjikstraNode> allNodes;
    List<DjikstraNode> Q;
    DjikstraNode backTrackNode;
    Phase phase;


    public override void algorithmPreInitialization()
    {
        // Need to select start and end node
        startNodeGroups = new List<(string visGroupName, string nodeName)>
        {
            ("start", "startNode"),
            ("end",    "endNode"),
        };

        configureVisualGroups(
        new List<(Color color, string name, string fullName)>
            {
                        (Color.cyan,   "visited", "Visited Nodes"),
                        (Color.green,   "start","Start Node"),
                        (Color.yellow,  "end", "End Node"),
                        (Color.red,     "active",  "Active Node"),
                        (Color.blue,    "path",  "Path Nodes"),
            }
        );
        phase = Phase.search;
        allNodes = AlgorithmUtility.getAllNodes().ToDictionary(_ => _.id, _ => new DjikstraNode(_));
    }



    private enum Phase
    {
        search,
        backtrack,
        finished
    }


    public override void algorithmInitialization()
    {
        Q = new List<DjikstraNode>() { findNodeByName("startNode") };
        findNodeByName("startNode").distance = 0;
        updateColors();
    }


    public override State runStep()
    {
        Debug.Log("running Step");
        if(phase == Phase.search)
        {
            (DjikstraNode node, bool reachedGoal) = searchAlgorithm();
            if(node == null)
            {
                return State.inactive;
            }
            visGroups["visited"].addNode(visGroups["active"].getFirstNode());
            visGroups["active"].startNewList(node.getNode());
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
        visGroups["path"].addNode(backTrackNode.getNode());
        if(backTrackNode != findNodeByName("startNode"))
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
            if (u == findNodeByName("endNode"))
            {
                endFlag = true;
            }
            Q.Remove(u);
            foreach(var x in u.node.findDestinationNodes())
            {
                DjikstraNode v = allNodes[x.id];
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

    private DjikstraNode findNodeByName(string name)
    {
        return allNodes[namedNodes[name]];
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

