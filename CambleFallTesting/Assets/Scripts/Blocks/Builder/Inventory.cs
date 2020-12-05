using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    [Header("Numbers")]
    public int numberOfSpeedys = 10;
    public int numberOfHeavys = 10;
    public int numberOfFluffys = 10;

    [Header("UI")]
    public Image uiSpeedyImg;
    public Image uiHeavyImg;
    public Image uiFluffyImg;

    public TextMeshProUGUI uiSpeedyTxt;
    public TextMeshProUGUI uiFluffyTxt;
    public TextMeshProUGUI uiHeavyTxt;

    public void AddToInventory(BlockType.types type, int amount)
    {
        if (type == BlockType.types.Speedy)
        {
            numberOfSpeedys+= amount;
            if (numberOfSpeedys > 0)
            {
                uiSpeedyTxt.color = Color.white;
                uiSpeedyImg.color = Color.white;
            }
        }

        else if (type == BlockType.types.Heavy)
        {
            numberOfFluffys+= amount;
            if (numberOfFluffys > 0)
            {
                uiHeavyTxt.color = Color.white;
                uiHeavyImg.color = Color.white;
            }
        }

        else if (type == BlockType.types.Fluffy)
        {
            numberOfHeavys+= amount;
            if (numberOfHeavys > 0)
            {
                uiFluffyTxt.color = Color.white;
                uiFluffyImg.color = Color.white;
            }
        }

        else
        {
            Debug.Log("Error : Blockcolor does not exist " + transform.name);
        }

        UpdateUiText();
    }

    public void RemoveFromInventory(BlockType.types type)
    {
        if (type == BlockType.types.Speedy)
        {
            numberOfSpeedys--;
            if(numberOfSpeedys < 1)
            {
                uiSpeedyTxt.color = Color.red;
                uiSpeedyImg.color = Color.red;
            }
        }

        else if (type == BlockType.types.Heavy)
        {
            numberOfFluffys--;
            if (numberOfFluffys < 1)
            {
                uiHeavyTxt.color = Color.red;
                uiHeavyImg.color = Color.red;
            }
        }

        else if (type == BlockType.types.Fluffy)
        {
            numberOfHeavys--;
            if (numberOfHeavys < 1)
            {
                uiFluffyTxt.color = Color.red;
                uiFluffyImg.color = Color.red;
            }
        }

        else
        {
            Debug.Log("Error : Blockcolor does not exist " + transform.name);
        }

        UpdateUiText();
    }

    public bool CheckInventory(BlockType.types type)
    {
        if (type == BlockType.types.Speedy)
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
        else if (type == BlockType.types.Heavy)
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
        else if (type == BlockType.types.Fluffy)
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
        else
        {
            Debug.Log("ErrorCubeDoesntExist");

            return (false);
        }

    }

    public void UpdateUiText()
    {
        uiSpeedyTxt.text = numberOfSpeedys.ToString().PadLeft(2, '0');
        uiFluffyTxt.text = numberOfHeavys.ToString().PadLeft(2, '0');
        uiHeavyTxt.text = numberOfFluffys.ToString().PadLeft(2, '0');
    }
}