using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransition : MonoBehaviour
{
    List<Node> queue = new List<Node>();
    [SerializeField] public float period = 0.1f;
    private float time = 0;
    public int state = 0;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        clock();
    }

    private void clock()
    {
        time += Time.deltaTime;
        if (time >= period)
        {
            time = time % period;
            //Debug.Log("Trigger Event");
            state = (state + 1) % 2;
        }
    }

    public void restartClock()
    {
        time = 0f;
    }

    public IEnumerator waitForClock()
    {
        yield return new WaitForSeconds(1);
    }
}
