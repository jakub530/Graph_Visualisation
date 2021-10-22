using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendController : MonoBehaviour
{
    [SerializeField] float descriptionLength = 220;
    
    public List<GameObject> legendList = new List<GameObject>();
    [SerializeField] public GameObject legedItemPrefab;

    float lowHeight = 40;
    float highHeight = 110;

    float initPosX = -920;


    void Start()
    {
        //createLegend("First Legend", Color.red);
        //createLegend("Second Legend", Color.blue);
        //createLegend("3 Legend", Color.cyan);
        //createLegend("4 Legend", Color.magenta);
        //createLegend("5 Legend", Color.gray);
        //createLegend("6 Legend", Color.green);
        //createLegend("7 Legend", Color.black);
        //createLegend("8 Legend", Color.white);
        //clearLegends();
    }


    public void createLegend(string description, Color color)
    {
        GameObject legendItem = Instantiate(legedItemPrefab, Vector3.zero, transform.rotation);
        LegendObject legend = legendItem.GetComponent<LegendObject>();
        legendList.Add(legendItem);
        legend.setProperties(color, description);

        legendItem.transform.SetParent(gameObject.transform);
        legendItem.transform.localPosition = new Vector3(getPosX(), getPosY(), 0);
    }

    public void clearLegends()
    {
        for(int i=0;i<legendList.Count;i++)
        {
            Destroy(legendList[i]);
        }

        legendList = new List<GameObject>();
    }

    float getPosY()
    {
        return legendList.Count % 2 == 0 ? 40 : 110; 
    }

    float getPosX()
    {
        return initPosX + descriptionLength * ((legendList.Count -  1) / 2);
    }
}
