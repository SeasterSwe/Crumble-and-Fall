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
    public enum gameStates { BuildCountDown, Build, StartFight, Fight, StartSuddenDeath, SuddenDeath, StartGameOver, GameOver };

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
    public CannonHealth canonOne;
    public CannonHealth canonTwo;
    public GameOverUIMaster GameOverPreFab;


    public Ease easein;
    public Ease easeOut;

    // Start is called before the first frame update
    void Start()
    {
        buildTimeLeft = GameStats.buildTime;
        currentState = gameStates.BuildCountDown;
        StartCoroutine(StartDelay(3f));
    }

    IEnumerator StartDelay(float counterTime)
    {
        float t = counterTime;
        while (t >= 0)
        {
            ScaleTextPeriod();
            uiGameTimeText.text = "";
            uiGameStateText.text = t.ToString("F0").PadLeft(2, '0');
            t -= Time.deltaTime;
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        buildTimeLeft = GameStats.buildTime;
        currentState = gameStates.Build;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Enter function depending of gameStates
        switch (currentState)
        {
            case gameStates.BuildCountDown:
                break;
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

            case gameStates.StartSuddenDeath:
                {
                    StartSuddenDeath();
                }
                break;
            case gameStates.SuddenDeath:
                {
                    SuddenDeath(canonOne.currentHeatlh, canonTwo.currentHeatlh);
                }
                break;

            case gameStates.StartGameOver:
                {
                    StartGameOver(canonOne.currentHeatlh, canonTwo.currentHeatlh);
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
            uiGameTimeText.text = buildTimeLeft.ToString("F0").PadLeft(2, '0');
            ScaleText();
        }
        else
        {
            TogglegameStatesForward();
        }
    }


    //STARTFIGHT
    void StartFight()
    {
        StartCoroutine(FightTextDisapear(3));
        uiGameStateText.text = fightText;
        roundTimeLeft = GameStats.fightTime;
        TogglegameStatesForward();
    }
    IEnumerator FightTextDisapear(float t)
    {
        Scale(0.8f);
        yield return new WaitForSeconds(t);
        uiGameStateText.text = null;
    }
    void Scale(float t)
    {
        Vector3 scale = uiGameStateText.rectTransform.localScale;
        uiGameStateText.rectTransform.DOScale(scale + Vector3.one * 0.4f, t).OnComplete(ScaleBack);
    }

    void ScaleBack()
    {
        Color color = uiGameStateText.color;
        color.a = 0f;
        uiGameStateText.DOColor(color, 2.2f);
        uiGameStateText.rectTransform.DOScale(Vector3.one * 3.184851f, 2.2f);
    }

    //FIGHTING
    void Fighting()
    {
        roundTimeLeft -= Time.deltaTime;
        uiGameTimeText.text = roundTimeLeft.ToString("F0").PadLeft(2, '0');
        ScaleText();

        if (roundTimeLeft < 0)
        {
            TogglegameStatesForward();
            //switchStateTo(gameStates.StartGameOver);
        }
    }

    public void StartSuddenDeath()
    {
        Debug.Log("Start Sudden Death");
        TogglegameStatesForward();
    }

    public void SuddenDeath(float scoreOne, float scoreTwo)
    {
        if(scoreOne != scoreTwo)
        {
            Debug.Log("Print Exit Sudden Death");
            TogglegameStatesForward(); 
        }
    }

    public void StartGameOver(float scoreOne, float scoreTwo)
    {
        //Instantiate(GameOverPreFab, canvas.transform.position, canvas.transform.rotation, canvas.transform).GameOver(scoreOne, scoreTwo);
        if(scoreOne == scoreTwo)
        {
            switchStateTo(gameStates.StartSuddenDeath);
            return;

        }else if (scoreOne > scoreTwo)
            GameObject.FindGameObjectWithTag("Finish").GetComponent<RoundTracker>().LeftWin();
        else if (scoreOne < scoreTwo)
            GameObject.FindGameObjectWithTag("Finish").GetComponent<RoundTracker>().RightWin();

        /*
        else
        {
            
            if (scoreOne != 0)
            {
                roundTimeLeft += 30f;
                switchStateTo(gameStates.Fight);
                return;
            }
            else
                Instantiate(GameOverPreFab, canvas.transform.position, canvas.transform.rotation, canvas.transform).GameOver(scoreOne, scoreTwo);
        }
            */

        switchStateTo(gameStates.GameOver);
        print("StartGameOver");
    }

    //GAMEOVER
    void GameOver()
    {
        //print("GameOver");
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

            case gameStates.StartSuddenDeath:
                {
                    currentState = gameStates.SuddenDeath;
                }
                break;

            case gameStates.SuddenDeath:
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
            case gameStates.SuddenDeath:
                {
                    currentState = gameStates.SuddenDeath;
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
    bool active = false;
    void ScaleText()
    {
        if (!active)
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

    bool activePeriod = false;
    void ScaleTextPeriod()
    {
        if (!activePeriod)
        {
            activePeriod = true;
            uiGameStateText.rectTransform.DOScale(Vector3.one * 3.5f, 0.5f).SetEase(easein).OnComplete(ResetTextPeriod);
        }
    }
    void ResetTextPeriod()
    {
        uiGameStateText.rectTransform.DOScale(Vector3.one * 3f, 0.5f).SetEase(easeOut).OnComplete(ActivePeriodFalse);
    }
    void ActivePeriodFalse()
    {
        activePeriod = false;
    }
}
