using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static int ActiveQuestID; //Giver's ID
    //public int InternalQuestNumber;
    public static bool isTakingQuest = false;
	public GameObject lightB;
    public static int questsCompleted;
    public PlayerController playerObject;
    public GameObject[] NPCFrom;
    public GameObject[] NPCTo;

    private int firstQuestTriggerTime = 145;
    private int secondQuestTriggerTime = 145;
	private int thirdQuestTriggerTime = 145;
	private int fourthQuestTriggerTime = 145;
	private int fifthQuestTriggerTime = 145; //TODO

	private void Start() {
        hideNPCs();
	}

	public void HideLight() {
		lightB.SetActive(false);
	}
	void FixedUpdate()
    {

		if (isTakingQuest) {
			lightB.SetActive(true);
            lightB.transform.position = NPCTo[ActiveQuestID].transform.position;
/*			switch (ActiveQuestID) {
				case 0:
					lightB.transform.position = NPC100.transform.position;
					break;
				case 1:
					lightB.transform.position = NPC101.transform.position;
					break;
				case 2:
					lightB.transform.position = NPC102.transform.position;
					break;
				case 3:
					lightB.transform.position = NPC103.transform.position;
					break;
				case 4:
					lightB.transform.position = NPC104.transform.position;
					break;
				default:
					break;
			}
*/		}



        if (GlobalTimer.theSeconds == firstQuestTriggerTime)
        {
            NPCFrom[0].SetActive(true);
            NPCTo[0].SetActive(true);
        }
		if (GlobalTimer.theSeconds == secondQuestTriggerTime) {
            NPCFrom[1].SetActive(true);
            NPCTo[1].SetActive(true);
        }
		if (GlobalTimer.theSeconds == thirdQuestTriggerTime) {
            NPCFrom[2].SetActive(true);
            NPCTo[2].SetActive(true);
        }
		if (GlobalTimer.theSeconds == fourthQuestTriggerTime) {
            NPCFrom[3].SetActive(true);
            NPCTo[3].SetActive(true);
        }
		if (GlobalTimer.theSeconds == fifthQuestTriggerTime) {
            NPCFrom[4].SetActive(true);
            NPCTo[4].SetActive(true);
        }
	}

    private void hideNPCs()
    {
        foreach(GameObject go in NPCFrom)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in NPCTo)
        {
            go.SetActive(false);
        }
    }
    public void rewardPlayer(int ID)
    {
        questsCompleted += 1;
        int incrementAmount = 0;
        if (ID == 100)
        {
            incrementAmount = 50; //
            NPCFrom[0].SetActive(false);
            NPCTo[0].SetActive(false);

        } else if (ID == 101)
        {
            incrementAmount = 50;
            NPCFrom[1].SetActive(false);
            NPCTo[1].SetActive(false);
        }
		else if (ID == 102) {
			incrementAmount = 50;
            NPCFrom[2].SetActive(false);
            NPCTo[2].SetActive(false);
        }
		else if (ID == 103) {
			incrementAmount = 50;
            NPCFrom[3].SetActive(false);
            NPCTo[3].SetActive(false);
        }
		else if (ID == 104) {
			incrementAmount = 40;
            NPCFrom[4].SetActive(false);
            NPCTo[4].SetActive(false);
        }


		playerObject.Energy += incrementAmount;
    }
}
