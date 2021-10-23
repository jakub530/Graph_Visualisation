using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    int intenalClock = 0;
    List<GameObject> selectedNodes;
    public int nodesToSelect = 0;
    State state = State.inactive;

    


    StateTransition clock;
    AlgorithmControl algorithmControl;
    NodeClickEvent nodeClickEvent;

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
            if(selectedNodes.Count < nodesToSelect)
            {
                selectedNodes.Add(node);

                if(selectedNodes.Count == nodesToSelect)
                {
                    algorithmInitialization(selectedNodes);
                    state = State.active;
                }
            }
        }
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

    public virtual void algorithmInitialization(List<GameObject> selectedNodes)
    {
        Debug.Log("You should run actual method");
    }

    public virtual void algorithmPreInitialization()
    {
        Debug.Log("You should run actual method");
    }



    public void algorithmClick()
    {
        selectedNodes = new List<GameObject>();
        algorithmPreInitialization();
        algorithmControl.setActiveAlgorithm(this);

        if(nodesToSelect == 0)
        {
            algorithmInitialization(selectedNodes);
            state = State.active;
        }
        else
        {
            state = State.pending;
        }
    }

}

public enum State
{
    inactive,
    active,
    pending
}
