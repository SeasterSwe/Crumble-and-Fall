using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

//This script keeps track of gameStates, timers, UI text and other UI.
public class GameState : MonoBehaviour
{
    [Header("gameState")]
    public static gameStates currentState;
    public enum gameStates { Build, StartFight, Fight, StartGameOver, GameOver};

    [Header("Indicators")]
    public TextMeshProUGUI uiGameTimeText;
    public TextMeshProUGUI uiGameStateText;



    [Header("BuildMode")]
    public string buildText = "Build time left ";
    public float buildTime = 30;
    private float buildTimeLeft;

    [Header("Fight")]
    public string fightText = "FIGHT";
    public float RoundTime = 60;
    private float roundTimeLeft;

    [Header("TMP")]
    //TODO : Move to game over;
    public Canvas canvas;
    public ElevationCheck hightOne;
    public ElevationCheck hightTwo;
    public GameOverUIMaster GameOverPreFab;

    public Ease easein;
    public Ease easeOut;

    // Start is called before the first frame update
    void Start()
    {
        buildTimeLeft = buildTime;
        currentState = gameStates.Build;
    }

    // Update is called once per frame
    void Update()
    {
        //Enter function depending of gameStates
        switch(currentState)
        {
            case gameStates.Build:
                {
                    BuildMode();
                }
                break;

            case gameStates.StartFight:
                {
                    StartFight();
                }
                break;

            case gameStates.Fight:
                {
                    Fighting();
                }
                break;

            case gameStates.StartGameOver:
                {
                    StartGameOver(hightOne.towerHight, hightTwo.towerHight);
                }
                break;
            case gameStates.GameOver:
                {
                    GameOver();
                }
                break;

            default:
                {
                    currentState = gameStates.Build;
                }
                break;
        }
    }

    

    //BUILDMODE
    void BuildMode()
    {
        buildTimeLeft -= Time.deltaTime;
        if (buildTimeLeft > 0)
        {
            uiGameStateText.text = buildText;
            uiGameTimeText.text = buildTimeLeft.ToString("F0").PadLeft(2,'0');
            ScaleText();
        }
        else
        {
            TogglegameStatesForward();
        }
    }

    bool active = false;
    void ScaleText()
    {
        if(!active)
        {
            active = true;
            uiGameTimeText.rectTransform.DOScale(Vector3.one * 2.5f, 0.5f).SetEase(easein).OnComplete(ResetText);
        }
    }

    void ResetText()
    {
        uiGameTimeText.rectTransform.DOScale(Vector3.one * 2.2f, 0.5f).SetEase(easeOut).OnComplete(ActiveFalse);
    }
    void ActiveFalse()
    {
        active = false;
    }

    //STARTFIGHT
    void StartFight()
    {
        StartCoroutine(fightTextDisapear());
        uiGameStateText.text = fightText;
        roundTimeLeft = RoundTime;
        TogglegameStatesForward();
    }
    IEnumerator fightTextDisapear()
    {
        yield return new WaitForSeconds(3);
        uiGameStateText.text = null;
    }

    //FIGHTING
    void Fighting()
    {
        roundTimeLeft -= Time.deltaTime;
        uiGameTimeText.text = roundTimeLeft.ToString("F0").PadLeft(2,'0');    

        if (roundTimeLeft < 0)
        {
            TogglegameStatesForward();
            //switchStateTo(gameStates.StartGameOver);
        }
    }

    public void StartGameOver(float scoreOne, float scoreTwo)
    {
        Instantiate(GameOverPreFab, canvas.transform.position, canvas.transform.rotation, canvas.transform).GameOver(scoreOne, scoreTwo);
        switchStateTo(gameStates.GameOver);
        print("StartGameOver");
    }

    //GAMEOVER
    void GameOver()
    {
        print("GameOver");
    }
   
    //TOGGLE gameStates
    public static void TogglegameStatesForward()
    {
        switch (currentState)
        {
            case gameStates.Build:
                {
                    currentState = gameStates.StartFight;
                }
                break;

            case gameStates.StartFight:
                {
                    currentState = gameStates.Fight;
                }
                break;

            case gameStates.Fight:
                {
                    currentState = gameStates.StartGameOver;
                }
                break;

            case gameStates.StartGameOver:
                {
                    currentState = gameStates.GameOver;
                }
                break;
            case gameStates.GameOver:
                {
                    currentState = gameStates.Build;
                }
                break;

            default:
                {
                    currentState = gameStates.Build;
                }
                break;
        }
    }

    
    public static void switchStateTo(gameStates newState)
    {
        switch (newState)
        {
            case gameStates.Build:
                {
                    currentState = gameStates.Build;
                }
                break;

            case gameStates.StartFight:
                {
                    currentState = gameStates.StartFight;
                }
                break;

            case gameStates.Fight:
                {
                    currentState = gameStates.Fight;
                }
            break;

            case gameStates.StartGameOver:
                {
                    currentState = gameStates.StartGameOver;
                }
                break;

            case gameStates.GameOver:
                {
                    currentState = gameStates.GameOver;
                }
            break;

            default:
                {
                    currentState = gameStates.Build;
                }
            break;
        }
    }
}
