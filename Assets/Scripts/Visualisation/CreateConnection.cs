using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateConnection : MonoBehaviour
{
    [SerializeField] GameObject Connection;
    ConnectLine connectionScript = null;
    GameObject nodeVis;
    Role role = Role.bidirect;
    ControlEdgeCreation controller;

    private void Start()
    {
        nodeVis = transform.parent.gameObject;
        controller = GameObject.FindGameObjectWithTag("ControlEdgeCreation").GetComponent<ControlEdgeCreation>();
        
    }

    private void Update()
    {
        role = controller.newConnectionRole;
    }

    void OnMouseDown()
    {
        GameObject newConnection = Instantiate(Connection, transform.position, transform.rotation);
        connectionScript = newConnection.GetComponent<ConnectLine>();
        connectionScript.createClickDown(nodeVis, role);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnMouseUp()
    {
        connectionScript.createClickUp();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
