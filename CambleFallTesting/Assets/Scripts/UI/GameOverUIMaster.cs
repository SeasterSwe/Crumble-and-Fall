using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIMaster : MonoBehaviour
{
    public float fadeTime;
    public void GameOver(int scorePlOne, int scorePlTwo)
    {
        GetComponentInChildren<FadeInOut>().FadeIn(true, fadeTime);
        GetComponentInChildren<GameOverUIText>().PlayerWin(scorePlOne, scorePlTwo);

    }
    // Start is called before the first frame update
    void Start()
    {
        //Warning : Test
        GameOver(2, 1);
    }
}
