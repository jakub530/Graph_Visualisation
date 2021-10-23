using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] Camera orthographicCamera;

    private Vector3 screenPoint;
    private Vector3 offset;
    bool middleClicked = false;

    void mouseClick()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void mouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint); //+ offset;
        Vector3 posDiff = transform.position - curPosition;
        //Debug.Log(posDiff.magnitude);

        transform.position = - posDiff * 5f * Time.deltaTime + transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            //Debug.Log("Clicked middle mouse button");
            middleClicked = true;
            mouseClick();
        }
        if (Input.GetMouseButtonUp(2))
        {
            //Debug.Log("Unclicked middle mouse button");
            middleClicked = false;
        }
        if(middleClicked)
        {
            mouseDrag();
        }
    }

    void OnGUI()
    {
        float minCamera = 1f;
        float maxCamera = 16f;
        orthographicCamera.orthographicSize += Input.mouseScrollDelta.y * 0.1f;
        orthographicCamera.orthographicSize = Mathf.Clamp(orthographicCamera.orthographicSize, minCamera, maxCamera);

        // Mouse tracking script
        //mouseDown = true;
        //mZCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //mOffset = gameObject.transform.position - GetMouseWorldPos();
    }
    
}
