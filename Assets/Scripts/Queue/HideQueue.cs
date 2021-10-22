using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideQueue : MonoBehaviour
{
    [SerializeField] bool hidden = false;
    [SerializeField] float startingAngle = 0f;
    [SerializeField] float endAngle = 180f;
    [SerializeField] float distance = 400f;
    [SerializeField] int axis = 0;
    [SerializeField] int direction;
    float moved = 0;
    bool transition = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, startingAngle);
    }

    // Update is called once per frame
    void Update()
    {
       if(transition)
       {
           updatePosition();
       }
    }

    void updatePosition()
    {
        float deltaPos = hidden ? 1.0f : -1.0f;
        float angle = hidden ? endAngle : startingAngle;
        float moveDistance = deltaPos * Time.deltaTime * 1000f * direction;
        if(Mathf.Abs(moved) > distance)
        {
            transform.eulerAngles = new Vector3(0f, 0f, angle);
            transition = false;
            moved = 0;

        }
        else
        {
            moved += moveDistance;
            Vector3 moveVector = Vector3.zero;
            if (axis == 0)
            {
                moveVector.x += moveDistance;
            }
            else
            {
                moveVector.y += moveDistance;
            }

            transform.parent.position += moveVector;
            

        }

    }


    public void click()
    {
        if(!transition)
        {
            transition = true;
            hidden = !hidden;
        }
    }

}
