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

    //public void CheckWhoWon()
    //{
    //    if (winsLeft >= roundsToWin)
    //        Debug.Log("LeftWins");
    //    if (winsRight >= roundsToWin)
    //        Debug.Log("RightWins");            
    //}
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
            Debug.Log("LeftWon");
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RightWin()
    {
        winsRight++;
        if (CheckIfWin())
            Debug.Log("RightWon");
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
