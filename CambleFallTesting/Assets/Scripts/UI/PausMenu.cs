using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenu; 
    

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

    public void Quit()
    {   
        Debug.Log ("quit!");
        Application.Quit();
    }

    public void Options()
    {   
        Debug.Log ("Options");
        
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
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
