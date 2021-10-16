using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VisExperiments : MonoBehaviour
{
    List<Node> nodeList = new List<Node>();
    bool triggerCascade = false;
    Color switchColor = Color.white;
    [SerializeField] StateTransition clock;
    int clockState = 0;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if(clock.state != clockState)
        {
            clockState = clock.state;
            if(triggerCascade)
            {
                cascadeColors();
            }

        } 
    }

    public void ChangeNodeColor()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach(var node in nodes)
        {
            NodeVis vis = node.GetComponent<NodeVis>();
            vis.setColor(Color.blue);
            //vis.setColor(new Color(0.7f, 0.3f, 0.2f));
        }
    }

    public void cascadeColors()
    {
        Debug.Log("Darkenning");
        List<Node> futureNodes = new List<Node>();
        foreach (Node node in nodeList)
        {
            futureNodes = futureNodes.Concat(node.findDestinationNodes()).ToList();
        }
        foreach (Node node in futureNodes)
        {
            Debug.Log(node);
            foreach (Edge edge in node.connections)
            {
                Debug.Log(edge);
            }

        }
        futureNodes = futureNodes.Distinct().ToList();
        futureNodes = futureNodes.Except(nodeList).ToList();
        List<Node> nodesToRemove = new List<Node>();
        foreach(Node node in futureNodes)
        {
            NodeVis nodeScript = node.vis.GetComponent<NodeVis>();
            if(!nodeScript.switchColor(switchColor))
            {
                nodesToRemove.Add(node);
            }
        }
        futureNodes = futureNodes.Except(nodesToRemove).ToList();


        if (futureNodes.Count == 0)
        {
            triggerCascade = false;
        }
        else
        {
            nodeList = futureNodes;
        }
        
    }

    

    public void ClickedNode(GameObject nodeObject)
    {
        nodeList = new List<Node>();
        switchColor = Random.ColorHSV();
        NodeVis nodeScript = nodeObject.GetComponent<NodeVis>();
        nodeScript.switchColor(switchColor);
        Node node = nodeScript.attachedNode;
        nodeList.Add(node);
        triggerCascade = true;
        clock.restartClock();
        Debug.Log("Triggered Cascade");

        
    }
}
