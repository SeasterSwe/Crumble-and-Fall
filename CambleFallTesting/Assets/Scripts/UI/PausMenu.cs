using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.UI;

public class PausMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenu; 
    //public Animation Testanimation_01;

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            
            else
            {
                Pause();
            }
        }
    }
    public void Resume ()
    {
        Time.timeScale = 1f; 
        Debug.Log ("Resume game");
        PauseMenu.SetActive(false);
        GameIsPaused = false;
    }

    public void Controls()
    {
        Debug.Log("Show controls");
    }

    public void Quit()
    {   
        Debug.Log ("quit!");
        Application.Quit();
    }

    public void Options()
    {   
        Debug.Log ("Show Options");
        
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(0);        
        //PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause ()
    {
        Debug.Log ("Pause");
        PauseMenu.SetActive(true);
       // GetComponent<Animation>("Testanimation_01");
        //animation.Play();
        //animation.Play("Testanimation_01");
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
