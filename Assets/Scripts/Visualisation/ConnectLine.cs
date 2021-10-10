using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectLine : MonoBehaviour
{
    [SerializeField] GameObject endpointPrefab;
    private List<GameObject> endpoints = new List<GameObject>() { null, null };
    private LineRenderer line;
    private List<float> spriteSize;

    private Endpoint endpointScript(GameObject endpointObject)
    {
        return endpointObject.GetComponent<Endpoint>();
    }

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        drawLine();
    }


    void createEndpoints(GameObject node)
    {
        for(int i=0; i<2; i++)
        {
            endpoints[i] = Instantiate(endpointPrefab, transform.position, transform.rotation);
            endpoints[i].transform.SetParent(this.transform);
        }

        endpointScript(endpoints[1]).setOtherEndpoint(endpoints[0]);
        endpointScript(endpoints[0]).setOtherEndpoint(endpoints[1]);
        endpointScript(endpoints[1]).SetClicked();
        endpointScript(endpoints[0]).setParentNode(node);
    }

    void updateSpriteSize()
    {
        spriteSize = new List<float>();
        for (int i = 0; i < 2; i++)
        {
            float spriteWidth = endpoints[i].GetComponent<SpriteRenderer>().sprite.rect.width;
            float spritePixelPerUnit = endpoints[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
            spriteSize.Add(spriteWidth / spritePixelPerUnit); 
        }
    }

    void drawLine()
    {
        Vector3 direction = (endpoints[1].transform.position - endpoints[0].transform.position).normalized;
        Vector3[] positionArray = new[] { endpoints[0].transform.position + direction * spriteSize[0], endpoints[1].transform.position - direction * spriteSize[1] };
        line.SetPositions(positionArray);
    }

    public void createClickDown(GameObject nodeVis)
    {
        createEndpoints(nodeVis);
        updateSpriteSize();
    }

    public void createClickUp()
    {
        endpointScript(endpoints[1]).SetUnclicked();
    }
}
