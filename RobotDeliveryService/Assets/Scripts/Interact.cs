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
    public QuestManager questManager;
    public bool _Talk = false;

    // Building ID in range of 1000-9999.
    // Quest Giving NPC: ID in range of 0 - 99.
    // Receiving NPC: ID in range of 100 - 199
    public int ID; 

    public GameObject _UIQuestinfo; 
    public GameObject _UIQuestname;
    public GameObject _Questdisplayname;

    private GameObject _Object;
    public GameObject[] buttons;

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

    public void AcceptQuest()
    {
        _UIquest.SetActive(false);
        _Questdisplayname.GetComponent<Text>().text = "Quest: " + _UIQuestinfo.GetComponent<Text>().text;
        _Questdisplayname.SetActive(true);
        QuestManager.isTakingQuest = true;
    }

    void DeniedQuest()
    {
        _UIquest.SetActive(false);
        _Questdisplayname.SetActive(false);
    }

    void OnMouseOver()
    {
        //Cannot talk to someone on a rooftop or high ground
        if (_Distance <= 3 && _Player.transform.position.y < 1.5 && !_Talk)
        {
            if (_Object.tag.Contains("NPC"))
            {
                _CommandDisplay.GetComponent<Text>().text = "Talk";
                _CommandText.GetComponent<Text>().text = "E";
            }
            else if (_Object.tag.Contains("Building"))
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
                if (_Object.tag.Contains("Building"))
                {
                    _Player.transform.Translate(Vector3.up * _Object.transform.localScale.y);
                    _Player.transform.Translate(Vector3.forward * _Object.transform.localScale.z);
                }
                else
                {   // Assume object is either building or NPC
                    _UIquest.SetActive(true);
                    if(ID >= 100 && ID < 200)
                    {
                        foreach (GameObject go in buttons)
                        {
                            go.SetActive(false);
                        }
                    }
                    if (ID == 0)    
                    {   //TODO: Change all this into an array
                        _UIQuestinfo.GetComponent<Text>().text = "First Quest Info Placeholder";
                        _UIQuestname.GetComponent<Text>().text = "First Quest Name Placeholder";
                        _Questdisplayname.GetComponent<Text>().text = "First Quest DisplayName Placeholder";
                        QuestManager.ActiveQuestID = ID;
                        _Talk = true;
                    }
                    else if (ID == 1)
                    {
                        _UIQuestinfo.GetComponent<Text>().text = "Second Quest Info Placeholder";
                        _UIQuestname.GetComponent<Text>().text = "Second Quest Name Placeholder";
                        _Questdisplayname.GetComponent<Text>().text = "Second Quest DisplayName Placeholder";
                        QuestManager.ActiveQuestID = ID;
                        _Talk = true;
                    }
                    else if (100 == ID)
                    {
                        QuestManager.isTakingQuest = false;
                        _UIQuestinfo.GetComponent<Text>().text = "First Quest Receiver Info Placeholder";
                        _UIQuestname.GetComponent<Text>().text = "First Quest Receiver Name Placeholder";
                        _Questdisplayname.GetComponent<Text>().text = "First Quest Receiver DisplayName Placeholder";
                        _Talk = true;
                        questManager.rewardPlayer(ID);
                    }
                    else if (101 == ID)
                    {
                        QuestManager.isTakingQuest = false;
                        _UIQuestinfo.GetComponent<Text>().text = "Second Quest Receiver Info Placeholder";
                        _UIQuestname.GetComponent<Text>().text = "Second Quest Receiver Name Placeholder";
                        _Questdisplayname.GetComponent<Text>().text = "Second Quest Receiver DisplayName Placeholder";
                        _Talk = true;
                        questManager.rewardPlayer(ID);
                    }
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
