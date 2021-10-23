using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DjikstraTest : MonoBehaviour
{
    int intenalClock = 0;
    bool runAlgorithmFlag = false;
    Djikstra djikstra;
    StateTransition clock;

    // Start is called before the first frame update
    void Start()
    {
        clock = GameObject.FindGameObjectWithTag("Clock").GetComponent<StateTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clock.state != intenalClock)
        {
            intenalClock = clock.state;
            if (runAlgorithmFlag)
            {
                runAlgorithmFlag = !djikstra.algorithmOuter();
            }

        }
    }

    public void initDjikstra()
    {
        djikstra = new Djikstra();
        djikstra.setUp();
        djikstra.initAlgorithm();
        runAlgorithmFlag = true;
    }


}
