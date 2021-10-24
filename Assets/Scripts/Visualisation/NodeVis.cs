using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class NodeVis : MonoBehaviour
{
    private float radius = 2f;
    public Node attachedNode = null;
    [SerializeField] private Text text;
    [SerializeField] int edgeCount;
    [SerializeField] GameObject connectionCreator;
    private NodeClickEvent nodeClickEvent;


    // Start is called before the first frame update
    void Start()
    {
        connectClickEvent();
    }

    void connectClickEvent()
    {
        GameObject algorithmControlObject = GameObject.FindGameObjectWithTag("AlgorithmControl");
        AlgorithmControl algorithmControl = algorithmControlObject.GetComponent<AlgorithmControl>();
        nodeClickEvent = algorithmControl.nodeClickEvent;
    }



    public void attachNode()
    {
        attachedNode = new Node(gameObject.name, gameObject);
        setText(attachedNode.id.ToString());
        GameObject Nodes = GameObject.FindGameObjectWithTag("Nodes");
        gameObject.transform.SetParent(Nodes.transform);
        gameObject.name = "Node:" + attachedNode.id.ToString();
        //connectionCreator.SetActive(false);
    }

    void Update()
    {
        /*GameObject closestNode = Helper.FindClosestGameObject(
            origin: transform.position,
            tag: "Node",
            exclusionList: new List<GameObject>() { gameObject }
        );*/

        // Move away if something is in radius
        /*if (Helper.CheckIfInRange(radius, gameObject, closestNode))
        {
            moveAway(closestNode);
        }*/
    }

    public void disableEdgeCreation()
    {
        connectionCreator.SetActive(false);
    }

    public void setText(string msg)
    {
        text.text = msg;
    }

    public void OnMouseDown()
    {
        nodeClickEvent.Invoke(gameObject);
    }



    // Get Attached Node
    public Node getNode()
    {
        return attachedNode;
    }

    public void setColor(Color color)
    {
        SpriteRenderer innerCircle = transform.GetChild(0).GetComponent<SpriteRenderer>();
        SpriteRenderer outerCircle = transform.GetComponent<SpriteRenderer>();

        float darkMultiplier = 0.7f;
        Color darkerColor = new Color(color.r * darkMultiplier, color.g * darkMultiplier, color.b * darkMultiplier);

        innerCircle.color = color;
        outerCircle.color = darkerColor;
    }

    public void darkenColor()
    {
        SpriteRenderer innerCircle = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Color color = innerCircle.color;
        setColor(new Color(color.r * 0.99f, color.g * 0.99f, color.b * 0.99f));
    }

    public bool switchColor(Color inputColor)
    {
        SpriteRenderer innerCircle = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (innerCircle.color != inputColor)
        {
            setColor(inputColor);
            return true;
        }
        else
        {

            return false;
        }
    }

    private void moveAway(GameObject otherNode)
    {
        transform.position += (transform.position - otherNode.transform.position).normalized * 0.01f;
    }
}
