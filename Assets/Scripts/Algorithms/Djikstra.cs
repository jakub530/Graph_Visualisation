using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Djikstra
{
    List<DjikstraNode> allNodes = new List<DjikstraNode>();

    Djikstra()
    {
        List<Node> nodes = AlgorithmUtility.getAllNodes();
        foreach(Node node in nodes)
        {
            allNodes.Add(new DjikstraNode(node));
        }
    }



    
}

public class DjikstraNode
{
    Node node = null;
    double distance =  Mathf.Infinity;
    Node prevNode = null;

    public DjikstraNode(Node _node)
    {
        node = _node;
    }
}

