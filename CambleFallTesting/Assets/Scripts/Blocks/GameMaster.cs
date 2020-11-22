using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//This script keeps track of Gamestate, timers, UI text and other UI.
public class GameMaster : MonoBehaviour
{
    [Header("GameState")]
    public TextMeshProUGUI uiCountDown;
    public enum gameState { Build, StartFight, Fight, GameOver };
    public gameState currentGameState;

    [Header("BuildMode")]
    public string buildText = "Build time left ";
    public float buildTime = 30;
    private float buildTimeLeft;

    [Header("Fight")]
    public string fightText = "Fight /n TimeLeft ";
    public float RoundTime = 60;
    private float roundTimeLeft;

    public GameObject cannonPreFab;
    public int hpPlayerOne = 5;
    public int hpPlayerTwo = 5;

    public IndicatorBar firePowerPLOne;
    public IndicatorBar firePowerPLTwo;

    public Lancher lancherPLOne;
    public Lancher lancherPLTwo;

    [Header("HightMeters")]
    public IndicatorBar hightMeterOne;
    public IndicatorBar hightMeterTwo;

    public BlockBuilder blockBuilderOne;
    public BlockBuilder blockBuilderTwo;

    // Start is called before the first frame update
    void Start()
    {
        if(uiCountDown == null)
        {
            uiCountDown = FindObjectOfType<TextMeshProUGUI>();
        }
        buildTimeLeft = buildTime;
        
        if(blockBuilderOne == null || blockBuilderTwo == null)
        {
            BlockBuilder[] blockBuilders = FindObjectsOfType<BlockBuilder>();
            foreach(BlockBuilder b in blockBuilders)
            {
                if(b.transform.position.x < 0) {
                    blockBuilderOne = b;
                }
                else
                {
                    blockBuilderTwo = b;
                }
            }
        }

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

    void UpdadeUIMeters()
    {
        hightMeterOne.UpdateValue(blockBuilderOne.towerHight);
        hightMeterTwo.UpdateValue(blockBuilderTwo.towerHight);
    }
    void BuildMode()
    {
        buildTimeLeft -= Time.deltaTime;
        if (buildTimeLeft > 0)
        {
            uiCountDown.text = buildText + buildTimeLeft.ToString("F0").PadLeft(2,'0');
            UpdadeUIMeters();
        }
        else
        {
            ToggleGameStateForward();
        }
    }
    void StartFight(){

        uiCountDown.text = fightText;
        roundTimeLeft = RoundTime;
        //Spawn cannons
        SpawnALancher(1);
        SpawnALancher(2);
        ToggleGameStateForward();
    }

    void SpawnALancher(int pl)
    {
        Vector3 spawnPoint; 
        Transform lancherParent; 
        if (pl == 1)
        {
            spawnPoint = blockBuilderOne.highestBlock.transform.position + Vector3.up;
            lancherParent = blockBuilderOne.highestBlock.transform;
        }
        else
        {
            spawnPoint = blockBuilderTwo.highestBlock.transform.position + Vector3.up;
            lancherParent = blockBuilderTwo.highestBlock.transform;
        }

        GameObject lancher = Instantiate(cannonPreFab, spawnPoint, Quaternion.identity, lancherParent);
        Lancher lancherScript = lancher.GetComponent<Lancher>();
        lancherScript.Player = pl;
        lancherScript.gm = this;

        if (pl == 1)
        {
            lancherScript.firePowerUI = firePowerPLOne;
            lancherPLOne = lancherScript;
        }
        else
        {
            lancherScript.firePowerUI = firePowerPLTwo;
            lancherPLTwo = lancherScript;
        }
            
    }

    void Fighting()
    {
        roundTimeLeft -= Time.deltaTime;
        uiCountDown.text = fightText + roundTimeLeft.ToString("F0").PadLeft(2,'0');

        UpdadeUIMeters();

        if(lancherPLOne == null)
        {
            SpawnALancher(1);
            hpPlayerOne--;
        }
        if(lancherPLTwo == null)
        {
            SpawnALancher(2);
            hpPlayerTwo--;
        }


        if (Input.GetKeyDown("k") || roundTimeLeft < 0)
        {
            switchStateTo(gameState.GameOver);
        }
    }

    void GameOver()
    {
        uiCountDown.text = "Game Over";
        print("GameOver");
    }


    void ToggleGameStateForward()
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
    void switchStateTo(gameState newState)
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
