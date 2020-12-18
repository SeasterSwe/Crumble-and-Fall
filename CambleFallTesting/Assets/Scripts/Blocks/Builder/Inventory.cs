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
    public Vector2 lowestPoint;
    public Vector2 spaceBetweenIcons;
    public Vector3 uiInvNumberOffsett = Vector3.one;

    public float selectedScale = 1;
    public float midScale = 0.75f;
    public float smallScale = 0.5f;
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
        TogggleBlock();
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
    }
    private void GetHighPoint()
    {
        lowestPoint = Vector2.one * Mathf.Infinity;
        if (lowestPoint.y > uiFluffyImg.rectTransform.position.y)
        {
            lowestPoint = uiFluffyImg.rectTransform.position;
        }
        if (lowestPoint.y > uiHeavyImg.rectTransform.position.y)
        {
            lowestPoint = uiHeavyImg.rectTransform.position;
        }
        if (lowestPoint.y > uiSpeedyImg.rectTransform.position.y)
        {
            lowestPoint = uiSpeedyImg.rectTransform.position;
        }
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

                    SetFirst(uiSpeedyImg, uiSpeedyTxt);
                    SetSecond(uiHeavyImg, uiHeavyTxt);
                    SetThird(uiFluffyImg, uiFluffyTxt);
                }
                break;

            case BlockType.types.Heavy:
                {
                    selectedBlock = fluffyBlock;

                    SetFirst(uiFluffyImg, uiFluffyTxt);
                    SetSecond(uiSpeedyImg, uiSpeedyTxt);
                    SetThird(uiHeavyImg, uiHeavyTxt);
                }
                break;


            case BlockType.types.Speedy:
                {
                    selectedBlock = heavyBlock;

                    SetFirst(uiHeavyImg, uiHeavyTxt);
                    SetSecond(uiFluffyImg, uiFluffyTxt);
                    SetThird(uiSpeedyImg, uiSpeedyTxt);
                }
                break;

            default:
                {
                    Debug.LogError("Block type does not exist in inventory");
                    selectedBlock = speedyBlock;

                    SetFirst(uiSpeedyImg, uiSpeedyTxt);
                    SetSecond(uiHeavyImg, uiHeavyTxt);
                    SetThird(uiFluffyImg, uiFluffyTxt);
                }
                break;
        }
    }
    private void SetFirst(Image img, TextMeshProUGUI txt)
    {
        img.rectTransform.position = lowestPoint;
        img.rectTransform.localScale = Vector3.one * selectedScale;
        SetNonTransperent(img);

        txt.rectTransform.position = img.rectTransform.position + uiInvNumberOffsett;
        txt.color = Color.white;
    }
    private void SetSecond(Image img, TextMeshProUGUI txt)
    {
        img.rectTransform.position = lowestPoint + spaceBetweenIcons * midScale;
        img.rectTransform.localScale = Vector3.one * midScale;
        SetSemiTransperent(img);

        txt.rectTransform.position = img.rectTransform.position + uiInvNumberOffsett * midScale;
       // txt.color = Color.clear;
    }
    private void SetThird(Image img, TextMeshProUGUI txt)
    {
        img.rectTransform.position = lowestPoint + spaceBetweenIcons * midScale + spaceBetweenIcons * smallScale;
        img.rectTransform.localScale = Vector3.one * smallScale;
        SetSemiTransperent(img);

        txt.rectTransform.position = img.rectTransform.position + uiInvNumberOffsett * smallScale;
       // txt.color = Color.clear;
    }
    private void SetSemiTransperent(Image img)
    {
        Color col = img.color;
        col.a = 0.5f;
        img.color = col;
    }
    private void SetNonTransperent(Image img)
    {
        Color col = img.color;
        col.a = 1;
        img.color = col;
    }
    public void UpdateUiText()
    {
        uiSpeedyTxt.text = numberOfSpeedys.ToString().PadLeft(2, '0');
        uiHeavyTxt.text = numberOfHeavys.ToString().PadLeft(2, '0');
        uiFluffyTxt.text = numberOfFluffys.ToString().PadLeft(2, '0');
    }


    public GameObject TakeActiveBlockFromInventory()
    {
        RemoveFromInventory(selectedBlock.GetComponent<BlockType>().type);
        return (selectedBlock);
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
                        uiSpeedyImg.color = Color.white;
                        if (selectedBlock != speedyBlock)
                            SetSemiTransperent(uiSpeedyImg);
                        else
                            uiSpeedyTxt.color = Color.white;
                    }
                    UpdateUiText();
                }
                break;

            case BlockType.types.Heavy:
                {
                    numberOfHeavys += amount;
                    if (numberOfHeavys > 0)
                    {
                        uiHeavyImg.color = Color.white;
                        if (selectedBlock != heavyBlock)
                            SetSemiTransperent(uiHeavyImg);
                        else
                            uiHeavyTxt.color = Color.white;
                    }
                    UpdateUiText();
                }
                break;

            case BlockType.types.Fluffy:
                {
                    numberOfFluffys += amount;
                    if (numberOfFluffys > 0)
                    {
                        uiFluffyImg.color = Color.white;
                        if (selectedBlock != fluffyBlock)
                            SetSemiTransperent(uiFluffyImg);
                        else
                            uiFluffyTxt.color = Color.white;
                    }
                    UpdateUiText();
                }
                break;


            default:
                Debug.Log("Error : Blockcolor does not exist " + transform.name);
                break;
        }
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
                    numberOfHeavys--;
                    if (numberOfHeavys < 1)
                    {
                        uiHeavyTxt.color = Color.red;
                        uiHeavyImg.color = Color.red;
                    }
                }
                break;

            case BlockType.types.Fluffy:
                {
                    numberOfFluffys--;
                    if (numberOfFluffys < 1)
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
                    if (numberOfHeavys > 0)
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
                    if (numberOfFluffys > 0)
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

}