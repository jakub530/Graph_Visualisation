using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroupVis 
{
    public List<Node> nodes = new List<Node>();
    public Color32 color;
    public string name;

    public void addNode(Node newNode)
    {
        if (newNode != null)
        {
            nodes.Add(newNode);
        }

    }

    public void removeNode(Node node)
    {
        nodes.Remove(node);
    }

    public void startNewList(Node node)
    {
        if (node != null)
        {
            nodes = new List<Node>() { node };
        }
        else
        {
            nodes = new List<Node>();
        }
    }

    public Node getFirstNode()
    {
        if(nodes.Count > 0)
        {
            return nodes.First();
        }       
        else
        {
            return null;
        }
    }

    public GroupVis(Color32 _color, List<Node> initNodes, string _name)
    {
        color = _color;
        name = _name;
        nodes = initNodes;
        LegendController.getLegend().createLegend(name, color);
    }

    public void updateColors()
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
