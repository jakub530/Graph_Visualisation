using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LegendObject : MonoBehaviour
{
    [SerializeField] GameObject descriptionObject;
    [SerializeField] GameObject outerCircleObject;
    [SerializeField] GameObject innerCircleObject;

    TextMeshProUGUI descriptionField;
    Image innerCircle;
    Image outerCircle;

    Color color;
    string description;

    void Start()
    {
        
    }

    public void setProperties(Color _color, string _description)
    {
        initObjects();
        color = _color;
        description = _description;

        setColor();
        setDescription();

    }

    void initObjects()
    {
        descriptionField = descriptionObject.GetComponent<TextMeshProUGUI>();
        innerCircle = innerCircleObject.GetComponent<Image>();
        outerCircle = outerCircleObject.GetComponent<Image>();
    }

    public void setColor()
    {
        float darkMultiplier = 0.7f;
        Color darkerColor = new Color(color.r * darkMultiplier, color.g * darkMultiplier, color.b * darkMultiplier);

        innerCircle.color = color;
        outerCircle.color = darkerColor;
    }


    private void setDescription()
    {
        descriptionField.text = description;
    }
}
