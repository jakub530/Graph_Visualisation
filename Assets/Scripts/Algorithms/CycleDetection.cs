using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CycleDetection : Algorithm
{
    // Necessary variables for algorithm
    Dictionary<int, CycleNode> allNodes;
    List<CycleNode> Q;
    List<CycleNode> cycle;
    Phase phase;

    public override void algorithmPreInitialization()
    {
        // Need to select start and end node
        startNodeGroups = new List<(string visGroupName, string nodeName)>();

        configureVisualGroups(
            new List<(Color color, string name, string fullName)>
            {
                (Color.blue,   "queued", "Queued Nodes"),
                (Color.cyan,   "visited","Visited Nodes"),
                (Color.red,    "active", "Active Nodes"),
                (Color.yellow, "added",  "Added Node"),
                (Color.green,  "cycle",  "Cycle Nodes"),
            }
        );
        phase = Phase.search;
        allNodes = AlgorithmUtility.getAllNodes().ToDictionary(_ => _.id, _ => new CycleNode(_));
    }

    public override void algorithmInitialization()
    {
        Q = new List<CycleNode>() {  };
        updateColors();
    }

    public override State runStep()
    {
        Debug.Log("running Step");

        if(phase == Phase.search)
        {
            (CycleNode node, bool reachedGoal, bool switchActive) = searchAlgorithm();
            if (!reachedGoal)
            {
                if (switchActive)
                {
                    visGroups["visited"].addNode(visGroups["active"].getFirstNode());

                    visGroups["added"].startNewList(null);
                    if (node != null)
                    {
                        visGroups["active"].startNewList(node.getNode());
                    }
                }
            }
            else
            {
                phase = Phase.finished;
            }
        }
        else if(phase == Phase.cycle)
        {
            visualizeCycle();
        }

        updateColors();
        updateQueue(ConvertList(Q));
        return phase == Phase.finished ? State.inactive : State.active;
    }

    public void initializeCycle(CycleNode n1, CycleNode n2)
    {
        CycleNode commonNode = null;
        List<CycleNode> n1List = createList(n1);
        List<CycleNode> n2List = createList(n2);
        foreach(CycleNode node in n1List)
        {
            if(n2List.Contains(node))
            {
                commonNode = node;
                n1List = n1List.TakeWhile(_ => _ != node).ToList();
                break;
            }
        }

        n2List = n2List.TakeWhile(_ => _ != commonNode).Reverse().ToList();
        cycle = n1List.Append(commonNode).Concat(n2List).ToList();
        phase = Phase.cycle;

        
    }

    public void visualizeCycle()
    {
        CycleNode node = cycle.First();
        cycle.Remove(node);
        visGroups["cycle"].addNode(node.getNode());
        if (cycle.Count == 0)
        {
            phase = Phase.finished;
        }
    }



    public List<CycleNode> createList(CycleNode node)
    {
        List<CycleNode> nodes = new List<CycleNode>();
        while(node != null)
        {
            nodes.Add(node);
            node = node.prevNode;
        }
        return nodes;
    }

    public (CycleNode, bool, bool) searchAlgorithm()
    {
        CycleNode u;
        bool endFlag = false;
        bool switchActive = false;

        if (Q.Count > 0)
        {
            u = Q.First();
            u.visited = true;
            CycleNode v = u.node.findDestinationNodes().Select(_ => allNodes[_.id]).Where(_ => _.visited == false && _.queued == false).FirstOrDefault();

            CycleNode cycle = u.node.findDestinationNodes().Select(_ => allNodes[_.id]).Where(_ => _.visited == true && u.prevNode != _).FirstOrDefault();

            if (cycle != null)
            {
                initializeCycle(cycle, u);
                return (null, false, false);
            }
            
            if (v != null)
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
            CycleNode nextNode = allNodes.Values.Where(_ => _.visited == false).FirstOrDefault();
            if(nextNode != null)
            {
                Q.Add(nextNode);
                switchActive = true;
            }
            else
            {
                u = null;
                endFlag = true;
            }
        }
        return (Q.FirstOrDefault(), endFlag, switchActive);
    }

    public void subStep(CycleNode v)
    {
        Q.Add(v);
        v.queued = true;
        v.prevNode = Q.First();
        visGroups["queued"].addNode(v.getNode());
        visGroups["added"].startNewList(v.getNode());
    }

    private List<AlgorithmNode> ConvertList(List<CycleNode> nodes)
    {
        return nodes.Select(_ => (AlgorithmNode)_).ToList();
    }

    private CycleNode findNodeByName(string name)
    {
        return allNodes[namedNodes[name]];
    }

    private enum Phase
    {
        search,
        cycle,
        finished
    }
}



public class CycleNode : AlgorithmNode
{
    public bool visited;
    public float distance;
    public CycleNode prevNode;
    public bool queued;

    public CycleNode(Node _node)
    {
        node = _node;
        visited = false;
        queued = false;
        distance = Mathf.Infinity;
    }



    public override (string nodeName, List<string> propLabels, List<string> propValues) getProps()
    {
        string nodeName = getNodeName();
        List<string> propLabels = new List<string>() { "Visited" };
        List<string> propValues = new List<string>() { visited.ToString() };

        return (nodeName: nodeName, propLabels: propLabels, propValues: propValues);
    }

    public static CycleNode findNodeById(List<CycleNode> nodes, int id)
    {
        return nodes.Where(_ => _.node.id == id).First();
    }
}

