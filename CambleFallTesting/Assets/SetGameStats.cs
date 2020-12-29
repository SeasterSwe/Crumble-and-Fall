using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameStats : MonoBehaviour
{
    public float buildTime;
    public float cannonHealth;
    public float fightTime;
    public int startBlocks;
    public int rounds;
    public void SetStats()
    {
        Set(buildTime, cannonHealth, fightTime, startBlocks, rounds);
    }

    public void Set(float buildTime, float cannonHealth, float fightTime, int startBlocks, int rounds)
    {
        GameStats.buildTime = buildTime;
        GameStats.cannonStartHealth = cannonHealth;
        GameStats.fightTime = fightTime;
        GameStats.startBlocks = startBlocks;

        GameStats.amountOfRounds = rounds;
    }


}
