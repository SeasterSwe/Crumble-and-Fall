using UnityEngine;

public class FX_WaterReflection : MonoBehaviour
{
    //2D waterReflection made by Robert Sandh
    //Add to Quadmesh component
    public int pxPerMeter = 32;
    void Start()
    {
        Vector2 bounds = new Vector2(33, 10);// gameObject.GetComponent<Renderer>().bounds.extents;

        RenderTexture rtex = new RenderTexture((int)(bounds.x * 2 * pxPerMeter), (int)(bounds.y * 2 * pxPerMeter),100);
        rtex.filterMode = FilterMode.Point;

        Camera reflectionCam = new GameObject().AddComponent<Camera>();
        reflectionCam.orthographic = true;
        reflectionCam.targetTexture = rtex;
        reflectionCam.orthographicSize = bounds.y;
        reflectionCam.transform.position = new Vector3(0, -6, -20);

        gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", rtex);
    }
}
