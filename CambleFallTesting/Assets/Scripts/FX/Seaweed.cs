using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Seaweed : MonoBehaviour
{
    [Header("Model")]
//    public float startWitdh = 0.2f;
//    public float endWidth = 0;
    public int segments = 6;
    private float step;

    [Header("Motion")]
    public bool autoOffsett = true;
    public float animationOffsett = 0;
    public float motionSpeed = 2;
    public float motionAmount = 10;
    public float motionOffsett = 0.5f;
    private LineRenderer liner;

    private float animValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        liner = GetComponent<LineRenderer>();
        InitLineRenderer();

        if (autoOffsett)
            animationOffsett = Random.Range(0, Mathf.PI*2);

        animValue = animationOffsett;

       // GetComponent<Renderer>().sortingOrder = 1000;
    }

    void InitLineRenderer()
    {
        float length = liner.GetPosition(liner.positionCount - 1).y;
        step = length / (segments - 1);

        liner.positionCount = segments;
        for (int i = 0; i < segments; i++)
        {
            liner.SetPosition(i, Vector3.up * i * step);
        }

       // liner.startWidth = startWitdh;
       // liner.endWidth = endWidth;
    }
    // Update is called once per frame
    void Update()
    {
        animValue += Time.deltaTime;
        AnimateLine();
    }
    void AnimateLine()
    {
        for (int i = 1; i < liner.positionCount; i++)
        {
            Vector3 rootPos = liner.GetPosition(i - 1);
            Vector3 dir = Quaternion.Euler(0, 0, Mathf.Sin(animValue + motionOffsett * i) * motionAmount) * (Vector3.up * step);
            liner.SetPosition(i, rootPos + dir);
        }
    }
}
