using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//TODO : Switch only
public class Inventory : MonoBehaviour
{
    [Header("Settings")]
    public float scrollSpeed = 5f;
    public Vector2 highPoint;
    public Vector2 spaceBetweenIcons;
    public Vector3 uiInvNumberOffsett = Vector3.one;

    public float selectedScale = 1.2f;
    public GameObject selectedBlock;

    [Header("Numbers")]
    public int numberOfSpeedys = 10;
    public int numberOfHeavys = 10;
    public int numberOfFluffys = 10;

    [Header("UI")]
    public Image uiSpeedyImg;
    public Image uiHeavyImg;
    public Image uiFluffyImg;

    public TextMeshProUGUI uiSpeedyTxt;
    public TextMeshProUGUI uiHeavyTxt;
    public TextMeshProUGUI uiFluffyTxt;

    [Header("BlockPreFabs")]
    public GameObject speedyBlock;
    public GameObject heavyBlock;
    public GameObject fluffyBlock;

    private void Start()
    {
        numberOfFluffys = GameStats.startBlocks;
        numberOfHeavys = GameStats.startBlocks;
        numberOfSpeedys = GameStats.startBlocks;

        selectedBlock = heavyBlock;
        UpdateUiText();

        GetSpaceBetweenIcons();
        GetHighPoint();
    }

    private void GetSpaceBetweenIcons()
    {
        float spaceBetweenIconsOne = Mathf.Abs(uiFluffyImg.rectTransform.position.y - uiSpeedyImg.rectTransform.position.y);
        float spaceBetweenIconsTwo = Mathf.Abs(uiFluffyImg.rectTransform.position.y - uiHeavyImg.rectTransform.position.y);
        if (spaceBetweenIconsOne > spaceBetweenIconsTwo)
        {
            spaceBetweenIconsOne = spaceBetweenIconsTwo;
        }
        spaceBetweenIcons = new Vector2(0, spaceBetweenIconsOne);
        print(spaceBetweenIcons);
    }

    private void GetHighPoint()
    {
        highPoint = -Vector2.one * Mathf.Infinity;
        if (highPoint.y < uiFluffyImg.rectTransform.position.y)
        {
            highPoint = uiFluffyImg.rectTransform.position;
        }
        if (highPoint.y < uiHeavyImg.rectTransform.position.y)
        {
            highPoint = uiHeavyImg.rectTransform.position;
        }
        if (highPoint.y < uiSpeedyImg.rectTransform.position.y)
        {
            highPoint= uiSpeedyImg.rectTransform.position;
        }
    }
    
        public GameObject TakeActiveBlockFromInventory()
    {
        RemoveFromInventory(selectedBlock.GetComponent<BlockType>().type);
        return (selectedBlock);
    }

    public void TogggleBlock()
    {
        //Hej Cristian!!
        //Den här funktionene sätter "selectedBlock" och scalar inventory

        //Först nollställ alla UI bilders scala
        uiFluffyImg.rectTransform.localScale = Vector3.one;
        uiHeavyImg.rectTransform.localScale = Vector3.one;
        uiSpeedyImg.rectTransform.localScale = Vector3.one;

        SetSemiTransperent(uiFluffyImg);
        SetSemiTransperent(uiHeavyImg);
        SetSemiTransperent(uiSpeedyImg);
        
        //Kolla vilken typ det är, set valt block, skala ui bild
        BlockType.types type = selectedBlock.GetComponent<BlockType>().type;

        switch (type)
        {
            case BlockType.types.Fluffy:
                {
                    selectedBlock = speedyBlock;
                    uiSpeedyImg.rectTransform.localScale = Vector3.one * selectedScale;
                    SetNonTransperent(uiSpeedyImg);
                    uiSpeedyImg.rectTransform.position = highPoint;
                    uiHeavyImg.rectTransform.position = highPoint - spaceBetweenIcons;
                    uiFluffyImg.rectTransform.position = highPoint - spaceBetweenIcons * 2;
                }
                break;

            case BlockType.types.Heavy:
                {
                    selectedBlock = fluffyBlock;
                    uiFluffyImg.rectTransform.localScale = Vector3.one * selectedScale;
                    SetNonTransperent(uiFluffyImg);
                    uiFluffyImg.rectTransform.position = highPoint;
                    uiSpeedyImg.rectTransform.position = highPoint - spaceBetweenIcons;
                    uiHeavyImg.rectTransform.position = highPoint - spaceBetweenIcons * 2;
                }
                break;


            case BlockType.types.Speedy:
                {
                    selectedBlock = heavyBlock;
                    uiHeavyImg.rectTransform.localScale = Vector3.one * selectedScale;
                    SetNonTransperent(uiHeavyImg);
                    uiHeavyImg.rectTransform.position = highPoint;
                    uiFluffyImg.rectTransform.position = highPoint - spaceBetweenIcons;
                    uiSpeedyImg.rectTransform.position = highPoint - spaceBetweenIcons * 2;
                }
                break;

            default:
                {
                    Debug.LogError("Block type does not exist in inventory");
                    selectedBlock = speedyBlock;
                    uiSpeedyImg.rectTransform.localScale = Vector3.one * selectedScale;
                    uiSpeedyImg.rectTransform.position = highPoint;
                    uiHeavyImg.rectTransform.position = highPoint + spaceBetweenIcons;
                    uiFluffyImg.rectTransform.position = highPoint + spaceBetweenIcons * 2;
                }
                break;
        }
    }

    void SetSemiTransperent(Image img)
    {
        Color col = img.color;
        col.a = 0.5f;
        img.color = col;
    }

    void SetNonTransperent(Image img)
    {
        Color col = img.color;
        col.a = 1;
        img.color = col;
    }

