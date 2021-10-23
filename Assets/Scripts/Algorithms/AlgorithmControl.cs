using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlgorithmControl : MonoBehaviour
{
    public NodeClickEvent nodeClickEvent;


    // Start is called before the first frame update
    void Start()
    {
        if (nodeClickEvent == null)
        {
            nodeClickEvent = new NodeClickEvent();
            nodeClickEvent.AddListener(Ping);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Ping(GameObject node)
    {
        Debug.Log("Alorithm Control");
        Debug.Log(node);
    }
}

public class Algorithm
{





}

public class NodeClickEvent : UnityEvent<GameObject>
{

}