using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalTimer : MonoBehaviour
{

    public GameObject timeDisplay01;
    public GameObject timeDisplay02;
    public bool isTakingTime = false;
    public static int theSeconds = 150;

    void Update()
    {
        if (isTakingTime == false)
        {
            StartCoroutine(SubtractSecond());
        }
    }

    IEnumerator SubtractSecond()
    {
        isTakingTime = true;
        theSeconds -= 1;
        timeDisplay01.GetComponent<Text>().text = "Timer: " + theSeconds;
        timeDisplay02.GetComponent<Text>().text = "Timer: " + theSeconds;
        yield return new WaitForSeconds(1);
        isTakingTime = false;
    }

}