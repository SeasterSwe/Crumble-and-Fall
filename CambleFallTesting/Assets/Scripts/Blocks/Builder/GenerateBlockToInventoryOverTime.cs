using UnityEngine;

//This script adds a block to player inventory each intervall.
public class GenerateBlockToInventoryOverTime : MonoBehaviour
{

    [Header("Generate")]
    public bool generatorActive = true;
    public float timeBetweenGenerations = 10;

    public float minBonus = 1;
    public float maxBonus = 10;
    public float bonus;

    private float timeLeftToNextGen;

    public Inventory inventory;

    // public enum blockType {red, green, blue};
    public BlockType.types currentBlock = BlockType.types.Fluffy;

    public ElevationCheck evCheck;
    public Blockbuilder blockbuilder;
    

    // Start is called before the first frame update
    void Start()
    {
        if (!inventory)
        {
            inventory = GetComponent<Inventory>();
        }

        ElevationCheck[] evCheckers = FindObjectsOfType<ElevationCheck>();

        foreach(ElevationCheck evc in evCheckers)
        {
            if (GetComponent<RectTransform>().localPosition.x < 0 && evc.transform.position.x < 0)
                evCheck = evc;

            if (GetComponent<RectTransform>().localPosition.x > 0 && evc.transform.position.x > 0)
                evCheck = evc;
        }


        Blockbuilder[] bbs = FindObjectsOfType<Blockbuilder>();

        foreach(Blockbuilder bs in bbs)
        {
            if (GetComponent<RectTransform>().localPosition.x < 0 && bs.transform.position.x < 0)
                blockbuilder = bs;

            if (GetComponent<RectTransform>().localPosition.x > 0 && bs.transform.position.x > 0)
                blockbuilder = bs;
        }

        timeLeftToNextGen = timeBetweenGenerations;
    }

    void GetBonus()
    {
        bonus = evCheck.towerHight / blockbuilder.maxHeight;
    }
    // Update is called once per frame
    void Update()
    {
        GetBonus();


        if (GameState.currentState == GameState.gameStates.Fight)
        {
            timeLeftToNextGen -= Mathf.Lerp(minBonus, maxBonus, bonus) * Time.deltaTime ;
            if (timeLeftToNextGen < 0)
            {
                GenerateBlockToInventory();
                ToggleColor();
                //inventory.UpdateUiText();
                timeLeftToNextGen = timeBetweenGenerations;
            }
        }
    }

    void GenerateBlockToInventory()
    {
        switch (currentBlock)
        {
            case BlockType.types.Fluffy:
                inventory.AddToInventory(BlockType.types.Fluffy, 1);
                break;

            case BlockType.types.Heavy:
                inventory.AddToInventory(BlockType.types.Heavy, 1);
                break;

            case BlockType.types.Speedy:
                inventory.AddToInventory(BlockType.types.Speedy, 1);
                break;

            default:
                Debug.LogError("< color = red > Color does not exist : " + transform.name);
                break;
        }
    }

    void ToggleColor()
    {
        switch (currentBlock)
        {
            case BlockType.types.Fluffy:
                currentBlock = BlockType.types.Heavy;
                break;

            case BlockType.types.Heavy:
                currentBlock = BlockType.types.Speedy;
                break;

            case BlockType.types.Speedy:
                currentBlock = BlockType.types.Fluffy;
                break;

            default:
                currentBlock = BlockType.types.Fluffy;
                break;
        }
    }
}
