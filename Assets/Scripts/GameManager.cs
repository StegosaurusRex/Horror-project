using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    
    public static bool GameIsPaused=false;
    public GameObject pauseMenuUI;
    public GameObject pauseMenuUIOptions;
    public GameObject pauseMenuUIGameOver;
    public GameObject pauseMenuUIWinGame;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseMenuUIOptions.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        GameIsPaused = false;
        

    } 
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        
        Time.timeScale = 0f;
        GameIsPaused = true;
        

    }
    public void PauseGameOver()
    {
        pauseMenuUIGameOver.SetActive(true);
        
        Time.timeScale = 0f;
        GameIsPaused = true;
        

    }
    public void PauseWinGame()
    {
        pauseMenuUIWinGame.SetActive(true);
        
        Time.timeScale = 0f;
        GameIsPaused = true;
        

    }
    public void Restart()
    { 
        SceneManager.LoadScene(1);
        DogExample.playerIsDead = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
