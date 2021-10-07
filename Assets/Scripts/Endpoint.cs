using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endpoint : MonoBehaviour
{
    [SerializeField] public Sprite endpointSprite = null;
    [SerializeField] private GameObject parentNode = null;

    private GameObject otherEndpoint = null;


    [SerializeField] private Sprite defaultSprite = null;
    [SerializeField] Sprite connectionSprite = null;

    // Variables assiociated with mouse moving/dragging
    private Vector3 mOffset;
    private float mZCord;
    private bool mouseDown = false;

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

        if (mouseDown == true)
        {
            GameObject closestNode = findClosestNode();
            transform.position = GetMouseWorldPos() + mOffset;
            if (closestNode != null)
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
            if(parentNode != null)
            {
                Vector3 direction = otherEndpoint.transform.position - parentNode.transform.position;
                transform.position = direction.normalized + parentNode.transform.position;
            }
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
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked Endpoint");
        SetClicked();

    }

    private void OnMouseUp()
    {
        SetUnclicked();
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
                Debug.Log(getOtherEndpoint().getParentNode());
                Debug.Log(closest);
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
