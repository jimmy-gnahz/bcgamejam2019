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

    public void rewardPlayer(int ID)
    {
        questsCompleted += 1;
        int incrementAmount = 0;
        if (ID == 100)
        {
            incrementAmount = 5; //
        } else if (ID == 101)
        {
            incrementAmount = 4;
        }

        playerObject.Energy += incrementAmount;
        //increment energy
    }
}
