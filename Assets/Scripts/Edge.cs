using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge 
{
    public Node originNode;
    public Node otherNode;
    int cost;
    bool activeDir;

    public Edge(Node _originNode, Node _otherNode, int _cost, bool _activeDir)
    {
        originNode = _originNode;
        otherNode  = _otherNode;
        cost = _cost;
        activeDir = _activeDir;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
