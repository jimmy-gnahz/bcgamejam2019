using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    public int Trigger;
    public GameObject Player;
    public GameObject[] QuestList;
    public GameObject UIquest;
    public GameObject Questdisplayname;
    public bool _Talk = false;
    public bool _questing = false;
    // Update is called once per frame
    void Update()
    {
        Trigger++;
        if(Trigger % 500 == 0 && !_questing)
        {
           // GameObject UIquest = QuestList[Random.Range(0, QuestList.Length -1)];
            //quest.SetActive(true);
            UIquest.SetActive(true);
            _Talk = true;
        }

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
