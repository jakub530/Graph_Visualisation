using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditCanvas : MonoBehaviour
{
    [SerializeField] GameObject NodePrefab;
    bool goodClick = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        addNodeControl();
    }

    void addNodeControl()
    {
        if (Input.GetMouseButtonDown(0) && UIControl.get().getFlag(Flag.addNodeFlag))
        {
            goodClick = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            goodClick = false;
        }

        if (Input.GetMouseButtonUp(0) && UIControl.get().getFlag(Flag.addNodeFlag) && goodClick)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            GameObject newNode = Instantiate(NodePrefab, curPosition, transform.rotation);
            NodeVis newNodeScript = newNode.GetComponent<NodeVis>();
            newNodeScript.attachNode();
            goodClick = false;
        }
    }


}
