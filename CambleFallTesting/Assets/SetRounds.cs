using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRounds : MonoBehaviour
{
    public int rounds = 3;

    public void SelectNumberOfRounds()
    {
        GameStats.amountOfRounds = rounds;
    }
}
