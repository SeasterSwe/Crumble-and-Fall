using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundTracker : MonoBehaviour
{
    public int totalRounds = 3;
    public int roundsToWin;
    private int winsLeft;
    private int winsRight;

    public static RoundTracker instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            roundsToWin = Mathf.FloorToInt((totalRounds / 2f) + 1);
        }
        else
        {
            Destroy(gameObject);
        }
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

    public void LeftWin()
    {
        winsLeft++;
        if (CheckIfWin())
            ChangeScene();
        else
            GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>().ChangeScene(SceneManager.GetActiveScene().name);
    }
    public void RightWin()
    {
        winsRight++;
        if (CheckIfWin())
            ChangeScene();
        else
            GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>().ChangeScene(SceneManager.GetActiveScene().name);
    }

    void ChangeScene()
    {
       GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>().ChangeScene("GameOver");
    }

    public void ActivateAnimations()
    {
        Transform[] animations = GameObject.FindGameObjectWithTag("GameOverAnimations").GetComponentsInChildren<Transform>();
        print(animations.Length);
        if (winsLeft > winsRight)
        {
            animations[1].gameObject.SetActive(false);
            animations[4].gameObject.SetActive(false);
        }
        if (winsLeft < winsRight)
        {
            animations[2].gameObject.SetActive(false);
            animations[3].gameObject.SetActive(false);
        }
    }
}
