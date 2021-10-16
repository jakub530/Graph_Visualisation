using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupVis 
{
    List<Node> nodes = new List<Node>();
    Color32 color;
    string name;

    void addNode(Node newNode)
    {
        nodes.Add(newNode);
    }

    void removeNode(Node node)
    {
        nodes.Remove(node);
    }

    GroupVis(Color32 _color, List<Node> initNodes, string _name)
    {
        color = color;
        name = _name;
        nodes = initNodes;
    }

    void updateColors()
    {
        foreach(var node in nodes)
        {
            GameObject NodeObject = node.vis;
            NodeVis nodeScript = NodeObject.GetComponent<NodeVis>();
            nodeScript.switchColor(color);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
