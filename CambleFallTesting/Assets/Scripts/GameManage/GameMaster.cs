using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//This script keeps track of Gamestate, timers, UI text and other UI.
public class GameMaster : MonoBehaviour
{
    [Header("GameState")]
    public gameState currentGameState;
    public enum gameState { Build, StartFight, Fight, GameOver};

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
        //Enter function depending of gamestate
        switch(currentGameState)
        {
            case gameState.Build:
                {
                    BuildMode();
                }
                break;

            case gameState.StartFight:
                {
                    StartFight();
                }
                break;

            case gameState.Fight:
                {
                    Fighting();
                }
                break;

            case gameState.GameOver:
                {
                    GameOver();
                }
                break;

            default:
                {
                    currentGameState = gameState.Build;
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
            ToggleGameStateForward();
        }
    }

    //STARTFIGHT
    void StartFight()
    {
        uiGameInfoText.text = fightText;
        roundTimeLeft = RoundTime;
        ToggleGameStateForward();
    }
    
    //FIGHTING
    void Fighting()
    {
        roundTimeLeft -= Time.deltaTime;
        uiGameInfoText.text = fightText + roundTimeLeft.ToString("F0").PadLeft(2,'0');

        if(roundTimeLeft < 0)
        {
            switchStateTo(gameState.GameOver);
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
   
    //TOGGLE GAMESTATE
    public void ToggleGameStateForward()
    {
        switch (currentGameState)
        {
            case gameState.Build:
                {
                    currentGameState = gameState.StartFight;
                }
                break;

            case gameState.StartFight:
                {
                    currentGameState = gameState.Fight;
                }
                break;

            case gameState.Fight:
                {
                    currentGameState = gameState.GameOver;
                }
                break;

            case gameState.GameOver:
                {
                    currentGameState = gameState.Build;
                }
                break;

            default:
                {
                    currentGameState = gameState.Build;
                }
                break;
        }
    }

    
    public void switchStateTo(gameState newState)
    {
        switch (newState)
        {
            case gameState.Build:
                {
                    currentGameState = gameState.Build;
                }
                break;

            case gameState.StartFight:
                {
                    currentGameState = gameState.StartFight;
                }
                break;

            case gameState.Fight:
                {
                    currentGameState = gameState.Fight;
                }
            break;

            case gameState.GameOver:
                {
                    currentGameState = gameState.GameOver;
                }
            break;

            default:
                {
                    currentGameState = gameState.Build;
                }
            break;
        }
    }
}
