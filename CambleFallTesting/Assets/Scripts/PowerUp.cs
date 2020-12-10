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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetHeightPercentage();

        if (heightPercantage > powerUpActivate)
        {
            GetComponent<Image>().enabled = true;

        }
        else
        {
            GetComponent<Image>().enabled = false;
        }

        text = (heightPercantage * 10).ToString("f0").PadLeft(2,'0');
    }

    public void GetHeightPercentage()
    {
        heightPercantage = evCheck.towerHight / bbCheck.maxHeight;
    }
}
