using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNodes : MonoBehaviour
{
    [SerializeField] GameObject NodePrefab;
    [SerializeField] GameObject ConnectionPrefab;
    ConnectLine connectionScript = null;
    List<NodeVis> createdNodes = new List<NodeVis>();
    // Start is called before the first frame update
    void Start()
    {
        initNodes();
        createEdges(testEdges(80));
    }

    public  List<List<bool>> testEdges(int numberNodes)
    {
        List<List<bool>> output = new List<List<bool>>();
        for(int i=0; i <numberNodes; i++)
        {
            List<bool> row = new List<bool>();
            for(int j=0;j< numberNodes; j++)
            {
                if(j == 2*i || i == 2*j)
                {
                    row.Add(true);
                }
                else
                {
                    row.Add(false);
                }
            }
            output.Add(row);
        }
        return output;
    }

    void createEdges(List<List<bool>> edgeMatrix)
    {
        for(int row=0; row < edgeMatrix.Count; row++)
        {
            for(int col=0;col<row; col++)
            {
                if (edgeMatrix[row][col] && edgeMatrix[col][row])
                {
                    createEdge(createdNodes[row], createdNodes[col], true);
                }
                else if(edgeMatrix[row][col])
                {
                    createEdge(createdNodes[row], createdNodes[col], false);
                }
                else if(edgeMatrix[col][row])
                {
                    createEdge(createdNodes[col] , createdNodes[row], false);
                }
            }
        }
    }

    void initNodes()
    {
        int rowLength = 8;
        float dx = 3.1f;
        float dy = -3.1f;
        int startX = -8;
        int startY = 4;
        int colIndex = 0;
        Vector2 pos = new Vector2(startX, startY);
        
        for (int i = 0; i < 80; i++)
        {
            GameObject newNode = Instantiate(NodePrefab, pos, transform.rotation);
            NodeVis newNodeScript = newNode.GetComponent<NodeVis>();
            newNodeScript.attachNode();
            createdNodes.Add(newNodeScript);
            colIndex++;
            if(colIndex == rowLength)
            {
                pos.x = startX;
                pos.y += dy;
                colIndex = 0;
            }
            else
            {
                pos.x += dx;
            }
        }

    }

    void createEdge(NodeVis source, NodeVis destination, bool bidirect)
    {
        GameObject newConnection = Instantiate(ConnectionPrefab, transform.position, transform.rotation);
        connectionScript = newConnection.GetComponent<ConnectLine>();
        GameObject Edges = GameObject.FindGameObjectWithTag("Edges");
        newConnection.transform.SetParent(Edges.transform);

        connectionScript.scriptableEdgeCreation(source, destination, bidirect);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
