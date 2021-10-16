using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlEdgeCreation : MonoBehaviour
{
    [SerializeField] public Role newConnectionRole = Role.bidirect;
    [SerializeField] Button bidirectButton;
    [SerializeField] Button srcButton;
    [SerializeField] Button destButton;
    List<Button> buttonList;

    // Color scheme setting
    [SerializeField] Color32 defaultColor;
    [SerializeField] Color32 activeColor;

    Dictionary<string, (Button button, Role role)> roleAllocation = new Dictionary<string, (Button, Role)>();

    void Start()
    {
        roleAllocation.Add("source", (srcButton, Role.source));
        roleAllocation.Add("destination", (destButton, Role.destination));
        roleAllocation.Add("bidirect", (bidirectButton, Role.bidirect));
        buttonList = new List<Button>() { bidirectButton, srcButton, destButton };
        changeButtonColor(bidirectButton, activeColor);

    }

    void changeButtonColor(Button button, Color32 color)
    {
        var colors = button.colors;
        colors.normalColor = color;
        button.colors = colors;
    }

    void adjustAcitveButton(Button activeButton)
    {
        foreach(Button button in buttonList)
        {
            if(button == activeButton)
            {
                changeButtonColor(button, activeColor);
            }
            else
            {
                changeButtonColor(button, defaultColor);
            }
        }
    }


    public void processModeChange(string roleName)
    {
        adjustAcitveButton(roleAllocation[roleName].button);
        newConnectionRole = roleAllocation[roleName].role;
    }

    public void test()
    {

    }
}
