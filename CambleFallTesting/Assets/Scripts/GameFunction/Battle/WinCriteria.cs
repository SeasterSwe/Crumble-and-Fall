﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCriteria : MonoBehaviour
{

    public GameState gS;

    public CannonHealth hpPlOne;
    public CannonHealth hpPlTwo;

    //public ElevationCheck hightPlOne;
    //public ElevationCheck hightPlTwo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.currentState == GameState.gameStates.Fight)
        {
            if (hpPlOne.currentHeatlh <= 0 && /*hpPlOne.isActiveAndEnabled == true*/ /*||*/ hpPlTwo.currentHeatlh <= 0) /*&&*/ /*hpPlTwo.isActiveAndEnabled == true*/ // || hightPlOne.towerHight == 0 || hightPlTwo.towerHight == 0)
            {
                gS.StartGameOver(hpPlOne.currentHeatlh, hpPlTwo.currentHeatlh);
            }
        }


    }
}
