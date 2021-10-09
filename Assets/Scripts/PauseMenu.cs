using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
     public static bool PausedGame = false;
     public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
          {
               if (PausedGame)
               {
                    Resume();
               }
               else
               {
                    Pause();
               }
          }
    }

     public void Resume()
     {
          pauseMenuUI.SetActive(false);
          Time.timeScale = 1f; // set at normal rate
          PausedGame = false;
     }

     void Pause()
     {
          pauseMenuUI.SetActive(true);
          Time.timeScale = 0f; // freeze game
          PausedGame = true; 
     }

     public void LaunchMenu()
     {
          // Debug.Log("Launching Menu...");
          // Time.timescale = 1f;
          SceneManager.LoadScene("Menu");
     }

     public void ExitGame()
     {
          Debug.Log("Exiting Game...");
          Application.Quit();
     }
}
