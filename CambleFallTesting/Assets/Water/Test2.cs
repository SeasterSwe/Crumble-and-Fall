using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test2 : MonoBehaviour
{
    //tracking positions
    float[] xPositions;
    float[] yPositions;
    float[] velocities;
    float[] accelerations;
    GameObject[] colliders;
    public GameObject splash;
    [Header("Vertexer")]
    public int xSize = 20;

    [Header("WaterStats")]
    const float springconstant = 0.02f;
    public float damping = 0.03f;
    public float spread = 0.05f;
    const float zPos = -1;

    private int zSize = 1;
    [HideInInspector] public Vector3[] vertices;
    [HideInInspector] public int[] triangels;
    private Mesh mesh;

    [Header("IdelWave")]
    public float forceDivider = 1.4f;
    public float waveSpeed = 1.2f;
    public float waveFrequancy = 20;
    public float waveDamper = 20;
  
    [Header("NoiseForIdelWave")]
    public float noiseStreangth = 1f;
    public float noiseWalk = 1f;
    public float noiseMoveSpeed = 1f;

    [Header("Dimensions")]
    //dimensions of the water.
    public float baseHeight;
    private float left;
    public float bottom;
    public float width;

    public float maxHeight = 3f;
    public float minHeight = -3f;

    LineRenderer line;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        left = -width / 2;
        maxHeight = baseHeight + maxHeight;
        minHeight = baseHeight - minHeight;
        line = GetComponent<LineRenderer>();
        CreateShape(left, width, baseHeight, bottom);
        // transform.position += Vector3.right * (width / 4);
        UpdateMesh();
    }

    void CreateShape(float left, float width, float top, float bottom)
    {
        int vertexCount = (xSize + 1) * 2;
        line.SetVertexCount(xSize + 1);
        line.sortingOrder = 220;
        line.SetPosition(0, new Vector2(left + width / 4, top));
        vertices = new Vector3[vertexCount];
        int numberOfColliders = vertexCount / 2;
        colliders = new GameObject[numberOfColliders];

        yPositions = new float[vertexCount];
        xPositions = new float[vertexCount];
        velocities = new float[vertexCount];
        accelerations = new float[vertexCount];

        baseHeight = top;
        this.bottom = bottom;
        this.left = left;
        this.width = width;

        //Robbans Test
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z < 2; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                if (z == 0)
                {
                    yPositions[i] = bottom;
                    uvs[i] = new Vector2(0.25f, 0);//test
                }
                else
                {
                    yPositions[i] = top;
                    uvs[i] = new Vector2(0.25f, 1);//test
                }

                xPositions[i] = left + width * x / (vertexCount - 1);
                xPositions[i] += width / 4;
                velocities[i] = 0;
                accelerations[i] = 0;
                vertices[i] = new Vector3(xPositions[i], yPositions[i], zPos);
                i++;
            }
        }

        int vert = 0;
        int tris = 0;
        //gör en quad

        triangels = new int[(xSize * zSize * 6) - 6];
        for (int x = 0; x < xSize - 1; x++)
        {
            triangels[tris] = vert;
            triangels[tris + 1] = vert + xSize;
            triangels[tris + 2] = vert + 1;

            triangels[tris + 3] = vert + 1;
            triangels[tris + 4] = vert + xSize;
            triangels[tris + 5] = vert + 1 + xSize;

            vert++;
            tris += 6;
        }

        for (int i = 0; i < numberOfColliders; i++)
        {
            colliders[i] = new GameObject();
            colliders[i].name = "Trigger";
            colliders[i].AddComponent<BoxCollider2D>();
            colliders[i].transform.parent = transform;
            colliders[i].transform.position = new Vector3((left + width * (i * 0.5f) / (numberOfColliders - 1)) + (width / 4), top - 0.5f, zPos);
            colliders[i].transform.localScale = new Vector3(width / (vertexCount - 1), 1, 1);

            colliders[i].GetComponent<BoxCollider2D>().isTrigger = true;
            colliders[i].layer = 2;
            //splashEffekt
            colliders[i].AddComponent<WaterDetector>();
            colliders[i].GetComponent<WaterDetector>().index = i;

        }
        GetComponent<Renderer>().sortingOrder = 200;
        UpdateMesh();
     
        /*
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
        }
        */
        mesh.uv = uvs;
        //  mesh.RecalculateNormals();
    }

    void UpdateMesh()
    {
        //mesh.Clear();
        MakeWaves();

        mesh.vertices = vertices;
        mesh.triangles = triangels;
        //mesh.RecalculateNormals();
    }
    void MakeWaves()
    {
        for (int i = vertices.Length / 2 - 1; i < vertices.Length - 1; i++)
        {
            float y = yPositions[i];
            float x = xPositions[i];
            y = Mathf.Clamp(y, minHeight, maxHeight);
            float offset = Mathf.Sin(Mathf.Sin((Time.time * waveSpeed) + (i * waveFrequancy)) / waveDamper);
            offset = Mathf.Clamp(offset, 0, Mathf.Infinity);
            offset += Mathf.PerlinNoise(x * noiseWalk + Time.time * noiseMoveSpeed, y + Mathf.Sin(Time.time * 0.1f)) * noiseStreangth;
            
            if (i - xSize != 0)
                line.SetPosition(i - xSize, new Vector2(x, y + offset));

            vertices[i] = new Vector3(x, y + offset, zPos);
            vertices[i] = new Vector3(xPositions[i + 1], Mathf.Clamp(yPositions[i + 1], minHeight, maxHeight) + offset, zPos);
        }

    }

    private void FixedUpdate()
    {
        int startVal = xPositions.Length / 2;

        for (int i = startVal; i < xPositions.Length; i++)
        {
            float force = springconstant * (yPositions[i] - baseHeight) + velocities[i] * damping;
            accelerations[i] = -force;
            yPositions[i] += velocities[i];
            velocities[i] += accelerations[i];
            accelerations[i] = -force / 1; //1 kan bli mass
        }

        float[] leftDeltas = new float[xPositions.Length];
        float[] rightDeltas = new float[xPositions.Length];

        for (int j = 0; j < 8; j++)
        {
            for (int i = startVal; i < xPositions.Length; i++)
            {
                if (i > startVal)
                {
                    leftDeltas[i] = spread * (yPositions[i] - yPositions[i - 1]);
                    velocities[i - 1] += leftDeltas[i];
                }
                if (i < xPositions.Length - 1)
                {
                    rightDeltas[i] = spread * (yPositions[i] - yPositions[i + 1]);
                    velocities[i + 1] += rightDeltas[i];
                }
            }

            for (int i = startVal; i < xPositions.Length; i++)
            {
                if (i > startVal)
                {
                    yPositions[i - 1] += leftDeltas[i];
                }
                if (i < xPositions.Length - 1)
                {
                    yPositions[i + 1] += rightDeltas[i];
                }
            }
        }
        UpdateMesh();
    }

    public void Splash(int n, float velocity)
    {
        velocity = velocity / forceDivider;
        int index = (velocities.Length / 2) + n;
        if (Mathf.Abs(velocities[index]) > 0.3f)
        {
            return;
        }

        velocities[index] = velocity;

        //två startspeed för unity äger :D
        float lifetime = 0.93f + Mathf.Abs(velocity) * 0.07f;

        splash.GetComponent<ParticleSystem>().startSpeed = 8 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
        splash.GetComponent<ParticleSystem>().startSpeed = 9 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
        splash.GetComponent<ParticleSystem>().startLifetime = lifetime;

        Vector3 position = new Vector3(xPositions[index], yPositions[index] - 0.35f, 5);

        GameObject splish = Instantiate(splash, position, splash.transform.rotation) as GameObject;
        Destroy(splish, lifetime + 0.3f);
    }
}
