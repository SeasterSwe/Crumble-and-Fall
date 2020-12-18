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
                text.text = "Player1 is victorious";
            else
                text.text = "Player2 is victorious";
        }
        else
        {
            if (roundTracker.leftPlayerWon)
                text.text = "Player1 won the round";
            else
                text.text = "Player2 won the round";
        }
            roundTracker.ActivateAnimations();
    }
}
