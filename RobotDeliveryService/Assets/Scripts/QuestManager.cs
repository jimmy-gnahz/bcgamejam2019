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
    private int secondQuestTriggerTime = 145;
	private int thirdQuestTriggerTime = 145;
	private int fourthQuestTriggerTime = 145;
	private int fifthQuestTriggerTime = 145; //TODO

	private void Start() {
	}

	public void HideLight() {
		lightB.SetActive(false);
	}
	void FixedUpdate()
    {

		if (isTakingQuest) {
			lightB.SetActive(true);
			switch (ActiveQuestID) {
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
		}



        if (GlobalTimer.theSeconds == firstQuestTriggerTime)
        {
            NPC000.SetActive(true);
            NPC100.SetActive(true);
        }
		if (GlobalTimer.theSeconds == secondQuestTriggerTime) {
			NPC001.SetActive(true);
			NPC101.SetActive(true);
		}
		if (GlobalTimer.theSeconds == thirdQuestTriggerTime) {
			NPC002.SetActive(true);
			NPC102.SetActive(true);
		}
		if (GlobalTimer.theSeconds == fourthQuestTriggerTime) {
			NPC003.SetActive(true);
			NPC103.SetActive(true);
		}
		if (GlobalTimer.theSeconds == fifthQuestTriggerTime) {
			NPC004.SetActive(true);
			NPC104.SetActive(true);
		}
	}

    public void rewardPlayer(int ID)
    {
        questsCompleted += 1;
        int incrementAmount = 0;
        if (ID == 100)
        {
            incrementAmount = 50; //
            NPC000.SetActive(false);
            NPC100.SetActive(false);

        } else if (ID == 101)
        {
            incrementAmount = 50;
            NPC001.SetActive(false);
            NPC101.SetActive(false);
        }
		else if (ID == 101) {
			incrementAmount = 50;
			NPC001.SetActive(false);
			NPC101.SetActive(false);
		}
		else if (ID == 101) {
			incrementAmount = 50;
			NPC001.SetActive(false);
			NPC101.SetActive(false);
		}
		else if (ID == 101) {
			incrementAmount = 40;
			NPC001.SetActive(false);
			NPC101.SetActive(false);
		}


		playerObject.Energy += incrementAmount;
    }
}
