using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunction : MonoBehaviour
{

    public AudioSource buttonPress;
    //public GameObject bestScore;

    private void Start()
    {
        //bestScore = PlayerPrefs.GetInt("");

    }

    public void PlayGame()
    {
        buttonPress.Play();
        RedirectToLevel.redirectToLevel = 2;
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        buttonPress.Play();
        Application.Quit();
    }

    public void HighScore()
    {
        buttonPress.Play();
        SceneManager.LoadScene(3);
    }
}