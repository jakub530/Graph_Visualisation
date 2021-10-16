using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Djikstra
{
    List<DjikstraNode> allNodes = new List<DjikstraNode>();
    StateTransition clock;

    public void setUp()
    {
        List<Node> nodes = AlgorithmUtility.getAllNodes();
        foreach(Node node in nodes)
        {
            allNodes.Add(new DjikstraNode(node));
        }
        clock = GameObject.FindGameObjectWithTag("Clock").GetComponent<StateTransition>();
    }

    public void Update()
    {
        Debug.Log("Updating");
    }

    public IEnumerator fullAlgorithm()
    {
        DjikstraNode startNode = allNodes.Where(_ => _.node.id == 0).First();
        DjikstraNode endNode = allNodes.Where(_ => _.node.id == 10).First();
        List<DjikstraNode> Q = allNodes;
        startNode.distance = 0;
        int step = 0;
        while(Q.Count > 0)
        {
            yield return new WaitForSeconds(1f);
            step++;
            Q = Q.OrderBy(_ => _.distance).ToList();

            DjikstraNode u = Q.First();
            Debug.Log("Step: " + step.ToString());
            foreach(var node in Q)
            {
                //Debug.Log("Queue Item " + node.node.id);
            }

            if (u == endNode)
            {
                break;
            }
            Q.Remove(u);
            foreach(var x in u.node.findDestinationNodes())
            {
                DjikstraNode v = allNodes.Where(_ => _.node.id == x.id).First();
                double alt = u.distance + u.node.findConnectionByOtherNode(v.node).cost;
                if(alt < v.distance)
                {
                    v.distance = alt;
                    v.prevNode = u;
                }
            }
        }
        Debug.Log("The cost is " + endNode.distance.ToString());
    }



    
}

public class DjikstraNode
{
    public Node node = null;
    public double distance =  Mathf.Infinity;
    public DjikstraNode prevNode = null;

    public DjikstraNode(Node _node)
    {
        node = _node;
    }
}

