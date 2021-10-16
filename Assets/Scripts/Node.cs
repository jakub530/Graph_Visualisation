using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node 
{
    public List<Edge> connections = new List<Edge>();
    public int id;
    public GameObject vis;
    static int lastID = -1;


    public void addConnection(Edge newEdge)
    {
        connections.Add(newEdge);
    }

    public void removeConnection(Edge deletedEdge)
    {
        Debug.Log("Edge removed succesfully");
        connections.Remove(deletedEdge);
    }

    public Role findNodeRole(Edge edge)
    {
        if(edge.bidirect)
        {
            return Role.bidirect;
        }
        else
        {
            if(this==edge.dest)
            {
                return Role.destination;
            }
            else 
            {
                return Role.source;
            }
        }    
    }

    public Edge findConnectionByOtherNode(Node otherNode)
    {
        List<Edge> edges = connections.Where(_=>_.dest == otherNode || _.source == otherNode).ToList();
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
        return findConnectedNodes(new List<Role>() { Role.destination, Role.bidirect });
    }


    // Find destination/bidirect nodes
    public List<Node> findDestinationNodes()
    {
        return findConnectedNodes(new List<Role>() { Role.source, Role.bidirect });
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
        if(node0.findConnectionByOtherNode(node1) == null && node1.findConnectionByOtherNode(node0) ==null)
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

    public override string ToString()
    {
        return base.ToString() + ": " + id.ToString();
    }
}
