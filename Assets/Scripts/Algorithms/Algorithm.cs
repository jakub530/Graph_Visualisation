using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    int intenalClock = 0;
    int selectedNodes = 0;
    State state = State.inactive;

    StateTransition clock;
    AlgorithmControl algorithmControl;
    NodeClickEvent nodeClickEvent;

    public Dictionary<string, GroupVis> visGroups;
    public Dictionary<string, int> namedNodes;
    public List<(string visGroupName, string nodeName)> startNodeGroups;

    public void Start()
    {
        clock = GameObject.FindGameObjectWithTag("Clock").GetComponent<StateTransition>();
        algorithmControl = GameObject.FindGameObjectWithTag("AlgorithmControl").GetComponent<AlgorithmControl>();
        
        nodeClickEvent = algorithmControl.nodeClickEvent;
        nodeClickEvent.AddListener(processClick);
    }

    public void Update()
    {

        runAlgorithm();
    }

    private void processClick(GameObject node)
    {
        if(state == State.pending)
        {
            if(selectedNodes < startNodeGroups.Count)
            {
                processNodeClick(selectedNodes, node);
                selectedNodes++;

                if(selectedNodes == startNodeGroups.Count)
                {
                    algorithmInitialization();
                    state = State.active;
                }
            }
        }
    }

    public void configureVisualGroups(List<(Color color, string name, string fullName)> configuration)
    {
        visGroups = new Dictionary<string, GroupVis>();
        foreach (var config in configuration)
        {
            visGroups.Add(config.name, new GroupVis(config.color, new List<Node>(), config.fullName));
        }
    }

    public void processNodeClick(int index, GameObject gameObjectNode)
    {
        Node node = gameObjectNode.GetComponent<NodeVis>().attachedNode;
        var configRow = startNodeGroups[index];
        namedNodes.Add(configRow.nodeName, node.id);
        visGroups[configRow.visGroupName].addNode(node);
        updateColors();
    }

    public virtual void addToGroup(int nodeID, int index)
    {

    }

    public void runAlgorithm()
    {
        if (clock.state != intenalClock)
        {
            intenalClock = clock.state;
            if (state == State.active)
            {
                state = runStep();
            }

        }
    }

    public virtual State runStep()
    {
        Debug.Log("You should run actual method");
        return State.inactive;
    }

    public virtual void algorithmInitialization()
    {
        Debug.Log("You should run actual method");
    }

    public virtual void algorithmPreInitialization()
    {
        Debug.Log("You should run actual method");
    }



    public void algorithmClick()
    {
        LegendController.getLegend().clearLegends();
        namedNodes = new Dictionary<string, int>();
        clearColor();
        selectedNodes = 0;
        algorithmPreInitialization();
        algorithmControl.setActiveAlgorithm(this);

        if(startNodeGroups.Count == 0)
        {
            algorithmInitialization();
            state = State.active;
        }
        else
        {
            state = State.pending;
        }
    }

    public void updateQueue(List<AlgorithmNode> Q)
    {
        GameObject queueContent = GameObject.FindGameObjectWithTag("QueueContent");
        QueueGeneration queueGeneration = queueContent.GetComponent<QueueGeneration>();
        queueGeneration.renderQueue(Q);
    }

    public void updateColors()
    {
        foreach(GroupVis group in visGroups.Values)
        {
            group.updateColors();
        }
    }

    public void clearColor()
    {
        AlgorithmUtility.changeAllNodeColor(Color.grey);
    }
}

public enum State
{
    inactive,
    active,
    pending
}
