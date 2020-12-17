using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartAnimationsGameOver : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void OnEnable()
    {
        var roundTracker = GameObject.FindGameObjectWithTag("Finish").GetComponent<RoundTracker>();

        if(roundTracker.CheckIfWin())
        {
            if (roundTracker.leftPlayerWon)
                text.text = "Left Wins The Game";
            else
                text.text = "Right Wins The Game";
        }
        else
        {
            if (roundTracker.leftPlayerWon)
                text.text = "LeftWonRound";
            else
                text.text = "RightWonRound";
        }
            roundTracker.ActivateAnimations();
    }
}
