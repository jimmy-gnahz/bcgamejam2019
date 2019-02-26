//**************************************************************
//
//  This code is intended to be attached to objects that
//  can interact with the player, such as buildings and NPC's
//
//**************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public float distToPlayer;
    public GameObject commandTextUI;
    public GameObject commandLetterUI;
    public GameObject questUI;
    public GameObject player;

    public GameObject rooftop;

    public QuestManager questManager;
    public static bool isTalking = false;

    // Building ID in range of 1000-9999.
    // Quest Giving NPC: ID in range of 0 - 99.
    // Receiving NPC: ID in range of 100 - 199
    public int ID;

    public GameObject questInfoUI;
    public GameObject questNameUI;
    public GameObject currentQuestNameUI;
    public GameObject[] buttons;

    private int triggerActionPromptDistance = 3;

    private void Awake()
    {
        if (commandTextUI == null)
            commandTextUI = GameObject.FindGameObjectWithTag("ActionText");
        if (commandLetterUI == null)
		commandLetterUI = GameObject.FindGameObjectWithTag("KeyText");
        if (questUI == null)
			questUI = GameObject.FindGameObjectWithTag("UIquest");
		if (questInfoUI == null)
			questInfoUI = GameObject.FindGameObjectWithTag("UIQuestinfo");
		if (questNameUI == null)
			questNameUI = GameObject.FindGameObjectWithTag("UIQuestname");
        if (currentQuestNameUI == null)
			currentQuestNameUI = GameObject.FindGameObjectWithTag("Questdisplayname");
		if (player == null)
			player = GameObject.FindGameObjectWithTag("Player");
        if (gameObject.tag.Equals("Building"))
        {
            rooftop = transform.parent.GetChild(1).gameObject;
            print(rooftop.tag);
        }
    }

 
    void Update()
    {
        // 2D coordinates of the object, and 2D distance
        Vector3 planarPos = new Vector3(transform.position.x, 0, transform.position.z); 
        distToPlayer = (player.transform.position - planarPos).magnitude;

        HandleInputs();
    }

    public void HandleInputs()
    {
        if (QuestManager.isTakingQuest) 
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("Accept")) && isTalking)
            {
                questUI.SetActive(false);
                currentQuestNameUI.GetComponent<Text>().text = "Quest: ";
                currentQuestNameUI.SetActive(true);
                QuestManager.isTakingQuest = false;
                if (ID < 100)
                {
                    QuestManager.hasAQuest = true;
                }
                else if (ID >= 100 && ID < 200)    //talking to turn in npc
                {
                    Debug.Log("talking to turn in npc");
                    if (QuestManager.hasAQuest) //with a quest
                    {
                        Debug.Log("with a quest");
                        if (QuestManager.ActiveQuestID == ID - 100) //and the quest is right
                        {
                            Debug.Log("now we know the quest is finished");
                            QuestManager.hasAQuest = false; //now we know the quest is finished
                        }
                    }
                }
            }
        }
        else if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("Accept")) && isTalking)
        {
            AcceptQuest();
            isTalking = false;
        }
        else if (Input.GetButtonDown("Return") && isTalking)
        {
            DeniedQuest();
            isTalking = false;
            QuestManager.isTakingQuest = false;
        }
    }

    public void AcceptQuest()
    {
        questUI.SetActive(false);
        currentQuestNameUI.GetComponent<Text>().text = "Quest: " + questInfoUI.GetComponent<Text>().text;
        currentQuestNameUI.SetActive(true);
        QuestManager.isTakingQuest = true;

 
    }

    void DeniedQuest()
    {
        questUI.SetActive(false);
        currentQuestNameUI.SetActive(false);
    }

    void OnMouseOver()
    {
        //Cannot talk to someone on a rooftop or high ground
        if (distToPlayer <= triggerActionPromptDistance && 
			player.transform.position.y < 1.5 && 
			!isTalking)
        {
            if (gameObject.tag.Contains("NPC"))
            {
                commandTextUI.GetComponent<Text>().text = "Talk";
                commandLetterUI.GetComponent<Text>().text = "E";
            }

            else if (gameObject.tag.Equals("Building"))
            {
                commandTextUI.GetComponent<Text>().text = "Use Elevator";
                commandLetterUI.GetComponent<Text>().text = "E";
            }

            commandTextUI.SetActive(true);
            commandLetterUI.SetActive(true);
        }

        if (Input.GetButtonDown("Action"))
        {

            if (distToPlayer <= triggerActionPromptDistance && !isTalking)
            {
                if (gameObject.tag.Contains("Building"))
                {
                    player.transform.Translate(Vector3.up * rooftop.transform.position.y);
                    player.transform.SetPositionAndRotation(rooftop.transform.position, player.transform.rotation);
				}
                else
                {   // Assume object is either building or NPC
                    
					Debug.Log(" is ID : " + ID);
                    if (ID >= 100 && ID < 200)
                    {
                        buttons[1].SetActive(false);//handing in quests, please don't refuse....... just, you just keep being kind...
                    }
                    else if (ID < 100)
                    {
                        foreach (GameObject go in buttons)
                        {
                            go.SetActive(true);
                        }
                    }
                        if (ID == 0)    
                    {   //TODO: Change all this into an array
						questUI.SetActive(true);
						questInfoUI.GetComponent<Text>().text = "Dear K1ndess ROBO, \nHello, I need you to grab this manga…it’s called Undying lover to Tentacle-senpai.I’m unable to buy it because of reason. So please deliver that manga to me…< 3";
                        questNameUI.GetComponent<Text>().text = "The Otaku’s needs ";
						currentQuestNameUI.GetComponent<Text>().text = "The Otaku’s needs ";
                        QuestManager.ActiveQuestID = ID;
                        QuestManager.isTakingQuest = true;
						questNameUI.SetActive(true);
						questInfoUI.SetActive(true);
						isTalking = true;
					}
                    else if (ID == 1)
                    {
						questUI.SetActive(true);
                        questInfoUI.GetComponent<Text>().text = "Hello K1ndess Robot, /nPlease take this boy and bring him back home. He needs to attend his piano lessons in three minutes. He’s a busy boy and he’s already three months old…. THREE MONTHS!!!!...Driving him around just waste too much precious time where he could be studying math or practicing violin, or composing the next world famous symphony….Please bring him home safely and FAST!!!!!";

						questNameUI.GetComponent<Text>().text = "Bring my Son back home";
                        currentQuestNameUI.GetComponent<Text>().text = "Bring my Son back home";
                        QuestManager.ActiveQuestID = ID;
                        QuestManager.isTakingQuest = true;
                        questNameUI.SetActive(true);
						questInfoUI.SetActive(true);
						isTalking = true;
					}
					else if (ID == 2) {
						questUI.SetActive(true);
						questInfoUI.GetComponent<Text>().text = "Hello K1ndess Robot,\nA package from the post office has just arrived and I need you to deliver it to me immediately.Just to make sure you grab everything, the package contains variety textbooks such as Advance Vector Calculus, Theoretical and Experimental Physics, Quantum Computing…. my dear son needs to start early so when he grows up, he’ll be set to become a billionaire.Just imagine it, my son, the next Bill Gates….. NOW HURRY UP, THE MORE TIME YOU WASTE, THE LESS TIME FOR MY SON TO PREPARE FOR HIS LIFE GOAL!!!!!!!!";
					  questNameUI.GetComponent<Text>().text = "Getting ahead in life";
						currentQuestNameUI.GetComponent<Text>().text = "Getting ahead in life";
						QuestManager.ActiveQuestID = ID;
                        QuestManager.isTakingQuest = true;
                        questNameUI.SetActive(true);
						questInfoUI.SetActive(true);
						isTalking = true;
					}
					else if (ID == 3) {
						questUI.SetActive(true);
						questInfoUI.GetComponent<Text>().text = "Hello K1ndess Robot, \nI’ve written a confession letter to my crush and I need you to deliver to him. He lives across the city and I’m too embarrassed to give it to him in person. I’ve poured my whole heart into that letter so please give it to him. Here take it. ";
						questNameUI.GetComponent<Text>().text = "Someday You Will understand “Love”";
						currentQuestNameUI.GetComponent<Text>().text = "Someday You Will understand “Love”";
						QuestManager.ActiveQuestID = ID;
                        QuestManager.isTakingQuest = true;
                        questNameUI.SetActive(true);
						questInfoUI.SetActive(true);
						isTalking = true;
					}
					else if (ID == 4) {
						questUI.SetActive(true);
						questInfoUI.GetComponent<Text>().text = "Greeting K1ndness Robot, \nMy current delivery boy has just broken his legs trying to balance 10 pizza boxes on his head. Don’t worry, the pizzas in the boxes were fine. Can you please take over for him and deliver this pizza to our next customer, Thank you.";
						questNameUI.GetComponent<Text>().text = "PIZZA PIZZA!!!";
						currentQuestNameUI.GetComponent<Text>().text = "PIZZA PIZZA!!!";
						QuestManager.ActiveQuestID = ID;
                        QuestManager.isTakingQuest = true;
                        questNameUI.SetActive(true);
						questInfoUI.SetActive(true);
						isTalking = true;
					}

					else if (100 == ID && QuestManager.ActiveQuestID == 0) {
						questUI.SetActive(true);
						questInfoUI.GetComponent<Text>().text = "Oh boy, I can’t wait to read this, thank you ";
                        questNameUI.GetComponent<Text>().text = "The Otaku’s needs ";
                        currentQuestNameUI.GetComponent<Text>().text = "The Otaku’s needs ";
                        QuestManager.isTakingQuest = false;
                        questManager.rewardPlayer();
						questNameUI.SetActive(true);
						questInfoUI.SetActive(true);
						isTalking = true;

					}
                    else if (101 == ID && QuestManager.ActiveQuestID == 1) {
						questUI.SetActive(true);
						QuestManager.isTakingQuest = false;
                        questInfoUI.GetComponent<Text>().text = "Oh my, you already 0.5347 second late for your lessons. Thank you K1ndness";
                        questNameUI.GetComponent<Text>().text = "Bring my Son back home";
                        currentQuestNameUI.GetComponent<Text>().text = "Bring my Son back home";
                        QuestManager.isTakingQuest = false;
                        questManager.rewardPlayer();
						questNameUI.SetActive(true);
						questInfoUI.SetActive(true);
						isTalking = true;
					}
					else if (102 == ID && QuestManager.ActiveQuestID == 2) {
						questUI.SetActive(true);
						QuestManager.isTakingQuest = false;
						questInfoUI.GetComponent<Text>().text = "Thank you Kindness for deliver these to me. Because of you, my son is one step closer to becoming a billionaire.";
						questNameUI.GetComponent<Text>().text = "Getting ahead in life";
						currentQuestNameUI.GetComponent<Text>().text = "Getting ahead in life";
                        QuestManager.isTakingQuest = false;
                        questManager.rewardPlayer();
						questNameUI.SetActive(true);
						questInfoUI.SetActive(true);
						isTalking = true;
					}
					else if (103 == ID && QuestManager.ActiveQuestID == 3) {
						questUI.SetActive(true);
						QuestManager.isTakingQuest = false;
						questInfoUI.GetComponent<Text>().text = "Oh? A letter for me. I wonder who it’s from? ";
						questNameUI.GetComponent<Text>().text = "Someday You Will understand “Love”";
						currentQuestNameUI.GetComponent<Text>().text = "Someday You Will understand “Love”";
                        QuestManager.isTakingQuest = false;
                        questManager.rewardPlayer();
						questNameUI.SetActive(true);
						questInfoUI.SetActive(true);
						isTalking = true;
					}
					else if (104 == ID && QuestManager.ActiveQuestID == 4) {
						questUI.SetActive(true);
						QuestManager.isTakingQuest = false;
						questInfoUI.GetComponent<Text>().text = "Wow! That was faster than usual. Thank you, I was dying to get try this pizza... Wait, where’s my drink???? How do you expect for me to eat this without my diet cola…";
						questNameUI.GetComponent<Text>().text = "PIZZA PIZZA!!!";
						currentQuestNameUI.GetComponent<Text>().text = "PIZZA PIZZA!!!";
                        QuestManager.isTakingQuest = false;
                        questManager.rewardPlayer();
						questNameUI.SetActive(true);
						questInfoUI.SetActive(true);
						isTalking = true;
					}
				}
                commandTextUI.SetActive(false);
                commandLetterUI.SetActive(false);
            }
        }
    }
        
    void OnMouseExit()
    {
        commandLetterUI.SetActive(false);
        commandTextUI.SetActive(false);
//		_UIquest.SetActive(false);
	}

}
