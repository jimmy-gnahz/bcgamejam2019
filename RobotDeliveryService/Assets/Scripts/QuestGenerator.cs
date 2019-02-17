using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    public GameObject Player; 
    public GameObject[] QuestList; 
    public GameObject UIquest; // Current Quest
    public GameObject Questdisplayname; 
    public bool _Talk = false; // is talking to person, a panel UI shows up
    public bool _questing = false; // doing quests?
    // Update is called once per frame

    private int firstQuestTriggerTime = 140;

    void Update()
    {
        if(GlobalTimer.theSeconds == firstQuestTriggerTime && !_questing)
        {
           // New Quest Initialize
        }

        //if (Input.GetButtonDown("Accept") && _Talk)
        //{
        //    AcceptQuest();
        //    _Talk = false;
        //}
        //if (Input.GetButtonDown("Return") && _Talk)
        //{
        //    DeniedQuest();
        //    _Talk = false;
        //}

    }

    public void AcceptQuest()
    {
        UIquest.SetActive(false);
        Questdisplayname.SetActive(true);
        _questing = true;
    }

    void DeniedQuest()
    {
        UIquest.SetActive(false);
        Questdisplayname.SetActive(false);
        _questing = false;
    }
}
