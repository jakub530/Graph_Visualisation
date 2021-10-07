using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateConnection : MonoBehaviour
{
    [SerializeField] GameObject Arrow;
    ConnectLine connectLine = null;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        GameObject newConnection = Instantiate(Arrow, transform.position, transform.rotation);
        connectLine = newConnection.GetComponent<ConnectLine>();
        //connectLine.initNode(transform.parent.gameObject);
        connectLine.initClick(transform.parent.gameObject);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnMouseUp()
    {
        connectLine.initUnclick();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //gameObject.SetActive(true);
    }
}
