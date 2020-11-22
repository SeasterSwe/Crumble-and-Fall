using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBar : MonoBehaviour
{
    public void UpdateValue(float value)
    {
        Vector3 meter = transform.localScale;
        meter.y = value;
        transform.localScale = meter;
    }

    public void UpdateValue(float value, float scale)
    {
        Vector3 meter = transform.localScale;
        meter.y = value;
        meter.y *= scale;
        transform.localScale = meter;
    }
}
