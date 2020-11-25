using UnityEngine;

//This script adds a block to player inventory each intervall.
public class GenerateBlockToInventoryOverTime : MonoBehaviour
{

    [Header("Generate")]
    public bool generatorActive = true;
    public float timeBetweenGenerations = 10;
    private float timeLeftToNextGen;

    public Inventory inventory;

    public enum blockType {red, green, blue};
    public blockType currentColor;

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
                inventory.UpdateUiText();
                timeLeftToNextGen = timeBetweenGenerations;
            }
        }
    }

    void GenerateBlockToInventory()
    {
        switch (currentColor)
        {
            case blockType.red:
                inventory.redCube++;
                break;

            case blockType.green:
                inventory.greenCube++;
                break;

            case blockType.blue:
                inventory.blueCube++;
                break;

            default:
                Debug.LogError("< color = red > Color does not exist : " + transform.name);
                break;
        }
    }

    void ToggleColor()
    {
        switch (currentColor)
        {
            case blockType.red: 
                currentColor = blockType.green; 
                break; 

            case blockType.green:
                currentColor = blockType.blue;
                break;

            case blockType.blue: 
                currentColor = blockType.red; 
                break; 

            default:
                currentColor = blockType.red;
                break;
        }
    }
}
