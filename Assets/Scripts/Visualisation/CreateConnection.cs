using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateConnection : MonoBehaviour
{
    [SerializeField] GameObject Connection;
    ConnectLine connectionScript = null;
    GameObject nodeVis;

    private void Start()
    {
        nodeVis = transform.parent.gameObject;
    }

    void OnMouseDown()
    {
        GameObject newConnection = Instantiate(Connection, transform.position, transform.rotation);
        connectionScript = newConnection.GetComponent<ConnectLine>();
        connectionScript.initClick(nodeVis);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnMouseUp()
    {
        connectionScript.initUnclick();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
