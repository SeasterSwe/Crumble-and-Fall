using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHeightBonuses : MonoBehaviour
{
    Cannon cannon;
    public float maxVelBouns;
    private float bonusVelPerBlock;

    public float maxRotationSpeed = 1;
    private float bonusRotationSpeedPerBlock;

    private float maxHeight = 20;        

    private ElevationCheck elevationCheck;
    private float currentHeight;

    private void Start()
    {
        cannon = GetComponent<Cannon>();
        bonusVelPerBlock = maxVelBouns / maxHeight;
        bonusRotationSpeedPerBlock = maxRotationSpeed / maxHeight;
        elevationCheck = FindClosetElevationCheck.GetClosets(gameObject);
    }
    float currentTime;
    private void Update()
    {
        //currentHeight = elevationCheck.towerHight;
        //cannon.velBouns = Mathf.Clamp(Mathf.Round(bonusVelPerBlock * currentHeight), 0f, maxVelBouns);
        //cannon.bonunsRotationSpeed = Mathf.Clamp((bonusRotationSpeedPerBlock * currentHeight), 0, maxRotationSpeed);

        if (currentHeight != elevationCheck.towerHight)
        {
            currentHeight = elevationCheck.towerHight;
            cannon.velBouns = Mathf.Clamp(Mathf.Round(bonusVelPerBlock * currentHeight), 0f, maxVelBouns);
            cannon.bonunsRotationSpeed = Mathf.Clamp((bonusRotationSpeedPerBlock * currentHeight), 0, maxRotationSpeed);
        }
    }
}
