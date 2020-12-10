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

    public static ElevationCheck GetTheLeftOne()
    {
        ElevationCheck[] all = Object.FindObjectsOfType<ElevationCheck>();
        ElevationCheck left = null;

        float minDist = Mathf.Infinity;
        foreach (ElevationCheck gitFtw in all)
        {
            float x = gitFtw.gameObject.transform.position.x;

            if(x < minDist)
            {
                minDist = x;
                left = gitFtw;
            }
        }

        return left;
    }
    public static ElevationCheck GetTheRighttOne()
    {
        ElevationCheck[] all = Object.FindObjectsOfType<ElevationCheck>();
        ElevationCheck right = null;

        float minDist = -1000000f;
        foreach (ElevationCheck gitFtw in all)
        {
            float x = gitFtw.gameObject.transform.position.x;

            if (x > minDist)
            {
                minDist = x;
                right = gitFtw;
            }
        }

        return right;
    }
}
