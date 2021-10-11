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
}
