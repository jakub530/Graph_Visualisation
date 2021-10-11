using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeVis : MonoBehaviour
{
    private float radius = 2f;
    public Node attachedNode = null;
    [SerializeField] private Text text;

    // Start is called before the first frame update
    void Start()
    {
        attachedNode = new Node(gameObject.name, gameObject);
        setText(attachedNode.id.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        GameObject closestNode = Helper.FindClosestGameObject(
            origin: transform.position,
            tag: "Node",
            exclusionList: new List<GameObject>() { gameObject }
        );

        // Move away if something is in radius
        if (Helper.CheckIfInRange(radius, gameObject, closestNode))
        {
            moveAway(closestNode);
        }
    }

    public void setText(string msg)
    {
        text.text = msg;
    }

    public void OnMouseDown()
    {
        GameObject visualization = GameObject.FindGameObjectWithTag("Visualization");
        VisExperiments visExp = visualization.GetComponent<VisExperiments>();
        visExp.ClickedNode(gameObject);
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
