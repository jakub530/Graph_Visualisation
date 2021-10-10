using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VisExperiments : MonoBehaviour
{
    static List<Node> nodeList = new List<Node>();
    static bool triggerCascade = false;
    static float elapsed = 0f;
    static Color switchColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            Debug.Log("Trdggered elapsed");
            elapsed = elapsed % 1f;
            if (triggerCascade)
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
            futureNodes = futureNodes.Concat(node.findNeighbours()).ToList();
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

    

    static public void ClickedNode(GameObject nodeObject)
    {
        nodeList = new List<Node>();
        switchColor = Random.ColorHSV();
        NodeVis nodeScript = nodeObject.GetComponent<NodeVis>();
        nodeScript.switchColor(switchColor);
        Node node = nodeScript.attachedNode;
        nodeList.Add(node);
        triggerCascade = true;
        elapsed = 0f;
        Debug.Log("Triggered Cascade");

        
    }
}
