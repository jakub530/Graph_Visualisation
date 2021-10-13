using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge 
{
    public Node source;
    public Node dest;
    public int cost;
    public bool bidirect;

    public Edge(Node _srcNode, Node _destNode, int _cost = 1, bool _bidirect = true)
    {
        source = _srcNode;
        dest = _destNode;
        cost = _cost;
        bidirect = _bidirect;
    }

    // Return other node in given connection as well as role of the node in parameter (source/destination/bidirect)
    public (Node outputNode, Role role) findOtherNode(Node node)
    {
        if (source != node && dest != node)
        {
            Debug.LogError("No node found in edge");
            return (outputNode: null, role:Role.invalid);
        }
        else if (source == node)
        {
            if(bidirect)
            {
                return (dest, Role.bidirect);
            }
            else
            {
                return (dest, Role.source);
            }

        }
        else if(source == dest)
        {
            if (bidirect)
            {
                return (source, Role.bidirect);
            }
            else
            {
                return (source, Role.destination);
            }
        }
        else
        {
            Debug.LogError("Too many nodes found in edge");
            return (null, Role.invalid);
        }
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

public enum Role 
{
    source,
    destination,
    bidirect,
    invalid
}
