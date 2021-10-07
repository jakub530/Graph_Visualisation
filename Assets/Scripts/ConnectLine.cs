using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectLine : MonoBehaviour
{
    private List<GameObject> endpoints = new List<GameObject>() { null, null };
    private LineRenderer lineRend;
    [SerializeField] private GameObject endpointPrefab = null;
    
    public int activeEndpoint = -1;




    [SerializeField] float angle;
    
    [SerializeField] float radius = 0.5f;
    [SerializeField] float StickRadius = 0.5f;
    [SerializeField]  private GameObject connectedNode = null;



    private Vector2 idlePosition = new Vector2(1f, 0f);

    bool disconnected = false;

    // Variables assiociated with mouse moving/dragging
    private Vector3 mOffset;
    private float mZCord;
    private bool mouseDown = false;

    // Variable assiociated with cursour transformation
    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;
    private Sprite defaultSprite = null;
    [SerializeField] Sprite connectionSprite = null;


    private GameObject nearbyNode = null;


    //public void initNode(GameObject node)
    //{
    //    nodes[0] = node;
    //}
    //
    //private GameObject getOriginNode()
    //{
    //
    //    return nodes[0];
    //
    //}

    private Endpoint findEnpoint(GameObject endpointObject)
    {
        Endpoint endpoint = endpointObject.GetComponent<Endpoint>();

        return endpoint;
    }

    void Start()
    {
        
        lineRend = gameObject.GetComponent<LineRenderer>();
        defaultSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    void createEndpoints(GameObject node)
    {
        Debug.Log("Creating Endpoints");
        activeEndpoint = 1;
        for(int i=0; i<2; i++)
        {
            endpoints[i] = Instantiate(endpointPrefab, transform.position, transform.rotation);
            endpoints[i].transform.SetParent(this.transform);
        }
        findEnpoint(endpoints[0]).setParentNode(node);
    }

    void Update()
    {
        /*if (mouseDown == true)
        {
            transform.position = GetMouseWorldPos() + mOffset;
            nearbyNode = findClosestNode();

            if(nearbyNode != null)
            {
                Debug.Log("Changing cursor");
                connectionCursor();
            }
            else
            {
                defaultCursor();
            }    

        }
        else
        {
            defaultCursor();
        }

        if (mouseDown == true || connectedNode != null)
        {
            
            //redrawLine();
        }



        if(transform.position.y > getOriginNode().transform.position.y)
        {
            angle = Vector2.Angle(idlePosition, transform.position - getOriginNode().transform.position);
        }
        else
        {
            angle = -Vector2.Angle(idlePosition, transform.position - getOriginNode().transform.position);
        }

        if(connectedNode != null && mouseDown != true)
        {
            followConnection();
        }
        
        transform.eulerAngles = new Vector3(0f, 0f , angle);
        deleteDisconnected();*/
    }



    /*void redrawLine()
    {
        lineRend.enabled = true;
        Vector3[] positionArray = new[] { transform.position, getOriginNode().transform.position };
        lineRend.SetPositions(positionArray);
    }*/

    public void initClick(GameObject node)
    {
        createEndpoints(node);
        findEnpoint(endpoints[1]).SetClicked();
        findEnpoint(endpoints[1]).setOtherEndpoint(endpoints[0]);
        findEnpoint(endpoints[0]).setOtherEndpoint(endpoints[1]);
    }

    public void initUnclick()
    {
        findEnpoint(endpoints[1]).SetUnclicked();

    }



    /*private void followConnection()
    {
        transform.position = connectedNode.transform.position + (getOriginNode().transform.position - connectedNode.transform.position).normalized * (radius + 0.38f);
    }*/

/* private void deleteDisconnected()
 {
     if (disconnected)
     {
         Vector3 delta = (getOriginNode().transform.position - transform.position);
         if (delta.magnitude > radius)
         {
             transform.position += delta/30;
             //redrawLine();
         }
         else
         {
             disconnected = false;
             lineRend.enabled = false;
             Destroy(gameObject);
         }
     }
 }*/



    /////////////////////////////////////
    ///Used Code 
/*private Vector3 GetMouseWorldPos()
{
    Vector3 mousePoint = Input.mousePosition;
    mousePoint.z = mZCord;
    return Camera.main.ScreenToWorldPoint(mousePoint);
}*/

/*private GameObject findClosestNode()
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
        if (curDistance < distance)
        {
            closest = node;
            distance = curDistance;
        }
    }

    if(closest != getOriginNode() && distance < StickRadius)
    {
        return closest;
    }
    else
    {
        return null;
    }
}*/

/*void connectionCursor()
{
Cursor.visible = false;
gameObject.GetComponent<SpriteRenderer>().sprite = connectionSprite;
}

void defaultCursor()
{
Cursor.visible = true;
gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
}*/
}
