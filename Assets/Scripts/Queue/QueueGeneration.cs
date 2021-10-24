using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class QueueGeneration : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject propPrefab;
    List<GameObject> queueItems = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<QueueItemContent> convertNodeToQueueItems(List<AlgorithmNode> nodes)
    {
        List<QueueItemContent> itemList = new List<QueueItemContent>();
        foreach(AlgorithmNode node in nodes)
        {
            QueueItemContent item = new QueueItemContent();
            (string name, List<string> labels, List<string> values) = node.getProps();
            item.populateProps(name, labels, values);
            itemList.Add(item);
        }
        return itemList;
    }

    public void renderQueue(List<AlgorithmNode> nodes)
    {
        List<QueueItemContent> itemList = convertNodeToQueueItems(nodes);
        cleanUp();
        int initHeight = -100;
        int tmpPos = initHeight;
        int heightDelta = 155;

        int numItems = Mathf.Min(5, itemList.Count);
         
        for(int i=0; i < numItems; i++)
        {
            GameObject queueItem = Instantiate(itemPrefab, Vector3.zero, transform.rotation);
            queueItem.transform.SetParent(gameObject.transform);
            queueItem.transform.localPosition = new Vector3(0, tmpPos, 0);

            TextMeshProUGUI text = queueItem.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "#" + (i+1).ToString() + " " + "Node: " + itemList[i].nodeName;
            
            tmpPos -= heightDelta;
            renderProps(itemList[i], queueItem);
            queueItems.Add(queueItem);
        }
    }

    void renderProps(QueueItemContent item, GameObject itemObject)
    {
        int initHeight = -40;
        int tmpPos = initHeight;
        int heightDelta = 30;

        foreach (KeyValuePair<string, string> kvp in item.properities)
        {
            GameObject prop = Instantiate(propPrefab, Vector3.zero, transform.rotation);
            prop.transform.SetParent(itemObject.transform);
            prop.transform.localPosition = new Vector3(60f, tmpPos, 0);
            TextMeshProUGUI text = prop.GetComponent<TextMeshProUGUI>();
            text.text = kvp.Key + ": " + kvp.Value;
            tmpPos -= heightDelta;
        }
    }

    void cleanUp()
    {
        for(int i=0;i<queueItems.Count;i++)
        {
            Destroy(queueItems[i]);
        }
        queueItems = new List<GameObject>();
    }


}

public class QueueItemContent
{
    public Dictionary<string, string> properities = new Dictionary<string, string>();
    public string nodeName;

    public QueueItemContent()
    {

    }

    public void populateProps(string name, List<string> labels, List<string> values)
    {
        nodeName = name;
        properities = new Dictionary<string, string>();

        for (int i=0;i< labels.Count;i++)
        {
            properities.Add(labels[i], values[i]);
        }
    }

    



}
