using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWater : MonoBehaviour
{
    //tracking positions
    float[] xPositions;
    float[] yPositions;
    float[] velocities;
    float[] accelerations;
    LineRenderer body;

    GameObject[] meshObjects;
    GameObject[] colliders;
    Mesh[] meshes;

    public float springconstant = 0.02f;
    public float damping = 0.03f;
    public float spread = 0.05f;
    const float z = -1f;

    //dimensions of the water.
    public float baseHeight;
    private float left;
    public float bottom;
    public float width;

    public float maxHeight = 3f;
    public float minHeight = -3f;

    //splasheffekt
    public GameObject splash;
    //watermat
    public Material mat;
    public GameObject waterMesh;

    public float forceDivider = 1.4f;
    public float waveSpeed = 1.2f;
    public float waveFrequancy = 20;
    public float waveDamper = 20;
    private void Start()
    {
        left = -width / 2;
        CreateWater(left, width, baseHeight, bottom);
        maxHeight = baseHeight + maxHeight;
        minHeight = baseHeight - minHeight;
    }

    public void CreateWater(float left, float width, float top, float bottom)
    {
        int edgecount = Mathf.RoundToInt(width) * 5;
        int nodecount = edgecount + 1;

        //fixar linerenderer
        body = gameObject.GetComponent<LineRenderer>();
        //body.material = mat;
        body.material.renderQueue = 1000;
        body.SetVertexCount(nodecount);

        //body.SetWidth(0.1f, 0.1f); //tweeka sen

        //skapar variablarna i toppen
        xPositions = new float[nodecount];
        yPositions = new float[nodecount];
        velocities = new float[nodecount];
        accelerations = new float[nodecount];

        meshObjects = new GameObject[edgecount];
        meshes = new Mesh[edgecount];
        colliders = new GameObject[edgecount];

        baseHeight = top;
        this.bottom = bottom;
        this.left = left;
        this.width = width;

        //säter all data
        for (int i = 0; i < nodecount; i++)
        {
            yPositions[i] = top;
            xPositions[i] = left + width * i / edgecount;
            accelerations[i] = 0;
            velocities[i] = 0;
            body.SetPosition(i, new Vector3(xPositions[i], yPositions[i], z));
        }

        //skapa meshes
        for (int i = 0; i < edgecount; i++)
        {
            meshes[i] = new Mesh();
            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xPositions[i], yPositions[i], z);
            Vertices[1] = new Vector3(xPositions[i + 1], yPositions[i + 1], z);
            Vertices[2] = new Vector3(xPositions[i], bottom, z);
            Vertices[3] = new Vector3(xPositions[i + 1], bottom, z);

            Vector2[] UVs = new Vector2[4];
            UVs[0] = new Vector2(0, 1);
            UVs[1] = new Vector2(1, 1);
            UVs[2] = new Vector2(0, 0);
            UVs[3] = new Vector2(1, 0);

            //ritordning
            int[] tris = new int[6] { 0, 1, 3, 3, 2, 0 };
            meshes[i].vertices = Vertices;
            meshes[i].uv = UVs;
            meshes[i].triangles = tris;

            //Watermesh har meshfilter och meshrenderer
            meshObjects[i] = Instantiate(waterMesh, Vector3.zero, Quaternion.identity) as GameObject;
            meshObjects[i].GetComponent<MeshFilter>().mesh = meshes[i];
            meshObjects[i].transform.parent = transform;
            meshObjects[i].GetComponent<Renderer>().sortingOrder = 200;

            //colliders
            colliders[i] = new GameObject();
            colliders[i].name = "Trigger";
            colliders[i].AddComponent<BoxCollider2D>();
            colliders[i].transform.parent = transform;
            colliders[i].transform.position = new Vector3(left + width * (i + 0.5f) / edgecount, top - 0.5f, 0);
            colliders[i].transform.localScale = new Vector3(width / edgecount, 1, 1);
            colliders[i].GetComponent<BoxCollider2D>().isTrigger = true;
            colliders[i].layer = 2;

            //splashEffekt
            colliders[i].AddComponent<WaterDetector>();

        }
    }
    void UpdateMeshes()
    {
        //float r = Random.Range(0, 100f);
        //if(r >= 96)
        //{
        //    int index = Random.Range(0, velocities.Length);
        //    if (velocities[index] <= 0.1f && velocities[index] >= -0.1f)
        //    {
        //        velocities[index] += Random.Range(-2, 2);
        //        print(velocities[index]);
        //    }
        //}
        //if(r >= 99)
        //{
        //    float index = Random.Range(0, 10f);
        //    if (index >= 5)
        //        velocities[velocities.Length - 1] = Random.Range(5,10f);
        //    else
        //        velocities[0] = Random.Range(5, 10f);
        //}

        //for (int i = 0; i < velocities.Length; i++)
        //{
        //    if(i % 10 == 0)
        //        velocities[i] += Mathf.Sin(i + Time.time)/10;
        //}

        for (int i = 0; i < meshes.Length; i++)
        {
            //ändra offset till robert animationcurve?
            /* Mathf.Clamp(yPositions[i], minHeight, maxHeight)*/
            //float offset = Mathf.Pow(Mathf.Abs(xPositions[i] % 6) - 3, 2) * Mathf.Sin(Mathf.Sin((Time.time * waveSpeed) + (i * waveFrequancy)) / waveDamper);
            //float offset = Mathf.Pow(Mathf.Abs(xPositions[i] % 6) - 3,2); 
            //float offset = Mathf.Pow(Mathf.Abs(xPositions[i] % 6) - 3,2) * Mathf.Sin(Time.time * 0.01f); 
            //float offset = Mathf.Sin(Mathf.Sin((Time.time * waveSpeed) + (i * waveFrequancy)) / waveDamper);
            float offset = Mathf.Sin(Mathf.Sin((Time.time * waveSpeed) + (i * waveFrequancy)) / waveDamper);
            //offset = Mathf.Abs(offset);
            offset = Mathf.Clamp(offset, 0, Mathf.Infinity);
            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xPositions[i], Mathf.Clamp(yPositions[i], minHeight, maxHeight) + offset, z);
            Vertices[1] = new Vector3(xPositions[i + 1], Mathf.Clamp(yPositions[i + 1], minHeight, maxHeight) + offset, z);
            Vertices[2] = new Vector3(xPositions[i], bottom, z);
            Vertices[3] = new Vector3(xPositions[i + 1], bottom, z);

            meshes[i].vertices = Vertices;
        }
    }

    private void FixedUpdate()
    {
        //First, we're going to combine Hooke's Law with the Euler method to find the new positions, accelerations and velocities.   
        //https://en.wikipedia.org/wiki/Euler_method
        //https://sv.wikipedia.org/wiki/Hookes_lag

        for (int i = 0; i < xPositions.Length; i++)
        {
            float force = springconstant * (yPositions[i] - baseHeight) + velocities[i] * damping;
            accelerations[i] = -force;
            yPositions[i] += velocities[i];
            velocities[i] += accelerations[i];
            body.SetPosition(i, new Vector3(xPositions[i], yPositions[i], z));

            //we just add the acceleration to the velocity and the velocity to the position, every frame.
            accelerations[i] = -force / 1; //1 kan bli mass
        }

        float[] leftDeltas = new float[xPositions.Length];
        float[] rightDeltas = new float[xPositions.Length];

        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < xPositions.Length; i++)
            {
                if (i > 0)
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

            for (int i = 0; i < xPositions.Length; i++)
            {
                if (i > 0)
                {
                    yPositions[i - 1] += leftDeltas[i];
                }
                if (i < xPositions.Length - 1)
                {
                    yPositions[i + 1] += rightDeltas[i];
                }
            }
        }
        UpdateMeshes();
    }

    public void Splash(float xPos, float velocity)
    {
        if (xPos >= xPositions[0] && xPos <= xPositions[xPositions.Length - 1])
        {
            velocity = velocity / forceDivider;
            xPos -= xPositions[0];
            //vilken node den träffar
            int index = Mathf.RoundToInt((xPositions.Length - 1) * (xPos / (xPositions[xPositions.Length - 1] - xPositions[0])));

            if (Mathf.Abs(velocities[index]) > 0.3f)
            {
               // print(velocities[index]);
                return;
            }


            velocities[index] = velocity;

            //två startspeed för unity äger :D
            float lifetime = 0.93f + Mathf.Abs(velocity) * 0.07f;

            splash.GetComponent<ParticleSystem>().startSpeed = 8 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
            splash.GetComponent<ParticleSystem>().startSpeed = 9 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
            splash.GetComponent<ParticleSystem>().startLifetime = lifetime;

            Vector3 position = new Vector3(xPositions[index], yPositions[index] - 0.35f, 5);
            Quaternion rotation = Quaternion.LookRotation(new Vector3(xPositions[Mathf.FloorToInt(xPositions.Length / 2)], baseHeight + 8, 5) - position);

            GameObject splish = Instantiate(splash, position, rotation) as GameObject;
            Destroy(splish, lifetime + 0.3f);


        }
    }
}
