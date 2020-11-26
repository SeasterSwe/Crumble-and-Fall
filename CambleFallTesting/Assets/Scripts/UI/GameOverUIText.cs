using UnityEngine;
using TMPro;
public class GameOverUIText : MonoBehaviour
{
    void SetText(string txt)
    {
        GetComponent<TextMeshProUGUI>().text = txt;
    }

    public void PlayerWin(int scorePLOne, int scorePLTwo)
    {
        if(scorePLOne < scorePLTwo)
        {
            SetText("Player2 Wins!\nScore : " + scorePLTwo);
        }
        else if(scorePLOne > scorePLTwo)
        {
            SetText("Player1 Wins!\nScore : " + scorePLOne);
        }
        else
        {
            SetText("Draw!\nScore : " + scorePLTwo);
        }
    }
}
