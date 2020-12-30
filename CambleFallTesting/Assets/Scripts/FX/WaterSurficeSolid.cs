using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurficeSolid : MonoBehaviour
{
    [Header("Mesh Settings")]
    public int oderInLayer = 105;
    public float length = 40;
    public float deapth = 10;
    public float segments = 400;
    [Header("Wave Settings")]
    public AnimationCurve wave;
    public float waweLength = 30f;
    public float waweHight = 10f;
    public float waweSpeedOne = 2f;
    public float waweSpeedTwo = 3f;

    private float animOne = 0;
    private float animTwo = 0;



    private int halfLength;
    private Vector3[] verticesArray;
    private Vector2[] uvArray;
    private int[] facesArray;

    private Vector3[] modifiedArray;

    private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        halfLength = (int)(segments / 2 + 1);
        waweLength = 1 / waweLength;
        CreateVertisisForGeometry();
        CreateFaces();
        CreateMesh();

        //Clone array
        modifiedArray = new Vector3[verticesArray.Length];
        for (int i = 0; i < modifiedArray.Length; i++)
        {
            modifiedArray[i] = verticesArray[i];
        }
        meshFilter = GetComponent<MeshFilter>();

        GetComponent<Renderer>().sortingOrder = oderInLayer;
    }


    void CreateVertisisForGeometry()
    {
        float step = length * 2 / segments;
        float uvStep = 2 / segments;

        verticesArray = new Vector3[halfLength * 2];
        uvArray = new Vector2[halfLength * 2];

        Vector3 point = Vector3.zero;
        Vector2 uvPoint = Vector2.zero;

        for (int i = 0; i <= halfLength; i++)
        {
            uvArray[i] = uvPoint;
            verticesArray[i] = point;
            point += Vector3.right * step;
            uvPoint += Vector2.right * uvStep;
            Debug.DrawRay(verticesArray[i], Vector2.up, Color.yellow, 10);
        }

        point -= Vector3.up * deapth;
        point -= Vector3.right * step;

        uvPoint -= Vector2.up;
        uvPoint -= Vector2.right * uvStep;

        for (int i = halfLength; i < verticesArray.Length; i++)
        {
            point -= Vector3.right * step;
            uvPoint -= Vector2.right * uvStep;
            verticesArray[i] = point;
            uvArray[i] = uvPoint;
        }
    }

    void CreateFaces()
    {
        facesArray = new int[(halfLength * 6)];

        int f = 0;
        int l = verticesArray.Length - 1;
        for (int i = 0; i < halfLength; i++)
        {
            facesArray[f] = i;
            facesArray[f + 1] = i + 1;
            facesArray[f + 2] = l - i;

            facesArray[f + 3] = i + 1;
            facesArray[f + 4] = l - i - 1;
            facesArray[f + 5] = l - i;
            f += 6;
        }

        for (int i = 0; i < facesArray.Length; i += 3)
        {
            Debug.DrawLine(verticesArray[facesArray[i]], verticesArray[facesArray[i + 1]], Color.cyan, 5);
            Debug.DrawLine(verticesArray[facesArray[i + 1]], verticesArray[facesArray[i + 2]], Color.cyan, 5);
            Debug.DrawLine(verticesArray[facesArray[i + 2]], verticesArray[facesArray[i]], Color.cyan, 5);
        }
    }


    void CreateMesh()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = verticesArray;
        mesh.uv = uvArray;
        mesh.triangles = facesArray;
        mesh.RecalculateNormals();
    }
    // Update is called once per frame
    void Update()
    {
        animOne += waweSpeedOne * Time.deltaTime;
        animTwo += waweSpeedTwo * Time.deltaTime;
        for (int i = 0; i < halfLength; i++)
        {
            modifiedArray[i] = verticesArray[i] + Vector3.up * waweHight * wave.Evaluate(i * waweLength + animOne);
            modifiedArray[i] += Vector3.up * waweHight * wave.Evaluate(i * waweLength + animTwo);
        }
        meshFilter.mesh.vertices = modifiedArray;
    }
}
