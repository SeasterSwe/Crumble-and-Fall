using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[DefaultExecutionOrder(1)]

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
    public Color aimColor;
    public Color normalColor;

    public LayerMask buildLayer;

    private SpriteRenderer spriteRenderer;

    //private Inventory inventory;
    public string pickButton = "VerticalPlayerOne";
    int activeBlock = 0;

    //public float scaleSize = 1.4f;
    public Ease selectEase;
    public Ease unEase;

    //public Color red = Color.red;
    //public Color green = Color.green;
    //public Color blue = Color.blue;

    public float maxHeight = 10.0f;
    public float spriteAlpha = 0.5f;

    private GameObject spawnParticle;

    [Header("Sound")]
    public float pitchLevelMin = 0.5f;
    public float pitchLevelMax = 1.5f;
    public int pitchLevels = 5;
    private int currentPitchLevel;
    private float pStep;

    private void Start()
    {
        SpawnAreaSize();
     
        spawnerObject = transform.Find("Spawner");
        spawnerPosition = spawnerObject.parent.position;
        //inventory = GetComponent<Inventory>();
        //blockPreFab;// = BlockList.GetARandomBlock();

        // chooseBlocks = BlockList.buildList;

        spriteRenderer = spawnerObject.gameObject.GetComponent<SpriteRenderer>();

        blockPreFab = inventory.selectedBlock;
        AimChangeColor();
        spawnParticle = (Resources.Load("Dust") as GameObject);
        InitPitch();
    }

    private void InitPitch()
    {
        pStep = (pitchLevelMax - pitchLevelMin) / pitchLevels;
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
        if (Time.timeScale != 0 && GameState.currentState != GameState.gameStates.Intermission && GameState.currentState != GameState.gameStates.GameOver)
        {
            SpawnerLocation();
            AccurateBlockSpawn();

            if (Input.GetButtonDown(inputSpawn))
            {
                SpawnBlock();
            }

            if (Input.GetButtonDown(pickButton))
            {
                ToggleBetweenBlocks();
                //AimChangeColor();
            }
        }

        if (inventory.SelectedBlockIsInInventory())
        {
            spriteRenderer.color = normalColor;
        }

        else
        {
            spriteRenderer.color = aimColor;
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
        RaycastHit2D hit = Physics2D.BoxCast(spawnerObject.position + Vector3.up * 20, Vector2.one * 0.98f, 0, Vector2.down, Mathf.Infinity, buildLayer);
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
            if (inventory.SelectedBlockIsInInventory())
            {
                SoundManager.PlaySound(SoundManager.Sound.BuilderPlacementSound, spawnerPosition, TogglePitchUp());

                //vatenhöjd på localspawner
                if (spawnerObject.localPosition.y >= -15f) { 
                    GameObject dust = Instantiate(spawnParticle, spawnerObject.position, spawnParticle.transform.rotation);
                }

                GameObject newBlock = Instantiate(inventory.TakeActiveBlockFromInventory(), spawnerObject.position, Quaternion.identity);
                newBlock.GetComponent<BlockType>().SetState(BlockType.states.Idle);
            }
            else
                SoundManager.PlaySound(SoundManager.Sound.CannonOutOfAmmo, spawnerPosition);
        }
    }

    public float TogglePitchUp()
    {
        currentPitchLevel++;
        if (currentPitchLevel > pitchLevels)
        {
            currentPitchLevel = 0;
        }
        return (pitchLevelMin + pStep * currentPitchLevel);
    } 

    public void ToggleBetweenBlocks()
    {
        inventory.TogggleBlock();
        blockPreFab = inventory.selectedBlock;
        AimChangeColor();
        /*
        //TODO : Change to switch
        int nextBlock = activeBlock + 1;
        nextBlock = nextBlock % chooseBlocks.Length;
        activeBlock = nextBlock;
        blockPreFab = chooseBlocks[activeBlock];

        ScaleImage(blockPreFab.GetComponent<BlockType>().type);
        */
    }
    /*
    public void ScaleImage(BlockType.types blocktype)
        // Visa inte Robban dehär // JAG SÅG DET!!!
    {
        if (blocktype == BlockType.types.Speedy)
            ScaleText(inventory.uiSpeedyImg.gameObject, Vector3.one * scaleSize, selectEase);

        else
            ScaleText(inventory.uiSpeedyImg.gameObject, Vector3.one, unEase);

        if (blocktype == BlockType.types.Heavy)
            ScaleText(inventory.uiHeavyImg.gameObject, Vector3.one * scaleSize, selectEase);

        else
            ScaleText(inventory.uiHeavyImg.gameObject, Vector3.one, unEase);

        if (blocktype == BlockType.types.Fluffy)
           ScaleText(inventory.uiFluffyImg.gameObject, Vector3.one * scaleSize, selectEase);

        else
            ScaleText(inventory.uiFluffyImg.gameObject, Vector3.one, unEase);
    }

    private void ScaleText(GameObject text, Vector3 scale, Ease ease)
    {
        text.transform.parent.GetComponent<RectTransform>().DOScale(scale, 0.3f).SetEase(ease);
    }
    */
    public void AimChangeColor()
    {
        spriteRenderer.sprite = blockPreFab.GetComponent<SpriteRenderer>().sprite;
    }

}
