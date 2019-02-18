using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public float _Distance;
//    public Dialog _Dialog;
    public GameObject _CommandDisplay;
    public GameObject _CommandText;
    public GameObject _UIquest;
    public GameObject _Player;

    public GameObject _Rooftop;

    public QuestManager questManager;
    public static bool _Talk = false;

    // Building ID in range of 1000-9999.
    // Quest Giving NPC: ID in range of 0 - 99.
    // Receiving NPC: ID in range of 100 - 199
    public int ID;

    public GameObject _UIQuestinfo;
    public GameObject _UIQuestname;
    public GameObject _Questdisplayname;

    GameObject _Object;
    public GameObject[] buttons;

   /* private void Start()
    {
        /*      _Object = this.gameObject;
              _CommandDisplay = UIElements.inst.CommandDisplay;
              _CommandDisplay.SetActive(false);
              _CommandText = UIElements.inst.CommandText;
              print(_CommandText);
              _CommandText.SetActive(false);
              _UIquest = UIElements.inst.UIQuest;
              _UIQuestinfo = UIElements.inst.UIQuestinfo;
              _UIQuestname = UIElements.inst.UIQuestname;
              _Questdisplayname = UIElements.inst.QuestDisplayName;
              _Player = UIElements.inst.Player;
                     if (tag == "Building")
			_Rooftop = transform.GetChild(0).gameObject;


*/
    private void Awake()
    {
        _Object = gameObject;

        if (_CommandDisplay == null)
            _CommandDisplay = GameObject.FindGameObjectWithTag("ActionText");
        if (_CommandText == null)
		_CommandText = GameObject.FindGameObjectWithTag("KeyText");
        if (_UIquest == null)
			_UIquest = GameObject.FindGameObjectWithTag("UIquest");
		if (_UIQuestinfo == null)
			_UIQuestinfo = GameObject.FindGameObjectWithTag("UIQuestinfo");
		if (_UIQuestname == null)
			_UIQuestname = GameObject.FindGameObjectWithTag("UIQuestname");
        if (_Questdisplayname == null)

			_Questdisplayname = GameObject.FindGameObjectWithTag("Questdisplayname");
		if (_Player == null)
			_Player = GameObject.FindGameObjectWithTag("Player");

        //if (tag == "Building")
        //{
        //    _Rooftop = transform.parent.GetChild(1).gameObject;
        //    print(_Rooftop.tag);
        //}
    }

 
	

    void Update()
    {
        
      //  _Distance = PlayerCasting.Distancefromtarget;
        Vector3 plainerPos = new Vector3(transform.position.x, 0, transform.position.z);
        _Distance = (_Player.transform.position - plainerPos).magnitude;
		if (QuestManager.isTakingQuest ) {
		
			if (Input.GetButtonDown("Accept") && _Talk) {
				_UIquest.SetActive(false);
				_Questdisplayname.GetComponent<Text>().text = "Quest: ";
				_Questdisplayname.SetActive(true);
				QuestManager.isTakingQuest = false;
			}
		}
        else if (Input.GetButtonDown("Accept") && _Talk)
        {
            AcceptQuest();
            _Talk = false;
        }
		else if(Input.GetButtonDown("Return") && _Talk)
        {
            DeniedQuest();
            _Talk = false;
			QuestManager.isTakingQuest = false;

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
        if (_Distance <= 3 && 
			_Player.transform.position.y < 1.5 && 
			!_Talk)
        {
            if (_Object.tag.Contains("NPC"))
            {
                _CommandDisplay.GetComponent<Text>().text = "Talk";
                _CommandText.GetComponent<Text>().text = "E";
            }

            else if (_Object.tag.Equals("Building"))
            {
                _CommandDisplay.GetComponent<Text>().text = "Use Elevator";
                _CommandText.GetComponent<Text>().text = "E";
            }

            _CommandDisplay.SetActive(true);
            _CommandText.SetActive(true);
        }

        if (Input.GetButtonDown("Action"))
        {
            //          if (_Distance <= 3 && _Player.transform.position.y < 1.5 && !_Talk)
            if (_Distance <= 3 && !_Talk)
            {
                if (_Object.tag.Contains("Building"))
                {

//
                    _Player.transform.Translate(Vector3.up * _Rooftop.transform.position.y);
                    _Player.transform.SetPositionAndRotation(_Rooftop.transform.position, _Player.transform.rotation);
                    //                    FindObjectOfType<QuestManager>().HideLight();
//                    questManager.HideLight();
				}
                else
                {   // Assume object is either building or NPC
                    
					Debug.Log(" is ID : " + ID);
                    if (ID >= 100 && ID < 200)
                    {
                        foreach (GameObject go in buttons)
                        {
                            go.SetActive(false);
                        }

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
						_UIquest.SetActive(true);
						_UIQuestinfo.GetComponent<Text>().text = "Dear K1ndess ROBO, \nHello, I need you to grab this manga…it’s called Undying lover to Tentacle-senpai.I’m unable to buy it because of reason. So please deliver that manga to me…< 3";
                        _UIQuestname.GetComponent<Text>().text = "The Otaku’s needs ";
						_Questdisplayname.GetComponent<Text>().text = "The Otaku’s needs ";
                        QuestManager.ActiveQuestID = ID;

						_UIQuestname.SetActive(true);
						_UIQuestinfo.SetActive(true);
						_Talk = true;
					}
                    else if (ID == 1)
                    {
						_UIquest.SetActive(true);
                        _UIQuestinfo.GetComponent<Text>().text = "Hello K1ndess Robot, /nPlease take this boy and bring him back home. He needs to attend his piano lessons in three minutes. He’s a busy boy and he’s already three months old…. THREE MONTHS!!!!...Driving him around just waste too much precious time where he could be studying math or practicing violin, or composing the next world famous symphony….Please bring him home safely and FAST!!!!!";

						_UIQuestname.GetComponent<Text>().text = "Bring my Son back home";
                        _Questdisplayname.GetComponent<Text>().text = "Bring my Son back home";
                        QuestManager.ActiveQuestID = ID;
                        _Talk = true;
						_UIQuestname.SetActive(true);
						_UIQuestinfo.SetActive(true);
						_Talk = true;
					}
					else if (ID == 2) {
						_UIquest.SetActive(true);
						_UIQuestinfo.GetComponent<Text>().text = "Hello K1ndess Robot,\nA package from the post office has just arrived and I need you to deliver it to me immediately.Just to make sure you grab everything, the package contains variety textbooks such as Advance Vector Calculus, Theoretical and Experimental Physics, Quantum Computing…. my dear son needs to start early so when he grows up, he’ll be set to become a billionaire.Just imagine it, my son, the next Bill Gates….. NOW HURRY UP, THE MORE TIME YOU WASTE, THE LESS TIME FOR MY SON TO PREPARE FOR HIS LIFE GOAL!!!!!!!!";
					  _UIQuestname.GetComponent<Text>().text = "Getting ahead in life";
						_Questdisplayname.GetComponent<Text>().text = "Getting ahead in life";
						QuestManager.ActiveQuestID = ID;
						_Talk = true;
						_UIQuestname.SetActive(true);
						_UIQuestinfo.SetActive(true);
						_Talk = true;
					}
					else if (ID == 3) {
						_UIquest.SetActive(true);
						_UIQuestinfo.GetComponent<Text>().text = "Hello K1ndess Robot, \nI’ve written a confession letter to my crush and I need you to deliver to him. He lives across the city and I’m too embarrassed to give it to him in person. I’ve poured my whole heart into that letter so please give it to him. Here take it. ";
						_UIQuestname.GetComponent<Text>().text = "Someday You Will understand “Love”";
						_Questdisplayname.GetComponent<Text>().text = "Someday You Will understand “Love”";
						QuestManager.ActiveQuestID = ID;
						_Talk = true;
						_UIQuestname.SetActive(true);
						_UIQuestinfo.SetActive(true);
						_Talk = true;
					}
					else if (ID == 4) {
						_UIquest.SetActive(true);
						_UIQuestinfo.GetComponent<Text>().text = "Greeting K1ndness Robot, \nMy current delivery boy has just broken his legs trying to balance 10 pizza boxes on his head. Don’t worry, the pizzas in the boxes were fine. Can you please take over for him and deliver this pizza to our next customer, Thank you.";
						_UIQuestname.GetComponent<Text>().text = "PIZZA PIZZA!!!";
						_Questdisplayname.GetComponent<Text>().text = "PIZZA PIZZA!!!";
						QuestManager.ActiveQuestID = ID;
						_Talk = true;
						_UIQuestname.SetActive(true);
						_UIQuestinfo.SetActive(true);
						_Talk = true;
					}

					else if (100 == ID && QuestManager.ActiveQuestID == 0) {
						_UIquest.SetActive(true);
						_UIQuestinfo.GetComponent<Text>().text = "Oh boy, I can’t wait to read this, thank you ";
                        _UIQuestname.GetComponent<Text>().text = "The Otaku’s needs ";
                        _Questdisplayname.GetComponent<Text>().text = "The Otaku’s needs ";
                        _Talk = true;
                        questManager.rewardPlayer(ID);
						_UIQuestname.SetActive(true);
						_UIQuestinfo.SetActive(true);
						_Talk = true;

					}
                    else if (101 == ID && QuestManager.ActiveQuestID == 1) {
						_UIquest.SetActive(true);
						QuestManager.isTakingQuest = false;
                        _UIQuestinfo.GetComponent<Text>().text = "Oh my, you already 0.5347 second late for your lessons. Thank you K1ndness";
                        _UIQuestname.GetComponent<Text>().text = "Bring my Son back home";
                        _Questdisplayname.GetComponent<Text>().text = "Bring my Son back home";
                        _Talk = true;
                        questManager.rewardPlayer(ID);
						_UIQuestname.SetActive(true);
						_UIQuestinfo.SetActive(true);
						_Talk = true;
					}
					else if (102 == ID && QuestManager.ActiveQuestID == 2) {
						_UIquest.SetActive(true);
						QuestManager.isTakingQuest = false;
						_UIQuestinfo.GetComponent<Text>().text = "Thank you K1ndness for deliver these to me. Because of you, my son is one step closer to becoming a billionaire.";
						_UIQuestname.GetComponent<Text>().text = "Getting ahead in life";
						_Questdisplayname.GetComponent<Text>().text = "Getting ahead in life";
						_Talk = true;
						questManager.rewardPlayer(ID);
						_UIQuestname.SetActive(true);
						_UIQuestinfo.SetActive(true);
						_Talk = true;
					}
					else if (103 == ID && QuestManager.ActiveQuestID == 3) {
						_UIquest.SetActive(true);
						QuestManager.isTakingQuest = false;
						_UIQuestinfo.GetComponent<Text>().text = "Oh? A letter for me. I wonder who it’s from? ";
						_UIQuestname.GetComponent<Text>().text = "Someday You Will understand “Love”";
						_Questdisplayname.GetComponent<Text>().text = "Someday You Will understand “Love”";
						_Talk = true;
						questManager.rewardPlayer(ID);
						_UIQuestname.SetActive(true);
						_UIQuestinfo.SetActive(true);
						_Talk = true;
					}
					else if (104 == ID && QuestManager.ActiveQuestID == 4) {
						_UIquest.SetActive(true);
						QuestManager.isTakingQuest = false;
						_UIQuestinfo.GetComponent<Text>().text = "Wow! That was faster than usual. Thank you, I was dying to get try this pizza... Wait, where’s my drink???? How do you expect for me to eat this without my diet cola…";
						_UIQuestname.GetComponent<Text>().text = "PIZZA PIZZA!!!";
						_Questdisplayname.GetComponent<Text>().text = "PIZZA PIZZA!!!";
						_Talk = true;
						questManager.rewardPlayer(ID);
						_UIQuestname.SetActive(true);
						_UIQuestinfo.SetActive(true);
						_Talk = true;
					}
				}

 //               _CommandDisplay.
                _CommandDisplay.SetActive(false);
                _CommandText.SetActive(false);

            }
        }


    }

    void OnMouseExit()
    {
        _CommandText.SetActive(false);
        _CommandDisplay.SetActive(false);
//		_UIquest.SetActive(false);
	}

}
