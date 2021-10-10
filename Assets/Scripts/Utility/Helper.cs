using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{

    public static GameObject FindClosestGameObject(Vector3 origin, string tag, List<GameObject> exclusionList = null)
    {
        GameObject[] allNodes = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject node in allNodes)
        {
            Vector3 diff = node.transform.position - origin;
            float curDistance = diff.magnitude;
            if (curDistance < distance && !exclusionList.Contains(node))
            {
                  closest = node;
                  distance = curDistance;
            }
        }

        return closest;
    }

    public static bool CheckIfInRange(float maxRange, GameObject source, GameObject dest)
    {
        Vector3 vector = source.transform.position - dest.transform.position;
        float distance = vector.magnitude;
        if(distance < maxRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
