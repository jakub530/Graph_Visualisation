using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgorithmNode
{
    public Node node = null;

    public string getNodeName()
    {
        return node.id.ToString();
    }

    public virtual (string nodeName, List<string> propLabels, List<string> propValues) getProps()
    {
        Debug.LogWarning("Should be overwritten");
        return ("", new List<string>(), new List<string>());
    }

    public Node getNode()
    {
        return node;
    }
}