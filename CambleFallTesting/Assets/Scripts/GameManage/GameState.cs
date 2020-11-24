using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//This script keeps track of gameStates, timers, UI text and other UI.
public class GameState : MonoBehaviour
{
    [Header("gameState")]
    public static gameStates currentState;
    public enum gameStates { Build, StartFight, Fight, GameOver};

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

    // Start is called before the first frame update
    void Start()
    {
        buildTimeLeft = buildTime;
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
            switchStateTo(gameStates.GameOver);
        }
    }

    //GAMEOVER
    void GameOver()
    {
        /*
        //Win by highest tower
        if(evPlOne.towerHight == evPlTwo.towerHight)
        {
            uiGameInfoText.text = "Game Over\nDraw";
        }
        else if(evPlOne.towerHight < evPlTwo.towerHight)
        {
            uiGameInfoText.text = "Game Over\nPlayer 2 Wins";
        }
        else
        {
            uiGameInfoText.text = "Game Over\nPlayer 1 Wins";
        }
        */
        print("GameOver");
    }
   
    //TOGGLE gameStates
    public void TogglegameStatesForward()
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

    
    public void switchStateTo(gameStates newState)
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
