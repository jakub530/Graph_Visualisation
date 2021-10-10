using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endpoint : MonoBehaviour
{
    [SerializeField] public Sprite endpointSprite = null;
    [SerializeField] private GameObject parentNode = null;

    private GameObject otherEndpoint = null;

    private GameObject snapNode = null;
    [SerializeField] private Sprite defaultSprite = null;
    [SerializeField] Sprite connectionSprite = null;

    // Variables assiociated with mouse moving/dragging
    private Vector3 mOffset;
    private float mZCord;
    private bool mouseDown = false;

    bool disconnected = false;
    float deletetionRadius = 1f;
    float nodeRadius = 0.5f;
    float angle = 0;

    private Vector2 idlePosition = new Vector2(1f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        setEndpointSprite();
    }

    public void setParentNode(GameObject node)
    {
        parentNode = node;
    }

    public GameObject getParentNode()
    {
        return parentNode;
    }

    public NodeVis getParentNodeScript()
    {
        return parentNode.GetComponent<NodeVis>();
    }

    public void setOtherEndpoint(GameObject endpoint)
    {
        otherEndpoint = endpoint;
    }

    public Endpoint getOtherEndpoint()
    {
        return otherEndpoint.GetComponent<Endpoint>(); ;
    }



    // Update is called once per frame
    void Update()
    {

        updateAngle();
        if (mouseDown == true)
        {
            GameObject newSnapNode = findClosestNode();
            if (newSnapNode != snapNode)
            {
                onClosestNodeChange(newSnapNode);
            }
            transform.position = GetMouseWorldPos() + mOffset;

        }
        else
        {
            defaultCursor();
            deleteDisconnected();
            if (parentNode != null)
            {
                Vector3 direction = otherEndpoint.transform.position - parentNode.transform.position;
                transform.position = direction.normalized*nodeRadius + parentNode.transform.position;
            }
        }
    }

    void updateAngle()
    {
        if(transform.position.y > otherEndpoint.transform.position.y)
        {
            angle = Vector2.Angle(idlePosition, transform.position - otherEndpoint.transform.position);
        }
        else
        {
            angle = -Vector2.Angle(idlePosition, transform.position - otherEndpoint.transform.position);
        }



        transform.eulerAngles = new Vector3(0f, 0f , angle+180);
    }

    void onClosestNodeChange(GameObject newSnapNode)
    {
        snapNode = newSnapNode;
        if (snapNode != null)
        {
            //Debug.Log("Changing cursor");
            connectionCursor();
        }
        else
        {
            //Debug.Log("Removing cursor");
            defaultCursor();
        }
    }


    void connectionCursor()
    {
        Cursor.visible = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = connectionSprite;
    }

    void defaultCursor()
    {
        Cursor.visible = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = endpointSprite;
    }

    void OnMouseDown()
    {
        
        SetClicked();

    }

    private void OnMouseUp()
    {
        SetUnclicked();

    }


    public void SetClicked()
    {
        if(parentNode != null)
        {
            //Debug.Log("Removing Edge");
            removeEdge();
        }
        disconnected = false;
        mouseDown = true;
        mZCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void SetUnclicked()
    {
        mouseDown = false;
        parentNode = snapNode;
        snapNode = null;
        if (parentNode == null)
        {
            disconnected = true;
        }
        else
        {
            createEdge();
        }
            
       
    }

    public void createEdge()
    {
        Node ownNode = getParentNodeScript().getNode();
        Node otherNode = getOtherEndpoint().getParentNodeScript().getNode();
        //Debug.Log("Connecting Node" + ownNode.id);
        //Debug.Log("Connecting Node" + otherNode.id);

        if (!Node.doesEdgeExist(ownNode, otherNode))
        {
            ownNode.addConnection(otherNode);
            otherNode.addConnection(ownNode);
        }
        else
        {
            disconnected = true;
            parentNode = null;
        }
    }

    public void removeEdge()
    {
        Node ownNode = getParentNodeScript().getNode();
        Node otherNode = getOtherEndpoint().getParentNodeScript().getNode();
        //Debug.Log("Disconnecting Node" + ownNode.id);
        //Debug.Log("Disconnecting Node" + otherNode.id);


        if (Node.doesEdgeExist(ownNode, otherNode))
        {
            //Debug.Log("Edge exists");
            ownNode.removeConnection(otherNode);
            otherNode.removeConnection(ownNode);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void setEndpointSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = endpointSprite;
    }

    private void deleteDisconnected()
    {
        if (disconnected)
        {
            Vector3 delta = (otherEndpoint.transform.position - transform.position);
            if (delta.magnitude > deletetionRadius)
            {
                transform.position += Time.deltaTime * delta.normalized * 10;
                //redrawLine();
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    private GameObject findClosestNode()
    {
        GameObject[] allNodes;
        allNodes = GameObject.FindGameObjectsWithTag("Node");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject node in allNodes)
        {
            Vector3 diff = node.transform.position - position;
            float curDistance = diff.magnitude;
            if (curDistance < distance && node != getOtherEndpoint().getParentNode())
            {
                //Debug.Log(getOtherEndpoint().getParentNode());
                //Debug.Log(closest);
                closest = node;
                distance = curDistance;
            }
        }
    
        if(distance < 1f)
        {
        return closest;
        }
        else
        {
            return null;
        }
    }
}
