using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     public void PlayGame()
     {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     }

     public void QuitGame()
     {
          // LOG TO CONSOLE TO MAKE SURE ITS WORKING, UNITY EDITOR IS GROSS
          Debug.Log("QUIT!");
          Application.Quit();
     }
}
