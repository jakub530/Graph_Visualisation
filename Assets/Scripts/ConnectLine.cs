using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectLine : MonoBehaviour
{
    [SerializeField] private GameObject parentNode = null; 
    private LineRenderer lineRend;
    [SerializeField] float angle;
    bool rollDownB = false;
    [SerializeField] float radius = 0.5f;
    [SerializeField] float StickRadius = 0.5f;
    [SerializeField]  private GameObject connectedNode = null;
    private Vector2 idlePosition = new Vector2(1f, 0f);

    private Vector3 mOffset;
    private float mZCord;
    private bool mouseDown = false;


    public void setParentNode(GameObject node)
    {
        parentNode = node;
    }

    void Start()
    {
        lineRend = gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {

        if(mouseDown == true || connectedNode != null)
        {
            
            updateConnection();
        }

        if(mouseDown == true)
        {
            transform.position = GetMouseWorldPos() + mOffset;
        }

        if(transform.position.y > parentNode.transform.position.y)
        {
            angle = Vector2.Angle(idlePosition, transform.position - parentNode.transform.position);
        }
        else
        {
            angle = -Vector2.Angle(idlePosition, transform.position - parentNode.transform.position);
        }

        if(connectedNode != null && mouseDown != true)
        {
            followConnection();
        }
        
        transform.eulerAngles = new Vector3(0f, 0f , angle);
        rollDown();
    }

    void updateConnection()
    {
        lineRend.enabled = true;
        Vector3[] positionArray = new[] { transform.position, parentNode.transform.position };
        lineRend.SetPositions(positionArray);
    }

    public void SetClicked()
    {
        mouseDown = true;
        mZCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void SetUnclicked()
    {
        mouseDown = false;
        rollDownB = true;
        connectedNode = findClosestNode();
        if (connectedNode != null)
        {
            rollDownB = false;
        }
    }

    void OnMouseDown()
    {
        SetClicked();

    }
    void OnMouseDrag()
    {
        
    }

    private void OnMouseUp()
    {
        SetUnclicked();
    }

    private void followConnection()
    {
        transform.position = connectedNode.transform.position + (parentNode.transform.position - connectedNode.transform.position).normalized * (radius + 0.38f);
    }

    private void rollDown()
    {
        
        if (rollDownB)
        {
           
            Vector3 delta = (parentNode.transform.position - transform.position);
            if (delta.magnitude > radius)
            {
                transform.position += delta/30;
                updateConnection();
            }
            else
            {
                rollDownB = false;
                lineRend.enabled = false;
                Destroy(gameObject);

            }
        }
    }


    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private GameObject findClosestNode()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Node");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.magnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        if(closest != parentNode && distance < StickRadius)
        {
            return closest;
        }
        else
        {
            return null;
        }
    }
}
