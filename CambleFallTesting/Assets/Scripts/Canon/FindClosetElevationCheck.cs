using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosetElevationCheck : MonoBehaviour
{
    public static ElevationCheck GetClosets(GameObject obj)
    {
        ElevationCheck[] all = Object.FindObjectsOfType<ElevationCheck>();
        Vector3 currentPosition = obj.transform.position;
        Vector3 eleveationObjPos = new Vector3();

        float minDist = Mathf.Infinity;
        ElevationCheck best = null;
        foreach(ElevationCheck e in all)
        {
            eleveationObjPos = e.gameObject.transform.position;
            float dist = Vector3.Distance(currentPosition, eleveationObjPos);

            if(minDist > dist)
            {
                minDist = dist;
                best = e;
            }
        }
        return best;
    }
}
