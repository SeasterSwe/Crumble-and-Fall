using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTracker : MonoBehaviour
{
    public int totalRounds = 3;
    private int winsLeft;
    private int winsRight;

    public static RoundTracker instance;  //Singleton instance

    void Start()
    {
        if (instance == null)
        {
            instance = this; //Save our object so we can use it easily
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);   //If we already have an instance, avoid creating another.
        }
    }

    public void CheckForWin()
    {
        //WallaStämmer ej ju behöver mat
        if(winsLeft + winsRight >= totalRounds)
        {
            if (winsLeft > winsRight)
                print("left wins");
            else
                print("right wins");
        }
    }
}
