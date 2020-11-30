using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockbuilder : MonoBehaviour
{
    private Transform spawnerObject;

    [Header("Settings")]
    public string inputHorizontal = "HorizontalPlayerOne";
    public string inputSpawn = "FirePlayerOne";
    public float moveStep = 0.5f;
    public GameObject blockPreFab;
    private float minX;
    private float maxX;
    public float timeBetweenStep = 0.25f;
    public float timeToNextStep;
    private Vector3 spawnerPosition;
    public Inventory inventory;

    private GameObject[] chooseBlocks;
    //private Inventory inventory;
    public string pickButton = "VerticalPlayerOne";
    int activeBlock = 0;

    public Color red = Color.red;
    public Color green = Color.green;
    public Color blue = Color.blue;

    public float maxHeight = 10.0f;
    public float spriteAlpha = 0.5f;


    private void Start()
    {
        SpawnAreaSize();

        spawnerObject = transform.Find("Spawner");
        spawnerPosition = spawnerObject.parent.position;
        inventory = GetComponent<Inventory>();
        blockPreFab = BlockList.GetARandomBlock();

        chooseBlocks = BlockList.buildList;

        AimChangeColor();
    }

    // minmaxX från spawn areas volym. Sätter x koordinater. 
    private void SpawnAreaSize()
    {
        GameObject spawnArea = transform.Find("SpawnArea").gameObject; // Hittat vårt objekt
        Vector2 size = spawnArea.GetComponent<Renderer>().bounds.extents; // Tar ut renderaren från den och använda värdena från bounds
        Vector2 position = new Vector2(transform.position.x, transform.position.y); // Mittposition

        Vector2 cornerPosition = position - size; // Skapat hörnposition
        size *= 2; // Multiplicerar med 2 för att täcka hela arean

        Debug.DrawRay(cornerPosition, size, Color.red, 50); // Skriver ut röda
        Debug.DrawRay(cornerPosition, Vector2.up, Color.blue, 50); // Skriver ut blåa

        minX = cornerPosition.x;
        maxX = cornerPosition.x + size.x;

        Destroy(spawnArea);
    }

    private void Update()
    {
        SpawnerLocation();
        AccurateBlockSpawn();

        if (Input.GetButtonDown(inputSpawn) && inventory.CheckInventory(blockPreFab.GetComponent<BlockType>().category))
        {
            SpawnBlock();
        }

        if (Input.GetButtonDown(pickButton))
        {
            ToggleBetweenBlocks();
            AimChangeColor();
        }

    }
    // flytta höger vänster via input inom minxmaxx intervallet
    private void SpawnerLocation()
    {
       // spawnerPosition.x += Input.GetAxisRaw(inputHorizontal) * moveStep;

        

        if (Input.GetButton(inputHorizontal))
        {
            timeToNextStep -= Time.deltaTime;
            if (timeToNextStep < 0)
            {
                spawnerPosition.x += Input.GetAxisRaw(inputHorizontal) * moveStep;
                timeToNextStep = timeBetweenStep;
            }
        }

        else
        {
            timeToNextStep = -1;
        }
        if (spawnerPosition.x < minX)
        {
            spawnerPosition.x = minX;
        }
        else if (spawnerPosition.x > maxX)
        {
            spawnerPosition.x = maxX;
        }

        spawnerObject.position = spawnerPosition;
    }

    // flytta upp och ner beroende på markhöjd (med användning av BoxCast)

    private void AccurateBlockSpawn()
    {
        RaycastHit2D hit = Physics2D.BoxCast(spawnerObject.position + Vector3.up * 20, Vector2.one, 0, Vector2.down);
        if (hit != null)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.green);
            spawnerObject.position = new Vector3(spawnerObject.position.x, hit.point.y + 0.5f, 0);
        }
    }
    // spawnar ett block. 
    private void SpawnBlock()
    {

        if (spawnerObject.position.y <= maxHeight)
        {
            inventory.RemoveFromInventory(blockPreFab.GetComponent<BlockType>().category);
            GameObject newBlock = Instantiate(blockPreFab, spawnerObject.position, Quaternion.identity);
            //BlockType blockScript = newBlock.GetComponent<BlockType>();
        }
    }

    public void ToggleBetweenBlocks()
    {
        int nextBlock = activeBlock + 1;
        nextBlock = nextBlock % chooseBlocks.Length;
        activeBlock = nextBlock;
        blockPreFab = chooseBlocks[activeBlock];

    }

    public void AimChangeColor()
    {
        var spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        Color blockColor = spriteRenderer.color;
        blockColor.a = spriteAlpha;
        spriteRenderer.color = blockColor;

        spriteRenderer.sprite = blockPreFab.GetComponent<SpriteRenderer>().sprite;

    }

}
