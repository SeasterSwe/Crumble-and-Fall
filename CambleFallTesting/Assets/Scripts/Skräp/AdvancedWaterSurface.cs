using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedWaterSurface : MonoBehaviour
{
    int numberOfPoints = 200;
    float width = 30;
    const float springConstant = 0.005f;
    const float springConstantBaseLine = 0.005f;
    float yOffset = 0.1f;
    float damping = 0.98f;
    int interations = 5;
    private LineRenderer lineRenderer;
    Vector3[] wavePoints;

    private void Start()
    {
        wavePoints = new Vector3[numberOfPoints];
        lineRenderer = GetComponent<LineRenderer>();
        wavePoints = MakeWavePoints(numberOfPoints);
        lineRenderer.positionCount = numberOfPoints;
        SineStuff();
    }
    //dafuq?
    private Vector3[] MakeWavePoints(int n)
    {
        Vector3[] points = new Vector3[n];
        for (float i = 0; i < n; i++)
        {
            float x = (i / n) * width;
            float y = yOffset;

            Vector3 pos = new Vector3(x, y, 0);

            //lineRenderer.SetPosition((int)i, pos); 
            points[(int)i] = pos;
        }
        return points;
    }

    float offset = 0;
    int numOfBackroundWaves = 2;
    float backRoundWavesMaxHeight = 3;
    float backRoundWaveCompression = 1 / 5;
    float[] sineOffsets;
    float[] sineAmplitudes;
    float[] sineStretches;
    float[] sineOffSetStretches;

    void SineStuff()
    {
        sineOffsets = new float[numOfBackroundWaves];
        sineAmplitudes = new float[numOfBackroundWaves];
        sineStretches = new float[numOfBackroundWaves];
        sineOffSetStretches = new float[numOfBackroundWaves];

        for (int i = 0; i < numOfBackroundWaves; i++)
        {
            sineOffsets[i] = 1 - 2 * Random.Range(0, 1f);
            sineAmplitudes[i] = Random.Range(0, 1f) * backRoundWavesMaxHeight;
            sineStretches[i] = Random.Range(0, 1f) * backRoundWaveCompression;
            sineOffSetStretches[i] = Random.Range(0, 1f) * backRoundWaveCompression;
        }

    }

    //inout x?
    float OverLapSines(float x)
    {
        SineStuff();
        var result = 0f;
        for (int i = 0; i < numOfBackroundWaves; i++)
        {
            result = result + sineOffsets[i] + sineAmplitudes[i] * Mathf.Sin(x * sineStretches[i] + offset * sineOffSetStretches[i]);
        }
        return result;
    }

    void UpdatePoints(Vector3[] points)
    {
        for (int x = 0; x < interations; x++)
        {
            for (int i = 0; i < points.Length; i++)
            {
                float force = 0;
                float forceFromLeft, forceFromRight;

                if (i == 0)
                {
                    float dyy = points[i].y - points[i + 1].y;
                    forceFromLeft = springConstant * dyy;
                }
                else
                {
                    float dyy = points[i - 1].y - points[i].y;
                    forceFromLeft = springConstant * dyy;
                }

                if (i == points.Length - 1)
                {
                    float dyy = points[0].y - points[i].y;
                    forceFromRight = springConstant * dyy;
                }
                else
                {
                    float dyy = points[i + 1].y - points[i].y;
                    forceFromRight = springConstant * dyy;
                }

                float dy = yOffset - points[i].y;
                float forceToBaseLine = springConstantBaseLine * dy;

                force += forceFromLeft;
                force += forceFromRight;
                force += forceToBaseLine;

                float acc = force / 1; //1 = mass
                points[i].y = damping * points[i].y + acc;
            }
        }
    }

    float time;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            offset += 1;
        }

        UpdatePoints(wavePoints);

        if (Time.time > time)
        {
            time = Time.time + 0.1f;
            for (int i = 0; i < numberOfPoints + 1; i++)
            {
                if (i == 0) { }
                else
                {
                    Vector3 leftPoint = wavePoints[i - 1];
                    Vector3 posLeft = new Vector3(leftPoint.x, (leftPoint.y + OverLapSines(leftPoint.x)) / 10);
                    lineRenderer.SetPosition(i - 1, posLeft);
                }
            }
        }
    }
}
