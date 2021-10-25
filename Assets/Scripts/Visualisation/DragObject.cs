using UnityEngine;
using System.Collections;


public class DragObject : MonoBehaviour
{

    [SerializeField] private Vector3 screenPoint;
    [SerializeField]  private Vector3 offset;

    // Mode
    private ModeSwitchEvent modeSwitchEvent;
    private Mode mode;

    private void Start()
    {
        modeSwitchEvent = UIControl.get().modeSwitchEvent;
        modeSwitchEvent.AddListener(Switch);
        Switch(UIControl.get().initMode);
    }

    void Switch(Mode newMode)
    {
        mode = newMode;
    }

    void OnMouseDown()
    {
        if(mode == Mode.editMode)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
       
    }

    void OnMouseDrag()
    {
        if(mode == Mode.editMode)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }

    }
}