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

    float initPosX = 20;


    void Start()
    {

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

    public static LegendController getLegend()
    {
        GameObject legendObject = GameObject.FindGameObjectWithTag("Legend");
        LegendController legend = legendObject.GetComponent<LegendController>();
        return legend;
    }
}
