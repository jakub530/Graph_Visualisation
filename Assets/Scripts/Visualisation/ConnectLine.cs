using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectLine : MonoBehaviour
{
    [SerializeField] private GameObject endpointPrefab = null;
    private List<GameObject> endpoints = new List<GameObject>() { null, null };
    private LineRenderer lineRend;
    


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

        List<float> arrowLength = new List<float>();
        for(int i=0; i<2; i++)
        {
            arrowLength.Add(endpoints[0].GetComponent<SpriteRenderer>().sprite.rect.width / endpoints[0].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit);
        }

        Vector3[] positionArray = new[] { endpoints[0].transform.position + direction * arrowLength[0], endpoints[1].transform.position - direction * arrowLength[1] };
        lineRend.SetPositions(positionArray);
    }

    public void initClick(GameObject nodeVis)
    {
        createEndpoints(nodeVis);
        findEnpoint(endpoints[1]).SetClicked();
        findEnpoint(endpoints[1]).setOtherEndpoint(endpoints[0]);
        findEnpoint(endpoints[0]).setOtherEndpoint(endpoints[1]);
    }

    public void initUnclick()
    {
        findEnpoint(endpoints[1]).SetUnclicked();

    }
}
