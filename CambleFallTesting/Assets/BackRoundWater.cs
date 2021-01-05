using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRoundWater : MonoBehaviour
{
    public int sortingOrder;
    public GameObject water;
    private Mesh mesh;
    Test2 test2;
    private void Start()
    {
        //mesh = Instantiate( water.GetComponent<MeshFilter>().mesh);
        mesh = water.GetComponent<MeshFilter>().mesh;
        test2 = water.GetComponent<Test2>();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<Renderer>().sortingOrder = sortingOrder;
        Vector3[] vertices = test2.vertices;
        
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            if(i < vertices.Length * 0.5)
            {
                //uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
                uvs[i] = Vector2.one * 0.5f ;
            }
            else
            {
                uvs[i] = Vector2.one;
            }
        }

        mesh.uv = uvs;
        mesh.RecalculateNormals();
        gameObject.GetComponent<Renderer>().sortingOrder = sortingOrder;
    }
    private void Update()
    {
        mesh.vertices = test2.vertices;
        mesh.triangles = test2.triangels;
        GetComponent<Renderer>().sortingOrder = sortingOrder;
    }
}
