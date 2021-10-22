using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupVis 
{
    public List<Node> nodes = new List<Node>();
    public Color32 color;
    public string name;

    public void addNode(Node newNode)
    {
        nodes.Add(newNode);
    }

    public void removeNode(Node node)
    {
        nodes.Remove(node);
    }

    public GroupVis(Color32 _color, List<Node> initNodes, string _name)
    {
        color = _color;
        name = _name;
        nodes = initNodes;
        GroupVis.getLegend().createLegend(name, color);
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

    static LegendController getLegend()
    {
        GameObject legendObject = GameObject.FindGameObjectWithTag("Legend");
        LegendController legend = legendObject.GetComponent<LegendController>();
        return legend;
    }
}
