using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectLine : MonoBehaviour
{
    private List<GameObject> endpoints = new List<GameObject>() { null, null };
    private LineRenderer lineRend;
    [SerializeField] private GameObject endpointPrefab = null;
    
    public int activeEndpoint = -1;


    private Endpoint findEnpoint(GameObject endpointObject)
    {
        Endpoint endpoint = endpointObject.GetComponent<Endpoint>();

        return endpoint;
    }

    void Start()
    {
        lineRend = gameObject.GetComponent<LineRenderer>();
    }

    void createEndpoints(GameObject node)
    {
        Debug.Log("Creating Endpoints");
        activeEndpoint = 1;
        for(int i=0; i<2; i++)
        {
            endpoints[i] = Instantiate(endpointPrefab, transform.position, transform.rotation);
            endpoints[i].transform.SetParent(this.transform);
        }
        findEnpoint(endpoints[0]).setParentNode(node);
    }

    void Update()
    {
       redrawLine();
    }



    void redrawLine()
    {
        lineRend.enabled = true;
        Vector3 direction = (endpoints[1].transform.position - endpoints[0].transform.position).normalized;
        float arrow0 = endpoints[0].GetComponent<SpriteRenderer>().sprite.rect.width / endpoints[0].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        float arrow1 = endpoints[1].GetComponent<SpriteRenderer>().sprite.rect.width / endpoints[1].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        //float testDim = endpoints[0].transform.localScale.x * endpoints[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        //Debug.Log(arrowLength);
        Vector3[] positionArray = new[] { endpoints[0].transform.position + direction * arrow0, endpoints[1].transform.position - direction * arrow1 };
        lineRend.SetPositions(positionArray);
    }

    public void initClick(GameObject node)
    {
        createEndpoints(node);
        findEnpoint(endpoints[1]).SetClicked();
        findEnpoint(endpoints[1]).setOtherEndpoint(endpoints[0]);
        findEnpoint(endpoints[0]).setOtherEndpoint(endpoints[1]);
    }

    public void initUnclick()
    {
        findEnpoint(endpoints[1]).SetUnclicked();

    }
}
