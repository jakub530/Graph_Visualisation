using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgorithmUtility 
{
    static public List<Node> getAllNodes()
    {
        GameObject[] AllObjects = GameObject.FindGameObjectsWithTag("Node");
        List<Node> allNodes = new List<Node>();
        foreach(var item in AllObjects)
        {
            NodeVis nodeVisualisation = item.GetComponent<NodeVis>();
            allNodes.Add(nodeVisualisation.attachedNode);
        }
        return allNodes;
    }

    static public void changeAllNodeColor(Color color)
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (var node in nodes)
        {
            NodeVis vis = node.GetComponent<NodeVis>();
            vis.setColor(color);
        }
    }
}
