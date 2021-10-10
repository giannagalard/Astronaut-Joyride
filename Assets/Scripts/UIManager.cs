using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text pots;
    public GameObject pauseMenuUI;
    public Image jetpackFuel;
    public GameObject[] hearts;
    public Image EscapeButton;


    // Update is called once per frame
    void Update()
    {
        pots.text = transform.GetComponent<GameInfo>().potions.ToString();
    }

    public void Resume()
    {
        Time.timeScale = 1f; // set at normal rate
        PauseMenu.PausedGame = false;
        pauseMenuUI.SetActive(false);
    }
}
