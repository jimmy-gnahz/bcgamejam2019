using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public float _Distance;
    public GameObject _CommandDisplay;
    public GameObject _CommandText;
    public GameObject _UIquest;
    public GameObject _Player;
    public GameObject _Object;
    public bool _Talk = false;
    public GameObject _UIQuestinfo;
    public GameObject _UIQuestname;
    public GameObject _Questdisplayname;

    private void Start()
    {
        _Object = this.gameObject;
    }
    void Update()
    {

        _Distance = PlayerCasting.Distancefromtarget;

        if (Input.GetButtonDown("Accept") && _Talk)
        {
            AcceptQuest();
            _Talk = false;
        }
        if (Input.GetButtonDown("Return") && _Talk)
        {
            DeniedQuest();
            _Talk = false;
        }
    }


    void getQuest()
    {
        _UIQuestinfo.GetComponent<Text>().text = "My first quest =D";
        _UIQuestinfo.GetComponent<Text>().text = "Here please deliver this to somebody, thank you";

    }

    public void AcceptQuest()
    {
        _UIquest.SetActive(false);
        _Questdisplayname.GetComponent<Text>().text = "Quest: " + _UIQuestinfo.GetComponent<Text>().text;
        _Questdisplayname.SetActive(true);
        QuestManager.ActiveQuestNumber = 1;
    }

    void DeniedQuest()
    {
        _UIquest.SetActive(false);
        _Questdisplayname.SetActive(false);

    }
    void OnMouseOver()
    {
        if (_Distance <= 3 && _Player.transform.position.y < 1.5 && !_Talk)
        {
            if (_Object.name.Contains("NPC"))
            {
                _CommandDisplay.GetComponent<Text>().text = "Talk";
                _CommandText.GetComponent<Text>().text = "E";
            }
            else if (_Object.name.Contains("Building"))
            {
                _CommandDisplay.GetComponent<Text>().text = "Use Elevator";
                _CommandText.GetComponent<Text>().text = "E";
            }

            _CommandDisplay.SetActive(true);
            _CommandText.SetActive(true);
        }



        if (Input.GetButtonDown("Action"))
        {
            if (_Distance <= 3 && _Player.transform.position.y < 1.5 && !_Talk)
            {

                if (_Object.name.Contains("NPC"))
                {
                     getQuest();
                    _UIquest.SetActive(true);
                    _Talk = true;

                }
                else if (_Object.name.Contains("Building"))
                {
                    _Player.transform.Translate(Vector3.up * _Object.transform.localScale.y);
                    _Player.transform.Translate(Vector3.forward * _Object.transform.localScale.z);
                }
                _CommandDisplay.SetActive(false);
                _CommandText.SetActive(false);

            }
        }


    }

    void OnMouseExit()
    {
        _CommandText.SetActive(false);
        _CommandDisplay.SetActive(false);


    }

}
