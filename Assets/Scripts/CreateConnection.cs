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
        connectLine.setParentNode(transform.parent.gameObject);
        connectLine.SetClicked();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnMouseUp()
    {
        connectLine.SetUnclicked();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //gameObject.SetActive(true);
    }
}
