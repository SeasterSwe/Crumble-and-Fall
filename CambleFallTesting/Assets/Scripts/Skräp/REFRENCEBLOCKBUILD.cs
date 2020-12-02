/*/
 * WARNIGN : ONLY REFRENCE!!!!!!!!!!!!!!!
 */

//Christian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Creates new blocks by input

public class REFRENCEBLOCKBUILD : MonoBehaviour
{
    private Transform spawnerObject;

    [Header("Settings")]
    public LayerMask buildLayer;
    public int playerNumber = 1;
    public string inputHorizontal = "HorizontalPlayer1";
    public string inputSpawn = "Fire1";
    public float movementSpeed = 5;

    public float moveStep = 0.25f;
    public float timeBetweenSteps = 0.1f;
    private float timeToNextStep = 0.0f;

    public int numberOfBlocks = 10;

    public GameObject blockPreFab;
    private Vector2 spawnAreaSize;
    private float minX;
    private float maxX;
    private Vector3 spawnerPosition;

    private int nextBlockType = 0;

    private void Start()
    {
        SpawnAreaSize();
        spawnerObject = transform.Find("Spawner");
        spawnerPosition = spawnerObject.parent.position;
        nextBlockType = (int) Random.Range(0, 3);
        GetComponentInChildren<SpriteRenderer>().color = SetColorByNumber(nextBlockType);
    }
   

    private void Update()
    {
        SpawnerLocation();
        AlignSpawnerToGround();

        if (Input.GetButtonDown(inputSpawn))
        {
            if (numberOfBlocks > 0)
            {
                SpawnBlock();
                numberOfBlocks--;

                nextBlockType = (int)Random.Range(0, 3);
                SetColorByNumber(nextBlockType);
                GetComponentInChildren<SpriteRenderer>().color = SetColorByNumber(nextBlockType);
            }
        }
    }

    Color SetColorByNumber(int i)
    {
        if(i == 1)
        {
            return Color.green;
        }else if(i == 2)
        {
            return Color.blue;
        }
        else
        {
            return Color.red;
        }
    }

    //minX and maxX from SpawnArea box size. 
   private void SpawnAreaSize()
    {
        GameObject spawnArea = transform.Find("BuildArea").gameObject; // Hittat vårt objekt
        spawnAreaSize = spawnArea.GetComponent<Renderer>().bounds.extents; // Tar ut renderaren från den och använda värdena från bounds
        Vector2 position = new Vector2(transform.position.x, transform.position.y); // Mittposition

        Vector2 cornerPosition = position - spawnAreaSize; // Skapat hörnposition

        spawnAreaSize *= 2;
        Debug.DrawRay(cornerPosition, spawnAreaSize, Color.red, 50); // Skriver ut röda
        Debug.DrawRay(cornerPosition, Vector2.up, Color.blue, 50); // Skriver ut blåa

        minX = cornerPosition.x;
        maxX = cornerPosition.x + spawnAreaSize.x;

        Destroy(spawnArea);
    }

   //Step Left Right by inputAxis and timeIntervall
   private void SpawnerLocation()
    {
        if (Input.GetAxis(inputHorizontal) != 0)
        {
            if (timeToNextStep <= 0)
            {
                //spawnerPosition.x += Input.GetAxis(inputHorizontal) * movementSpeed * Time.deltaTime;
                spawnerPosition.x += Mathf.RoundToInt(Input.GetAxisRaw(inputHorizontal)) * moveStep;

                if (spawnerPosition.x < minX)
                {
                    spawnerPosition.x = minX;
                }
                else if (spawnerPosition.x > maxX)
                {
                    spawnerPosition.x = maxX;
                }
                timeToNextStep = timeBetweenSteps;
            }
        }
        else
        {
            timeToNextStep = 0;
        }
        spawnerObject.position = spawnerPosition;

        timeToNextStep -= Time.deltaTime;
    }

    //Move aim to ground by Boxcast
    private void AlignSpawnerToGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(spawnerObject.position + Vector3.up * 20, Vector2.one *0.99f, 0, Vector2.down, Mathf.Infinity, buildLayer);
        if (hit)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.green);
            spawnerObject.position = new Vector3(spawnerObject.position.x, hit.point.y + 0.5f, 0);
        }
    }
    // spawnar ett block. 
    private void SpawnBlock()
    {
        GameObject newBlock = Instantiate(blockPreFab, spawnerObject.position, Quaternion.identity);
        BlockType blockScript = newBlock.GetComponent<BlockType>();
        blockScript.setCatagoryByNumber(nextBlockType);
        blockScript.playerteam = playerNumber;
    }
}
