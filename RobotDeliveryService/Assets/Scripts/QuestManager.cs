using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static int ActiveQuestID; //Giver's ID
    //public int InternalQuestNumber;
    public static bool isTakingQuest = false;
    public static int questsCompleted;

    void Update()
    {
        //InternalQuestNumber = ActiveQuestNumber;
    }

    public static void rewardPlayer(int ID)
    {
        questsCompleted += 1;
        //increment energy
    }
}
