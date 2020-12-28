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
    public enum gameStates { Intermission, StartBuild, Build, StartFight, Fight, StartSuddenDeath, SuddenDeath, StartGameOver, GameOver };

    [Header("Indicators")]
    public TextMeshProUGUI uiGameTimeText;
    public TextMeshProUGUI uiGameStateText;

    [Header("Intermission")]
    public float timeTillGameStart = 3;

    [Header("BuildMode")]
    public string buildText = "Build time left ";
    private float buildTime = 30;


    private float buildTimeLeft;
    private float tempTimeFloor;

    [Header("Fight")]
    public string fightText = "FIGHT";
    private float RoundTime = 60;
    private float roundTimeLeft;

    [Header("Juice")]
    public AnimationCurve juiceScaleCurve;
    public AnimationCurve juiceFadeCurve;


    [Header("TMP")]
    //TODO : Move to game over;
    public Canvas canvas;
    public CannonHealth canonOne;
    public CannonHealth canonTwo;
    public GameOverUIMaster GameOverPreFab;
    public GameObject startParticle;

    public Ease easein;
    public Ease easeOut;

    // Start is called before the first frame update
    void Start()
    {
        buildTimeLeft = GameStats.buildTime;
        currentState = gameStates.Intermission;
    }


    //New Juice
    /*
    IEnumerator JuiceInfoText(TextMeshProUGUI text, float speedMul)
    {
        float lerpTime = 0;
        while(lerpTime > 0)
        {
            text.rectTransform.localScale = Vector3.one * juiceScaleCurve.Evaluate(lerpTime);
            lerpTime += speedMul * Time.deltaTime;
            yield return null;
        }
    }
    */
    IEnumerator JuiceFadeInfoText(TextMeshProUGUI text, float speed, Color from, Color to)
    {
        float lerpTime = 0;
        while (lerpTime < 1)
        {
            text.rectTransform.localScale = Vector3.one * juiceFadeCurve.Evaluate(lerpTime);
            text.color = Color.Lerp(to, from, juiceFadeCurve.Evaluate(lerpTime));
            lerpTime += speed * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator JuiceScale(TextMeshProUGUI text, float speed, Vector3 from)
    {
        float lerpTime = 0;
        while (lerpTime < 1)
        {
            text.rectTransform.localScale = from * juiceScaleCurve.Evaluate(lerpTime);
            lerpTime += speed * Time.deltaTime;
            yield return null;
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        //Enter function depending of gameStates
        switch (currentState)
        {
            case gameStates.Intermission:
                {
                    Intermission();
                }
                break;

            case gameStates.StartBuild:
                {
                    StartBuild();
                }
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

    void Intermission()
    {
        timeTillGameStart -= Time.deltaTime;
        float ttsFloor = Mathf.Floor(timeTillGameStart);

        if (timeTillGameStart > 0)
        {
            if (ttsFloor != tempTimeFloor)
            {
                uiGameTimeText.text = "";
                uiGameStateText.text = timeTillGameStart.ToString("F0").PadLeft(2, '0');
                StartCoroutine(JuiceScale(uiGameStateText, 1, Vector3.one));
                tempTimeFloor = ttsFloor;
            }
        }
        else
        {
           // Instantiate(startParticle, Vector3.up * -9.8f, startParticle.transform.rotation);
            TogglegameStatesForward();
        }
    }
    //BUILDMODE
    void StartBuild()
    {
        uiGameTimeText.color = Color.white;
        uiGameStateText.color = Color.white;
        uiGameStateText.rectTransform.localScale = Vector3.one;
        uiGameTimeText.rectTransform.localScale = Vector3.one;


        uiGameStateText.text = buildText;
        StartCoroutine(JuiceFadeInfoText(uiGameStateText, 0.5f, Color.white, Color.clear));
        TogglegameStatesForward();
    }
    void BuildMode()
    {
        buildTimeLeft -= Time.deltaTime;
        float bTimeFloor = Mathf.Floor(buildTimeLeft);

        if (buildTimeLeft > 0)
        {
            if (bTimeFloor != tempTimeFloor)
            {
                uiGameTimeText.text = buildTimeLeft.ToString("F0").PadLeft(2, '0');

                if (buildTimeLeft < 5)
                {
                    uiGameTimeText.color = Color.red;
                    uiGameStateText.color = Color.red;
                    uiGameStateText.text = buildTimeLeft.ToString("F0").PadLeft(2, '0');
                    StartCoroutine(JuiceScale(uiGameStateText, 1, Vector3.one));
                }

                tempTimeFloor = bTimeFloor;

                StartCoroutine(JuiceScale(uiGameTimeText, 1, Vector3.one));
            }

        }
        else
        {
            TogglegameStatesForward();
        }
    }


    //STARTFIGHT
    void StartFight()
    {
        Instantiate(startParticle, Vector3.up * -9.8f, startParticle.transform.rotation);

        roundTimeLeft = GameStats.fightTime;
        uiGameTimeText.color = Color.white;
        uiGameStateText.text = fightText;
        StartCoroutine(JuiceFadeInfoText(uiGameStateText, 0.5f, Color.white, Color.clear));

        TogglegameStatesForward();
    }

    //FIGHTING
    void Fighting()
    {
        roundTimeLeft -= Time.deltaTime;
        float fTimeFloor = Mathf.Floor(roundTimeLeft);

        if (fTimeFloor != tempTimeFloor)
        {
            uiGameTimeText.text = roundTimeLeft.ToString("F0").PadLeft(2, '0');
            StartCoroutine(JuiceScale(uiGameTimeText, 1, Vector3.one));
            tempTimeFloor = fTimeFloor;
            if(fTimeFloor < 5)
            {
                uiGameTimeText.color = Color.red;
                uiGameStateText.color = Color.red;
                uiGameStateText.text = roundTimeLeft.ToString("F0").PadLeft(2, '0');
                StartCoroutine(JuiceScale(uiGameStateText, 1, Vector3.one));
            }
        }
        if (roundTimeLeft < 0)
        {
            TogglegameStatesForward();
            //switchStateTo(gameStates.StartGameOver);
        }
    }

    public void StartSuddenDeath()
    {
        uiGameStateText.color = Color.red;
        uiGameStateText.text = "SuddenDeath";

        StartCoroutine(JuiceFadeInfoText(uiGameStateText, 0.5f, Color.red, Color.clear));

        Debug.Log("Start Sudden Death");
        TogglegameStatesForward();
    }

    public void SuddenDeath(float scoreOne, float scoreTwo)
    {
        if (scoreOne != scoreTwo || scoreOne == 0 && scoreTwo == 0)
        {
            Debug.Log("Print Exit Sudden Death");
            TogglegameStatesForward();
        }
    }

    public void StartGameOver(float scoreOne, float scoreTwo)
    {
        //Instantiate(GameOverPreFab, canvas.transform.position, canvas.transform.rotation, canvas.transform).GameOver(scoreOne, scoreTwo);
        if (scoreOne > scoreTwo)
            GameObject.FindGameObjectWithTag("Finish").GetComponent<RoundTracker>().LeftWin();
        else if (scoreOne < scoreTwo)
            GameObject.FindGameObjectWithTag("Finish").GetComponent<RoundTracker>().RightWin();
        else if (scoreOne == 0 && scoreTwo == 0)
        {
            //Draw
            Instantiate(GameOverPreFab, canvas.transform.position, canvas.transform.rotation, canvas.transform).GameOver(scoreOne, scoreTwo);
        }
        else if (scoreOne == scoreTwo)
        {
            switchStateTo(gameStates.StartSuddenDeath);
            return;
        }

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
            case gameStates.Intermission:
                {
                    currentState = gameStates.StartBuild;
                }
                break;
            case gameStates.StartBuild:
                {
                    currentState = gameStates.Build;
                }
                break;
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
            case gameStates.Intermission:
                {
                    currentState = gameStates.Intermission;
                }
                break;
            case gameStates.StartBuild:
                {
                    currentState = gameStates.StartBuild;
                }
                break;
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
            case gameStates.StartSuddenDeath:
                {
                    currentState = gameStates.StartSuddenDeath;
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
}
