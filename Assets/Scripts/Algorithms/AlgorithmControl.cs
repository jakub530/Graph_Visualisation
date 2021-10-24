using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlgorithmControl : MonoBehaviour
{
    public NodeClickEvent nodeClickEvent;
    public Algorithm activeAlgorithm;


    // Start is called before the first frame update
    void Awake()
    {
        if (nodeClickEvent == null)
        {
            nodeClickEvent = new NodeClickEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setActiveAlgorithm(Algorithm algorithm)
    {
        activeAlgorithm = algorithm;
    }
}



public class NodeClickEvent : UnityEvent<GameObject>
{

}