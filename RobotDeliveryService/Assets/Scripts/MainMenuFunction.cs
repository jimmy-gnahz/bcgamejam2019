﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunction : MonoBehaviour
{

    //public AudioSource buttonPress;
    //public GameObject bestScore;

    private void Start()
    {
        //bestScore = PlayerPrefs.GetInt("");

    }

    public void PlayGame()
    {
        //buttonPress.Play();
        RedirectToLevel.redirectToLevel = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //public void PlayCreds()
    //{
    //    buttonPress.Play();
    //    SceneManager.LoadScene(4);
    //}
}