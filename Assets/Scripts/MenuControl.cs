using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    
    
    public void Restart()
    { 
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        GameManager.GameIsPaused = false;
    }
   public void QuitGame()
    {
        Application.Quit();
    }
}
