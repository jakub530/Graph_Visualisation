using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectLine : MonoBehaviour
{
    [SerializeField] GameObject endpointPrefab;
    [SerializeField] GameObject edgeCost;
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

    void createCostLabel()
    {
        Vector3 position = (endpoints[0].transform.position + endpoints[1].transform.position) / 2;
        edgeCost.transform.position = position;
    }


    void createEndpoints(GameObject node, Role role)
    {
        initEndpoints();
        //Debug.Log(role.ToString());
        endpointScript(endpoints[0]).initEndpoint(node:node,  otherEndpoint:endpoints[1],  _role:role, setClickedFlag:false);
        
        endpointScript(endpoints[1]).initEndpoint(node: null, otherEndpoint: endpoints[0], _role:RoleHelper.OtherRole(role), setClickedFlag: true);
        //Debug.Log(RoleHelper.OtherRole(role).ToString());
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

    public void scriptableEdgeCreation(NodeVis source, NodeVis dest, bool bidirect)
    {
        Role sourceRole = bidirect ? Role.bidirect : Role.source;

        initEndpoints();
        endpointScript(endpoints[0]).scriptableInitEndpoint(node: source.gameObject, otherEndpoint: endpoints[1], _role: sourceRole);
        endpointScript(endpoints[1]).scriptableInitEndpoint(node: dest.gameObject, otherEndpoint: endpoints[0], _role: RoleHelper.OtherRole(sourceRole));

        endpointScript(endpoints[0]).createEdge();
        updateSpriteSize();
    }

    void initEndpoints()
    {
        for (int i = 0; i < 2; i++)
        {
            endpoints[i] = Instantiate(endpointPrefab, transform.position, transform.rotation);
            endpoints[i].transform.SetParent(this.transform);
        }
    }

    public void createClickDown(GameObject nodeVis, Role role)
    {
        createEndpoints(nodeVis, role);
        updateSpriteSize();
    }

    public void createClickUp()
    {
        endpointScript(endpoints[1]).SetUnclicked();
    }
}
