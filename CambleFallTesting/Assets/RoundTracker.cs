using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundTracker : MonoBehaviour
{
    public int totalRounds = 3;
    public int roundsToWin;
    private int winsLeft;
    private int winsRight;
    public int[] wins;
    public static RoundTracker instance;
    public GameObject starPos;
    public float distBetwean;
    public Image star;
    public Image redStar;
    public Image yellowStar;
    private Image[] stars;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            totalRounds = GameStats.amountOfRounds;
            roundsToWin = Mathf.FloorToInt((totalRounds / 2f) + 1);
        }
        else
        {
            Destroy(gameObject);
        }
        
        if (wins.Length == 0)
            wins = new int[totalRounds];
    }
    public bool CheckIfWin()
    {
        if (winsLeft >= roundsToWin)
            return true;
        else if (winsRight >= roundsToWin)
            return true;
        else
            return false;
    }

    public bool leftPlayerWon;

    public void LeftWin()
    {
        wins[winsLeft] = 1;
        winsLeft++;
        leftPlayerWon = true;
        
        ChangeScene();
    }
    public void RightWin()
    {
        wins[(totalRounds - 1) - winsRight] = 2;
        //instansiate redstar in middle?
        winsRight++;
        leftPlayerWon = false;

        ChangeScene();
    }

    void ChangeScene()
    {
        GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>().ChangeScene("GameOver");
    }

    public void ActivateAnimations()
    {
        Transform[] animations = GameObject.FindGameObjectWithTag("GameOverAnimations").GetComponentsInChildren<Transform>();
        print(animations.Length);
        if (leftPlayerWon == true)
        {
            animations[1].gameObject.SetActive(false);
            animations[5].gameObject.SetActive(false);
        }
        //höger
        if (leftPlayerWon == false)
        {
            animations[2].gameObject.SetActive(false);
            animations[4].gameObject.SetActive(false);
        }
    }

    public void Setup(GameObject obj)
    {
        Vector3 sPos = obj.transform.position;
        totalRounds = GameStats.amountOfRounds;
        float walla = (totalRounds - 1) * distBetwean / 2;

        starPos = obj;
        stars = starPos.GetComponentsInChildren<Image>();

        if (wins.Length == 0)
            wins = new int[totalRounds];

        for (int i = 0; i < totalRounds; i++)
        {
            Image startClone;
            if (wins[i] == 1)
                startClone = Instantiate(yellowStar, sPos + (Vector3.right * (-walla + distBetwean * i)), star.transform.rotation);
            else if (wins[i] == 2)
                startClone = Instantiate(redStar, sPos + (Vector3.right * (-walla + distBetwean * i)), star.transform.rotation);
            else
                startClone = Instantiate(star, sPos + (Vector3.right * (-walla + distBetwean * i)), star.transform.rotation);

            startClone.transform.SetParent(obj.transform);
        }

    }


    public void ResetStats()
    {
        wins = new int[totalRounds];
        winsLeft = 0;
        winsRight = 0;
    }
}
