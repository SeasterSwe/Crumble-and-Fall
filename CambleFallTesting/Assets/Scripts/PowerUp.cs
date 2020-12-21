using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class PowerUp : MonoBehaviour
{
    public float restictValue;
    public bool leftElevation;
    private ElevationCheck elevationCheck;
    private Image img;
    public Color color;
    float height;
    private void Start()
    {
        if(leftElevation)
            elevationCheck = FindClosetElevationCheck.GetTheLeftOne();
        else
            elevationCheck = FindClosetElevationCheck.GetTheRighttOne();
       
        img = GetComponent<Image>();
        img.color = color; 
    }
    private void Update()
    {
        if (GameState.currentState == GameState.gameStates.StartFight || GameState.currentState == GameState.gameStates.Fight)
        {
            Destroy(gameObject.GetComponent<PowerUp>());
        }
        else
        {
            if (elevationCheck.towerHight > restictValue)
            {
                //print(elevationCheck.towerHight);
                img.color = Color.white;
            }
            else
                img.color = color;
        }
    }
}

