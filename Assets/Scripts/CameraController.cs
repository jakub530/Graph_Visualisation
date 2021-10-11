using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] Camera orthographicCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        orthographicCamera.orthographicSize += Input.mouseScrollDelta.y * 0.1f;
        //Debug.Log(Input.mouseScrollDelta);
        //gameObject.GetComponent<Camera>().orthographicSize =+ * 0.001f;
        //camera.orthographi
    }
    
}
