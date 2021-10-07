using UnityEngine;
using System.Collections;


public class SquareDraggable : MonoBehaviour
{

    [SerializeField] private Vector3 mOffset;
    private float mZCord;

    void OnMouseDown()
    {
        mZCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();


    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3  mousePoint = Input.mousePosition;
        mousePoint.z = mZCord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;

    }

    void OnMouseUp()
    {
        //transform.position = Vector3.zero;

    }


}