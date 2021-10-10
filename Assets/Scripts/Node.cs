using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node 
{
    List<Edge> connections = new List<Edge>();
    public int id;
    public GameObject vis;


    public void addConnection(Node destination, int cost = 1, bool activeDir = true)
    {
        if (findConnectionByDestination(destination) == null)
        {
            connections.Add(
                new Edge(this, destination, cost, activeDir)
            );
        }
        else
        {
            Debug.LogError("Such connection already exists");
        }
    }

    public void removeConnection(Node destination)
    {
        Edge edge = findConnectionByDestination(destination);
        if(edge != null)
        {
            Debug.Log("Edge removed succesfully");
            connections.Remove(edge);
        }
        else
        {
            Debug.LogError("Couldn't find node to remove");
        }

    }

    public Edge findConnectionByDestination(Node destination)
    {
        List<Edge> matchingEdges = connections.Where(_ => _.otherNode == destination).ToList();
        if (matchingEdges.Count == 0)
        {
            return null;
        }
        else if (matchingEdges.Count == 1)
        {
            return matchingEdges.First();
        }
        else
        {
            Debug.LogError("Too many connections");
            return null;
        }
    }

    public List<Node> findNeighbours()
    {
        return connections.Select(_ => _.otherNode).ToList();
    }

    public Node(string msg, int _id, GameObject _vis)
    {
        id = _id;
        vis = _vis;
    }

    public static bool doesEdgeExist(Node node0, Node node1)
    {
        if(node0.findConnectionByDestination(node1) == null && node1.findConnectionByDestination(node0) ==null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
