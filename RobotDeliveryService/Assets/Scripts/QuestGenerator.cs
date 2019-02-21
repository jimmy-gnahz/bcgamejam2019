// This Class is not in use as of 2019-02-20

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    public GameObject Player; 
    public GameObject NPC000;
    public GameObject NPC001;
    public GameObject NPC002;
    public GameObject NPC003;
    public GameObject NPC004;
    public GameObject NPC100;
    public GameObject NPC101;
    public GameObject NPC102;
    public GameObject NPC103;
    public GameObject NPC104;
    public GameObject UIquest; // Current Quest
    public GameObject Questdisplayname; 
    public bool _Talk = false; // is talking to person, a panel UI shows up
    public bool _questing = false; // doing quests?
    // Update is called once per frame

    private int firstQuestTriggerTime = 140;

    void Update()
    {
        //if(GlobalTimer.theSeconds == firstQuestTriggerTime && !_questing)
        //{
        //    NPC000.SetActive(true);
        //    NPC100.SetActive(true);
        //}

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
