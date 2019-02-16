using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float _Distance;
    public GameObject _CommandDisplay;
    public GameObject _CommandText;
    public GameObject _Player;
    public GameObject _Building;
    void Update()
    {
       
        _Distance = PlayerCasting.Distancefromtarget;

        if (_Distance <= 1)
        {
            _CommandDisplay.SetActive(true);
            _CommandText.SetActive(true);
        }
        else
        {
            _CommandText.SetActive(false);
            _CommandDisplay.SetActive(false);

        }

        if (Input.GetButtonDown("Action"))
        {
            if (_Distance <= 1)
            {
                _CommandDisplay.SetActive(false);
                _CommandText.SetActive(false);
                _Player.transform.Translate(Vector3.up * 7);
                _Player.transform.Translate(Vector3.forward * 1);
            }
        }
    }
        
}
