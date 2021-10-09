using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    List<Node> nodes = new List<Node>();

    Node(string msg)
    {
        Debug.Log("Your message was + " +  msg);
    }


}
