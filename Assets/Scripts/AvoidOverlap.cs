using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidOverlap : MonoBehaviour
{
    private float radius = 2f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GameObject closest = findClosestNode();
        if(closest != null)
        {
            moveAway(closest);
        }

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
            if(go != gameObject)
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
        Debug.Log(closest);
        if (distance < radius)
        {
            Debug.Log("Test");
            return closest;
        }
        else
        {
            return null;
        }
    }
}
