using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIMaster : MonoBehaviour
{
    public float fadeTime;
    public void GameOver(float scorePlOne, float scorePlTwo)
    {
        //GetComponentInChildren<FadeInOut>().FadeIn(true, fadeTime);
       GetComponentInChildren<GameOverUIText>().PlayerWin(scorePlOne, scorePlTwo);

    }
}
