using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge 
{
    public Node source;
    public Node dest;
    public double cost;
    public bool bidirect;

    public Edge(Node _srcNode, Node _destNode, double _cost = 1, bool _bidirect = true)
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
            return bidirect ? (dest, Role.bidirect) : (dest, Role.source);
        }
        else if(dest == node)
        {
            return bidirect ? (source, Role.bidirect) : (source, Role.destination);
        }
        else
        {
            Debug.Log(source.id);
            Debug.Log(dest.id);
            Debug.LogError("Too many nodes found in edge");
            return (null, Role.invalid);
        }
    }

    public static Edge createEdge(Node _srcNode, Node _destNode, double _cost = 1, bool _bidirect = true)
    {
        Edge newEdge = new Edge(_srcNode, _destNode, _cost, _bidirect);
        _srcNode.addConnection(newEdge);
        _destNode.addConnection(newEdge);
        return newEdge;
    }

    public static void deleteEdge(Node node1, Node node2)
    {
        Edge edge = node1.findConnectionByOtherNode(node2);
        node1.removeConnection(edge);
        node2.removeConnection(edge);
    }

    ~Edge()
    {
        Debug.Log("Destroying edge");
    }

    public override string ToString()
    {
        return base.ToString() + " src:" + source + " dest: " + dest + " bidirect: " + bidirect;
    }
}

public enum Role 
{
    source,
    destination,
    bidirect,
    any,
    invalid,
}

public static class RoleHelper
{
    public static Role OtherRole(Role role)
    {
        switch(role)
        {
            case Role.source:
                return Role.destination;
            case Role.destination:
                return Role.source;
            default:
                return Role.bidirect;
        }
    }
}
