using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    public int numberOfPoints;
    public float length;
    public float waveHeight = 2;

    private float distBetween;
    private LineRenderer line;
    private List<Vector3> points = new List<Vector3>(); 

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = numberOfPoints;
    }

    void Update()
    {
        GeneratePoints();
    }
    public void GeneratePoints()
    {
        points.Clear();
        float halfWaveHeight = waveHeight * 0.5f;
        float step = length / numberOfPoints;
        for (int i = 0; i < numberOfPoints; ++i)
        {
            points.Add(new Vector3(i * step, Mathf.Sin(step * i + Time.time) * halfWaveHeight, 0));
            line.SetPosition(i, points[i]);
        }
    }
}