    public Sprite GetIconFromSelection()
    {
        BlockType.types type = selectedBlock.GetComponent<BlockType>().type;
        switch (type)
        {
            case BlockType.types.Fluffy:
                {
                    return (uiFluffyImg.sprite);
                }
                break;

            case BlockType.types.Heavy:
                {
                    return (uiHeavyImg.sprite);
                }
                break;


            case BlockType.types.Speedy:
                {
                    return (uiSpeedyImg.sprite);
                }
                break;

            default:
                {
                    Debug.LogError("Block type does not exist in inventory");
                    return (uiFluffyImg.sprite);
                }
                break;
        }
    }


    public void AddToInventory(BlockType.types type, int amount)
    {
        //Add a "amount" of blocks "Type" to inventory
        //Set ui to normal if inventory is larger then 0

        switch (type)
        {
            case BlockType.types.Speedy:
                {
                    numberOfSpeedys += amount;
                    if (numberOfSpeedys > 0)
                    {
                        uiSpeedyTxt.color = Color.white;
                        uiSpeedyImg.color = Color.white;
                        if (selectedBlock != speedyBlock)
                            SetSemiTransperent(uiSpeedyImg);
                    }
                    UpdateUiText();
                }
                break;

            case BlockType.types.Heavy:
                {
                    numberOfHeavys += amount;
                    if (numberOfHeavys > 0)
                    {
                        uiFluffyTxt.color = Color.white;
                        uiFluffyImg.color = Color.white;
                        if (selectedBlock != fluffyBlock)
                            SetSemiTransperent(uiFluffyImg);
                    }
                    UpdateUiText();
                }
                break;

            case BlockType.types.Fluffy:
                {
                    numberOfFluffys += amount;
                    if (numberOfFluffys > 0)
                    {
                        uiHeavyTxt.color = Color.white;
                        uiHeavyImg.color = Color.white;
                        if (selectedBlock != heavyBlock)
                            SetSemiTransperent(uiHeavyImg);
                    }
                    UpdateUiText();
                }
                break;


            default:
                Debug.Log("Error : Blockcolor does not exist " + transform.name);
                break;
        }
    }

    public void RemoveFromInventory(BlockType.types type)
    {
        //Remove blockType from inventory
        //If inventory for requested type is smaller then 0, set ui to red
        switch (type)
        {
            case BlockType.types.Speedy:
                {
                    numberOfSpeedys--;
                    if (numberOfSpeedys < 1)
                    {
                        uiSpeedyTxt.color = Color.red;
                        uiSpeedyImg.color = Color.red;
                    }
                }
                break;

            case BlockType.types.Heavy:
                {
                    numberOfFluffys--;
                    if (numberOfFluffys < 1)
                    {
                        uiHeavyTxt.color = Color.red;
                        uiHeavyImg.color = Color.red;
                    }
                }
                break;

            case BlockType.types.Fluffy:
                {
                    numberOfHeavys--;
                    if (numberOfHeavys < 1)
                    {
                        uiFluffyTxt.color = Color.red;
                        uiFluffyImg.color = Color.red;
                    }
                }
                break;

            default:
                {
                    Debug.Log("Error : Blockcolor does not exist " + transform.name);
                }
                break;
        }
        UpdateUiText();
    }

    public bool SelectedBlockIsInInventory()
    {
        //Check if their is a egnuf of "selectedBlock" in inventory
        //If inventory for requested type is larger then 0, return true else false
        BlockType.types type = selectedBlock.GetComponent<BlockType>().type;
        switch (type)
        {
            case BlockType.types.Speedy:
                {
                    if (numberOfSpeedys > 0)
                    {
                        return (true);
                    }
                    else
                    {
                        return (false);
                    }
                }
            case BlockType.types.Heavy:
                {
                    if (numberOfFluffys > 0)
                    {
                        return (true);
                    }
                    else
                    {
                        return (false);
                    }
                }
            case BlockType.types.Fluffy:
                {
                    if (numberOfHeavys > 0)
                    {
                        return (true);
                    }
                    else
                    {
                        return (false);
                    }
                }
            default:
                {
                    Debug.Log("ErrorCubeDoesntExist");
                    return (false);
                }
        }
    }

    public bool CheckInventoryFor(BlockType.types type)
    {
        //Check if their is a block of "type" in inventory
        //If inventory for requested type is larger then 0, return true else false

        switch (type)
        {
            case BlockType.types.Fluffy:
                {
                    if (numberOfFluffys > 0)
                    {
                        return (true);
                    }
                    else
                    {
                        return (false);
                    }
                }
                break;

            case BlockType.types.Heavy:
                {
                    if (numberOfHeavys > 0)
                    {
                        return (true);
                    }
                    else
                    {
                        return (false);
                    }
                }
                break;

            case BlockType.types.Speedy:
                {
                    if (numberOfSpeedys > 0)
                    {
                        return (true);
                    }
                    else
                    {
                        return (false);
                    }
                }
                break;

            default:
                {
                    Debug.LogError("No such Type in Inventory");
                    return false;
                }
                break;

        }
    }

    public void UpdateUiText()
    {
        uiSpeedyTxt.text = numberOfSpeedys.ToString().PadLeft(2, '0');
        uiHeavyTxt.text = numberOfFluffys.ToString().PadLeft(2, '0');
        uiFluffyTxt.text = numberOfHeavys.ToString().PadLeft(2, '0');

        uiSpeedyTxt.rectTransform.position = uiSpeedyImg.rectTransform.position + uiInvNumberOffsett;
        uiHeavyTxt.rectTransform.position = uiHeavyImg.rectTransform.position + uiInvNumberOffsett;
        uiFluffyTxt.rectTransform.position = uiFluffyImg.rectTransform.position + uiInvNumberOffsett;
    }
}