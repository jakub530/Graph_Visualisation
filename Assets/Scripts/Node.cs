using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node 
{
    List<Edge> connections = new List<Edge>();
    public int id;
    public GameObject vis;
    static int lastID = -1;


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
        List<Edge> edges = connections.Where(
        _ => _.dest == destination || 
        (_.source == destination && _.bidirect == true))
        .ToList();

        if (edges.Count == 0)
        {
            return null;
        }
        else if (edges.Count == 1)
        {
            return edges.First();
        }
        else
        {
            Debug.LogError("Too many connections");
            return null;
        }
    }

    // Find sources/bidirect nodes
    public List<Node> findSourceNodes()
    {
        return findConnectedNodes(new List<Role>() { Role.source, Role.bidirect });
    }


    // Find destination/bidirect nodes
    public List<Node> findDestinationNodes()
    {
        return findConnectedNodes(new List<Role>() { Role.destination, Role.bidirect });
    }
    
    // Find all connected nodes
    public List<Node> findAllNodes()
    {
        return findConnectedNodes(new List<Role>() { Role.destination, Role.bidirect, Role.source });
    }

    // Find connected nodes
    private List<Node> findConnectedNodes(List<Role> roles)
    {
        return connections.Select(_ => _.findOtherNode(this))
                          .Where(_ => roles.Contains(_.role))
                          .Select(_ => _.outputNode).ToList();
    }

    public Node(string msg,  GameObject _vis)
    {
        id = getNextID();
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

    static int getNextID()
    {
        lastID++;
        return lastID;
    }
}
