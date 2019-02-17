using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static int ActiveQuestID; //Giver's ID
    //public int InternalQuestNumber;
    public static bool isTakingQuest = false;
    public static int questsCompleted;
    public PlayerController playerObject;
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

    private int firstQuestTriggerTime = 145;
    private int secondQuestTriggerTime = 120;   

    void FixedUpdate()
    {
        if (GlobalTimer.theSeconds == firstQuestTriggerTime)
        {
            NPC000.SetActive(true);
            NPC100.SetActive(true);
        }
        if (GlobalTimer.theSeconds == secondQuestTriggerTime)
        {
            NPC001.SetActive(true);
            NPC101.SetActive(true);
        }
    }

    public void rewardPlayer(int ID)
    {
        questsCompleted += 1;
        int incrementAmount = 0;
        if (ID == 100)
        {
            incrementAmount = 5; //
            NPC000.SetActive(false);
            NPC100.SetActive(false);

        } else if (ID == 101)
        {
            incrementAmount = 4;
            NPC001.SetActive(false);
            NPC101.SetActive(false);
        }

        playerObject.Energy += incrementAmount;
    }
}
