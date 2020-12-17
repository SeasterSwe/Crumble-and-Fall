using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RematchButton : MonoBehaviour
{
    RoundTracker roundTracker;
    public Sprite newRound;
    public Sprite rematch;
    private Sprite image;
    void Start()
    {
        if(rematch == null)
            rematch = GetComponent<Image>().sprite;

        roundTracker = GameObject.FindGameObjectWithTag("Finish").GetComponent<RoundTracker>();
        if (roundTracker.CheckIfWin())
            image = rematch;
        else
            image = newRound;

       GetComponent<Image>().sprite = image;
    }
    public void Button()
    {
        if(roundTracker.CheckIfWin())
        {
            Debug.LogWarning("ResetedGame");
            roundTracker.ResetStats();
        }
    }
}
