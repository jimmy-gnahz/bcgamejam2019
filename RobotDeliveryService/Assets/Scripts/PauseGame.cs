using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{

    public bool gamePaused = false;
    //public AudioSource levelMusic;
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gamePaused == false)
            {
                Time.timeScale = 0;
                gamePaused = true;
                //levelMusic.Pause();
                pauseMenu.SetActive(true);
            }
            else
            {
                resumeGame();
            }
        }
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        //levelMusic.UnPause();
        gamePaused = false;
        Time.timeScale = 1;
    }

    public void restartLevel()
    {
        pauseMenu.SetActive(false);
        //levelMusic.UnPause();
        gamePaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(RedirectToLevel.redirectToLevel);
    }

    public void quitToMenu()
    {
        pauseMenu.SetActive(false);
        //levelMusic.UnPause();
        gamePaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}