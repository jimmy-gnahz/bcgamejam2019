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
        RedirectToLevel.redirectToLevel = 3;
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        buttonPress.Play();
        Application.Quit();
    }

    public void HighScore()
    {
        buttonPress.Play();
        SceneManager.LoadScene(2);
    }
}