using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Endpoint : MonoBehaviour
{
    // Configurable sprites
    [SerializeField] private Sprite defaultSprite = null;
    [SerializeField] Sprite connectionSprite = null;

    // Assiociated gameObjects
    private GameObject parentNode = null;
    private GameObject otherEndpoint = null;
    private GameObject snapNode = null;

    // Variables assiociated with mouse moving/dragging
    private Vector3 mOffset;
    private float mZCord;
    private bool mouseDown = false;

    // Configurable values (argubaly could be set to serializeField)
    float deletetionRadius = 1f;
    float nodeRadius = 0.5f;
    private Vector2 idlePosition = new Vector2(1f, 0f);

    // State
    bool disconnected = false;

    void Start()
    {
        setEndpointSprite();
    }

    public void setParentNode(GameObject node)
    {
        parentNode = node;
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

    void Update()
    {
        updateAngle();
        if (mouseDown == true)
        {
            updateSnapNode();
            trackMousePosition();
        }
        else
        {
            deleteDisconnected();
            trackParentNodePosition();
        }
    }

    void trackMousePosition()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    void trackParentNodePosition()
    {
        if (parentNode != null)
        {
            Vector3 direction = otherEndpoint.transform.position - parentNode.transform.position;
            transform.position = direction.normalized * nodeRadius + parentNode.transform.position;
        }
    }

    void updateSnapNode()
    {
        GameObject newSnapNode = Helper.FindClosestGameObject(
            origin:transform.position,
            tag:"Node",
            exclusionList:new List<GameObject>() { getOtherEndpoint().parentNode }
        );

        // Only snap node within the node range
        if (!Helper.CheckIfInRange(1f, gameObject, newSnapNode))
        {
            newSnapNode = null;
        }

        // Trigger script on snap node change
        if (newSnapNode != snapNode)
        {
            onSnapNodeChange(newSnapNode);
        }
    }

    void updateAngle()
    {
        int sign = Math.Sign(transform.position.y - otherEndpoint.transform.position.y);
        float angle = sign * Vector2.Angle(idlePosition, transform.position - otherEndpoint.transform.position);
        transform.eulerAngles = new Vector3(0f, 0f, angle + 180);
    }

    // Mouse Movement Related Section
    // #########################################
    void OnMouseDown()
    {
        SetClicked();
    }

    private void OnMouseUp()
    {
        SetUnclicked();
    }
    void onSnapNodeChange(GameObject newSnapNode)
    {
        snapNode = newSnapNode;
        if (snapNode == null)
        {
            setCursorDefault();
        }
        else
        {
            setCursorConnect();
        }
    }

    void setCursorConnect()
    {
        Cursor.visible = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = connectionSprite;
    }

    void setCursorDefault()
    {
        Cursor.visible = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }

    public void SetClicked()
    {
        // When changing the node remove edge after click
        if(parentNode != null)
        {
            removeEdge();
        }
        disconnected = false;

        // Mouse tracking script
        mouseDown = true;
        mZCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void SetUnclicked()
    {
        setCursorDefault();
        mouseDown = false;
        if (snapNode == null)
        {
            disconnected = true;
        }
        else
        {
            parentNode = snapNode;
            snapNode = null;
            createEdge();
        }     
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void createEdge()
    {
        Node ownNode = getParentNodeScript().getNode();
        Node otherNode = getOtherEndpoint().getParentNodeScript().getNode();

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


        if (Node.doesEdgeExist(ownNode, otherNode))
        {
            ownNode.removeConnection(otherNode);
            otherNode.removeConnection(ownNode);
        }
    }

    public void setEndpointSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }

    private void deleteDisconnected()
    {
        if (disconnected)
        {
            Vector3 delta = (otherEndpoint.transform.position - transform.position);
            if (delta.magnitude > deletetionRadius)
            {
                transform.position += Time.deltaTime * delta.normalized * 10;
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}
