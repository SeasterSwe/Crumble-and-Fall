using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int greenCube = 10;
    public int redCube = 10;
    public int blueCube = 10;

    public TextMeshProUGUI uiGreenCubes;
    public TextMeshProUGUI uiRedCubes;
    public TextMeshProUGUI uiBlueCubes;

    public void RemoveFromInventory(string color)
    {
        if (color == "Green")
        {
            greenCube--;
        }

        else if (color == "Blue")
        {
            blueCube--;
        }

        else if (color == "Red")
        {
            redCube--;
        }

        else
        {
            Debug.Log("Error : Blockcolor does not exist " + transform.name);
        }

        UpdateUiText();
    }

    public bool CheckInventory(string color)
    {
        if (color == "Green")
        {
            if (greenCube > 0)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        else if (color == "Blue")
        {
            if (blueCube > 0)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        else if (color == "Red")
        {
            if (redCube > 0)
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
        uiGreenCubes.text = greenCube.ToString().PadLeft(2, '0');
        uiRedCubes.text = redCube.ToString().PadLeft(2, '0');
        uiBlueCubes.text = blueCube.ToString().PadLeft(2, '0');
    }
}
