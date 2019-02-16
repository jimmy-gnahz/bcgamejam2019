using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreFunctions : MonoBehaviour
{
    public AudioSource buttonPress;

    private void Start()
    {
        //bestScore = PlayerPrefs.GetInt("");

    }

    // Update is called once per frame
    public void ReturnToMenu()
    {
        buttonPress.Play();
        SceneManager.LoadScene(1);
    }
}
