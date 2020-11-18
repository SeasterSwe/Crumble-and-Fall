using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawnSurface;
    public bool linkBlox;

    private int spawnBoundX;
    private int spawnBoundY;
    private Vector2 spawnPos;

    public GameObject instOne;
    public GameObject instTwo;
    public GameObject instTre;

    private GameObject[,] spawnedBoxes;
    private int[,] spawnedType;

    void Start()
    {
        Vector2 spawnBound = spawnSurface.GetComponent<Renderer>().bounds.extents;
        spawnPos = spawnSurface.transform.position;
        Destroy(spawnSurface);

        spawnPos -= spawnBound;
        spawnBound *= 2;
        spawnBoundX = Mathf.RoundToInt(spawnBound.x);
        spawnBoundY = Mathf.RoundToInt(spawnBound.y);

        SpawnBoxes();
        if (linkBlox)
            ChunkBoxes();

        AddRigidBody();
    }

    private void ResetBlocks()
    {
        DestroyAllBlox();
        SpawnBoxes();
        if (linkBlox)
            ChunkBoxes();

        AddRigidBody();
    }
     void DestroyAllBlox()
    {
        for(int x = 0; x < spawnBoundX; x++)
        {
            for(int y = 0; y < spawnBoundY; y++)
            {
                Destroy(spawnedBoxes[x, y]);
            }
        }
    }
    void SpawnBoxes()
    {
        spawnedBoxes = new GameObject[spawnBoundX, spawnBoundY];
        spawnedType = new int[spawnBoundX, spawnBoundY];

        for (int x = 0; x < spawnBoundX; x++)
        {
            for (int y = 0; y < spawnBoundY; y++)
            {
                int randomN = Random.Range(0, 3);

                if (randomN == 1)
                {
                    spawnedBoxes[x, y] = Instantiate(instOne, spawnPos + new Vector2(x, y), Quaternion.identity);
                    spawnedType[x, y] = 1;
                }
                else if (randomN == 2)
                {
                    spawnedBoxes[x, y] = Instantiate(instTwo, spawnPos + new Vector2(x, y), Quaternion.identity);
                    spawnedType[x, y] = 2;
                }
                else
                {
                    spawnedBoxes[x, y] = Instantiate(instTre, spawnPos + new Vector2(x, y), Quaternion.identity);
                    spawnedType[x, y] = 3;
                }
            }
        }
    }

    void ChunkBoxes() {
        //Check neighbour
        for (int x = 0; x < spawnBoundX; x++)
        {
            for (int y = 0; y < spawnBoundY; y++)
            {
                if (!(x + 1 >= spawnBoundX))
                {
                    if (spawnedType[x, y] == spawnedType[x + 1, y])
                    {
                        LinkToRoot(spawnedBoxes[x + 1, y].transform, spawnedBoxes[x, y].transform);
                    }
                }
                if (!(x - 1 < 0))
                {
                    if (spawnedType[x, y] == spawnedType[x - 1, y])
                    {
                        LinkToRoot(spawnedBoxes[x - 1, y].transform, spawnedBoxes[x, y].transform);
                    }
                }
                if (!(y + 1 >= spawnBoundY))
                {
                    if (spawnedType[x, y] == spawnedType[x, y + 1])
                    {
                        LinkToRoot(spawnedBoxes[x, y + 1].transform, spawnedBoxes[x, y].transform);
                    }
                }
                if (!(y - 1 < 0))
                {
                    if (spawnedType[x, y] == spawnedType[x, y - 1])
                    {
                        LinkToRoot(spawnedBoxes[x, y - 1].transform, spawnedBoxes[x, y].transform);
                    }
                }
            }
        }
    }

    void AddRigidBody() { 
        for (int x = 0; x < spawnBoundX; x++)
        {
            for(int y = 0; y < spawnBoundY; y++)
            {
                if (spawnedBoxes[x,y].transform.parent == null) {
                    spawnedBoxes[x,y].AddComponent<Rigidbody2D>();
                    spawnedBoxes[x,y].GetComponent<Rigidbody2D>().mass = 1 + spawnedBoxes[x,y].transform.childCount;
                    Debug.DrawRay(spawnedBoxes[x,y].transform.position - Vector3.forward, Vector3.up * 0.25f, Color.red, 1);
                }
            }
        }
    }

    void LinkToRoot(Transform c, Transform p)
    {
        if(p.parent == null)
        {
            c.parent = p;
            return;
        }
        else
        {
            c.parent = p.parent;
            return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            ResetBlocks();
        }
    }
}
