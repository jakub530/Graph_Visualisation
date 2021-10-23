using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Djikstra : Algorithm
{
    List<DjikstraNode> allNodes = new List<DjikstraNode>();

    // Necessary variables for algorithm
    DjikstraNode startNode;
    DjikstraNode endNode;
    List<DjikstraNode> Q;
    bool endFlag = false;

    List<string> propNames = new List<string>() { "Distance", "Previous node" , "Test" };

 // Groups 
    GroupVis visitedNodses;
    GroupVis currentNode;




    public override void algorithmPreInitialization()
    {
        nodesToSelect = 2;
    }

    public override void algorithmInitialization(List<GameObject> selectedNodes)
    {
        int startNodeID = selectedNodes[0].GetComponent<NodeVis>().attachedNode.id;
        int endNodeID   = selectedNodes[1].GetComponent<NodeVis>().attachedNode.id;

        List<Node> nodes = AlgorithmUtility.getAllNodes();
        foreach (Node node in nodes)
        {
            allNodes.Add(new DjikstraNode(node));
        }

        startNode = allNodes.Where(_ => _.node.id == startNodeID).First();
        endNode = allNodes.Where(_ => _.node.id == endNodeID).First();
        Q = new List<DjikstraNode>() { startNode };
        startNode.distance = 0;
        visitedNodses = new GroupVis(Color.gray, new List<Node>(), "Visited Nodes");
        currentNode = new GroupVis(Color.red, new List<Node>() { startNode.node }, "Current Node");
        updateColors();
    }

    public void updateColors()
    {
        visitedNodses.updateColors();
        currentNode.updateColors();
    }

    public override State runStep()
    {
        Debug.Log("running Step");
        bool step;
        DjikstraNode node;
        (node, step) = algorithmStep();
        visitedNodses.addNode(currentNode.nodes.First());

        if(node!=null)
        {
            currentNode.nodes = new List<Node>() { node.node };
        }
        else
        {
            currentNode.nodes = new List<Node>();
        }

        updateColors();
        updateQueue();
        return step ? State.inactive : State.active;
    }

    public (DjikstraNode, bool) algorithmStep()
    {
        DjikstraNode u;
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

    public void updateQueue()
    {
        List<QueueItemContent> queueItems = new List<QueueItemContent>();

        foreach(DjikstraNode node in Q)
        {
            queueItems.Add(createQueueItem(node));
        }

        GameObject queueContent = GameObject.FindGameObjectWithTag("QueueContent");
        QueueGeneration queueGeneration = queueContent.GetComponent<QueueGeneration>();

        queueGeneration.renderQueue(queueItems);
    }

    public QueueItemContent createQueueItem(DjikstraNode node)
    {
        QueueItemContent queueItem = new QueueItemContent(node.getNodeName());
        List<string> propValues = new List<string>() { node.distance.ToString(), node.prevNode.getNodeName(), "prop" };

        queueItem.populateProps(propNames, propValues);
        return queueItem;
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

    public string getNodeName()
    {
        return node.id.ToString();
    }
}

