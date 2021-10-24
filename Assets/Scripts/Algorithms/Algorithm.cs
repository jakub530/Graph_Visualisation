using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    int intenalClock = 0;
    int selectedNodes = 0;
    public int nodesToSelect = 0;
    State state = State.inactive;

    StateTransition clock;
    AlgorithmControl algorithmControl;
    NodeClickEvent nodeClickEvent;

    public Dictionary<string, GroupVis> visualGroups;

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
            if(selectedNodes < nodesToSelect)
            {
                processNodeClick(selectedNodes, node);
                selectedNodes++;

                if(selectedNodes == nodesToSelect)
                {
                    algorithmInitialization();
                    state = State.active;
                }
            }
        }
    }

    public virtual void processNodeClick(int index, GameObject node)
    {

    }

    public void runAlgorithm()
    {
        Debug.Log("Running Algorithm");
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
        clearColor();
        selectedNodes = 0;
        algorithmPreInitialization();
        algorithmControl.setActiveAlgorithm(this);

        if(nodesToSelect == 0)
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
        foreach(GroupVis group in  visualGroups.Values)
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
