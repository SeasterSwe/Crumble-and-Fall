using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//This script keeps track of gameStates, timers, UI text and other UI.
public class GameState : MonoBehaviour
{
    [Header("gameState")]
    public static gameStates currentState;
    public enum gameStates { Build, StartFight, Fight, StartGameOver, GameOver};

    [Header("Indicators")]
    public TextMeshProUGUI uiGameInfoText;

    

    [Header("BuildMode")]
    public string buildText = "Build time left ";
    public float buildTime = 30;
    private float buildTimeLeft;

    [Header("Fight")]
    public string fightText = "Fight \n TimeLeft ";
    public float RoundTime = 60;
    private float roundTimeLeft;

    [Header("TMP")]
    //TODO : Move to game over;
    public Canvas canvas;
    public ElevationCheck hightOne;
    public ElevationCheck hightTwo;
    public GameOverUIMaster GameOverPreFab;


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
            uiGameInfoText.text = buildText + buildTimeLeft.ToString("F0").PadLeft(2,'0');
        }
        else
        {
            TogglegameStatesForward();
        }
    }

    //STARTFIGHT
    void StartFight()
    {
        uiGameInfoText.text = fightText;
        roundTimeLeft = RoundTime;
        TogglegameStatesForward();
    }
    
    //FIGHTING
    void Fighting()
    {
        roundTimeLeft -= Time.deltaTime;
        uiGameInfoText.text = fightText + roundTimeLeft.ToString("F0").PadLeft(2,'0');

        if(roundTimeLeft < 0)
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
