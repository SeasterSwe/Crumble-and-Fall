using UnityEngine;

//This script adds a block to player inventory each intervall.
public class GenerateBlockToInventoryOverTime : MonoBehaviour
{

    [Header("Generate")]
    public bool generatorActive = true;
    public float timeBetweenGenerations = 10;
    private float timeLeftToNextGen;

    public Inventory inventory;

    // public enum blockType {red, green, blue};
    public BlockType.types currentBlock = BlockType.types.Fluffy;

    // Start is called before the first frame update
    void Start()
    {
        if (!inventory)
        {
            inventory = FindObjectOfType<Inventory>();
        }
        timeLeftToNextGen = timeBetweenGenerations;
    }

    // Update is called once per frame
    void Update()
    {
        if (generatorActive)
        {
            timeLeftToNextGen -= Time.deltaTime;
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
