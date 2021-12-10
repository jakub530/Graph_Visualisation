using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    GameObject runModeObject;
    GameObject editModeObject;
    Mode mode;
    public Mode initMode = Mode.editMode;
    private Dictionary<string,Flag> dictionaryNameTranslation;
    private Dictionary<Flag, bool> flagStateDictionary;

    public ModeSwitchEvent modeSwitchEvent;

    // Start is called before the first frame update
    void Awake()
    {
        if (modeSwitchEvent == null)
        {
            modeSwitchEvent = new ModeSwitchEvent();
        }
        initModes();
        setUpFlagTranslations();
        setUpFlagState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initModes()
    {
        runModeObject = GameObject.FindGameObjectWithTag("runMode");
        editModeObject = GameObject.FindGameObjectWithTag("editMode");
        setMode(initMode);
    }

    void setMode(Mode newMode)
    {
        Debug.Log("Setting Mode");
        mode = newMode;
        if (mode == Mode.runMode)
        {
            flagStateDictionary[Flag.addNodeFlag] = false;
            changeButtonState("Add Node", false);
            runModeObject.SetActive(true);
            editModeObject.SetActive(false);
            AlgorithmUtility.changeAllNodeColor(Color.gray);
        }
        else
        {
            AlgorithmUtility.changeAllNodeColor(Color.gray);
            runModeObject.SetActive(false);
            editModeObject.SetActive(true);

        }
        modeSwitchEvent.Invoke(newMode);
    }

    public void switchMode()
    {
        if(mode==Mode.runMode)
        {
            setMode(Mode.editMode);
        }
        else
        {
            setMode(Mode.runMode);
        }
    }

    public static UIControl get()
    {
        return GameObject.FindGameObjectWithTag("uiControl").GetComponent<UIControl>();
    }

    public void onClick(string flagName)
    {
        //Debug.Log(flagName);
        Flag flag = dictionaryNameTranslation[flagName];

        flagStateDictionary[flag] = !flagStateDictionary[flag];
        if (flagName == "Add Node")
        {
            changeButtonState(flagName, flagStateDictionary[flag]);
        }
    }

    public void changeButtonState(string buttonName, bool newState)
    {
        GameObject button = GameObject.Find(buttonName);
        button.GetComponent<Image>().color = newState ? Color.red : Color.white;
    }

    private void setUpFlagTranslations()
    {
        dictionaryNameTranslation = new Dictionary<string, Flag>() {
            { "Add Node", Flag.addNodeFlag },
        };
    }

    public bool getFlag(Flag flag)
    {
        return flagStateDictionary[flag];
    }

    private void setUpFlagState()
    {
        flagStateDictionary = new Dictionary<Flag, bool>();
        foreach (Flag foo in System.Enum.GetValues(typeof(Flag)))
        {
            flagStateDictionary.Add(foo, false);
        }
    }


}

public enum Mode
{
    runMode,
    editMode
}

public enum Flag
{
    addNodeFlag,
}

public class ModeSwitchEvent : UnityEvent<Mode>
{

}


