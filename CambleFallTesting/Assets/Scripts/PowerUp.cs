using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{

   public ElevationCheck evCheck;
   public Blockbuilder bbCheck;
   float heightPercantage;
   public float powerUpActivate = 0.5f;
    public string text;

    void Update()
    {
        GetHeightPercentage();

        if (GameState.currentState == GameState.gameStates.Fight)
        {
            GetComponent<Image>().enabled = true; 
            if (transform.GetChild(0) != null)
                transform.GetChild(0).gameObject.SetActive(true);

        }
        else
        {
            GetComponent<Image>().enabled = false;
            if (transform.GetChild(0) != null)
                transform.GetChild(0).gameObject.SetActive(false);
        }

        text = (heightPercantage * 10).ToString("f0").PadLeft(2,'0');
    }

    public void GetHeightPercentage()
    {
        heightPercantage = evCheck.towerHight / bbCheck.maxHeight;
    }
}
