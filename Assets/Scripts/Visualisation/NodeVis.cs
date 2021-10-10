using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeVis : MonoBehaviour
{
    private float radius = 2f;
    private Node attachedNode = null;
    // Start is called before the first frame update
    void Start()
    {
        attachedNode = new Node(gameObject.name, System.Int32.Parse(gameObject.name));
    }

    // Update is called once per frame
    void Update()
    {
        GameObject closest = findClosestNode();
        if (closest != null)
        {
            moveAway(closest);
        }
    }

    // Get Attached Node
    public Node getNode()
    {
        return attachedNode;
    }




    private void moveAway(GameObject otherNode)
    {
        transform.position += (transform.position - otherNode.transform.position).normalized * 0.01f;
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
            if (go != gameObject)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.magnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        if (distance < radius)
        {
            return closest;
        }
        else
        {
            return null;
        }
    }
}
