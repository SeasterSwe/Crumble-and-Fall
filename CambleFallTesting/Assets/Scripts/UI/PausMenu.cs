using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenu; 
    public GameObject Optionsmenu;
    public GameObject Controlsmenu;
    //public Animation Testanimation_01;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Controlsmenu.SetActive(true);
    }

    public void Quit()
    {   
        Debug.Log ("quit!");
        Application.Quit();
    }

    public void Options()
    {   
        Debug.Log ("Show Options");
        Optionsmenu.SetActive(true);
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
