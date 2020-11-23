using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//This script keeps track of Gamestate, timers, UI text and other UI.
public class GameMaster : MonoBehaviour
{
    [Header("GameState")]
    public TextMeshProUGUI uiGameInfoText;
    public enum gameState { Build, StartFight, Fight, GameOver};
    public gameState currentGameState;

    [Header("Indicators")]
    public ElevationCheck evPlOne;
    public ElevationCheck evPlTwo;

    public TextMeshProUGUI uiHpPLOne;
    public TextMeshProUGUI uiHpPLTwo;

    [Header("BuildMode")]
    public string buildText = "Build time left ";
    public float buildTime = 30;
    private float buildTimeLeft;

    public BlockBuilder blockBuilderOne;
    public BlockBuilder blockBuilderTwo;


    [Header("Fight")]
    public string fightText = "Fight \n TimeLeft ";
    public float RoundTime = 60;
    private float roundTimeLeft;

    public GameObject cannonPreFab;
    public int hpPlayerOne = 5;
    public int hpPlayerTwo = 5;

    public IndicatorBar firePowerPLOne;
    public IndicatorBar firePowerPLTwo;

    private Lancher lancherPLOne;
    private Lancher lancherPLTwo;

   

    // Start is called before the first frame update
    void Start()
    {
        buildTimeLeft = buildTime;
        /*
        if(uiGameInfoText == null)
        {
            uiGameInfoText = FindObjectOfType<TextMeshProUGUI>();
        }
       
        
        if(blockBuilderOne == null || blockBuilderTwo == null)
        {
            BlockBuilder[] blockBuilders = FindObjectsOfType<BlockBuilder>();
            foreach(BlockBuilder b in blockBuilders)
            {
                if(b.transform.position.x < 0) {
                    blockBuilderOne = b;
                    blockBuilderOne.playerNumber = 1;
                }
                else
                {
                    blockBuilderTwo = b;
                    blockBuilderTwo.playerNumber = 2;
                }
            }
        }
        */
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
            UpdateBuildText();
        }
        else
        {
            ToggleGameStateForward();
        }

        if (blockBuilderOne.numberOfBlocks <= 0 && blockBuilderTwo.numberOfBlocks <= 0)
        {
            ToggleGameStateForward();
        }
    }

    //STARTFIGHT
    void StartFight()
    {
        uiGameInfoText.text = fightText;
        roundTimeLeft = RoundTime;
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
            spawnPoint = evPlOne.highestBlock.transform.position + Vector3.up;
            lancherParent = evPlOne.highestBlock.transform;
        }
        else
        {
            spawnPoint = evPlTwo.highestBlock.transform.position + Vector3.up;
            lancherParent = evPlTwo.highestBlock.transform;
        }

        GameObject lancher = Instantiate(cannonPreFab, spawnPoint, Quaternion.identity, lancherParent);
        Lancher lancherScript = lancher.GetComponent<Lancher>();
        lancherScript.Player = pl;
        lancherScript.gm = this;

        if (pl == 1)
        {
            lancherScript.firePowerUI = firePowerPLOne;
            lancherScript.elevationScipt = evPlOne;
            lancherScript.hp = hpPlayerOne;
            lancherPLOne = lancherScript;
        }
        else
        {
            lancherScript.firePowerUI = firePowerPLTwo;
            lancherScript.elevationScipt = evPlTwo;
            lancherScript.hp = hpPlayerTwo;
            lancherPLTwo = lancherScript;
        }
    }

    //FIGHTING
    void Fighting()
    {
        roundTimeLeft -= Time.deltaTime;
        uiGameInfoText.text = fightText + roundTimeLeft.ToString("F0").PadLeft(2,'0');

        if(lancherPLOne == null)
        {
            hpPlayerOne--;
            if(hpPlayerOne > 0)
                SpawnALancher(1);
        }
        else
        {
            hpPlayerOne = lancherPLOne.hp;
        }
        if(lancherPLTwo == null)
        {
            hpPlayerTwo--;
            if(hpPlayerTwo > 0)
                SpawnALancher(2);
        }
        else
        {
            hpPlayerTwo = lancherPLTwo.hp;
        }

        UpdateFightText();

        if(hpPlayerOne < 1 || hpPlayerTwo < 1 || roundTimeLeft < 0)
        {
            switchStateTo(gameState.GameOver);
        }
    }

    //GAMEOVER
    void GameOver()
    {
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
        
        print("GameOver");
    }


    void UpdateFightText()
    {
        uiHpPLOne.text = "HP" + hpPlayerOne.ToString();
        uiHpPLTwo.text = "HP" + hpPlayerTwo.ToString();
    }
    void UpdateBuildText()
    {
        uiHpPLOne.text = "BlocksLeft " + blockBuilderOne.numberOfBlocks.ToString();
        uiHpPLTwo.text = "BlocksLeft " + blockBuilderTwo.numberOfBlocks.ToString();
    }
    //TOGGLE GAMESTATE
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
