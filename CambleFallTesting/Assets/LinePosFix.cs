using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2)]
public class LinePosFix : MonoBehaviour
{
    public float restictValue;
    void Start()
    {
        ElevationCheck elevationCheck = FindClosetElevationCheck.GetClosets(gameObject);
        float heightPercentense = elevationCheck.groundlevel + restictValue;
        var pos = transform.position;
        pos.y = heightPercentense + 0.5f;

        transform.position = pos;
    }
}
