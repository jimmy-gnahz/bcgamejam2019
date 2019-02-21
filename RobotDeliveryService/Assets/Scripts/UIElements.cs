using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElements : MonoBehaviour
{
	public static UIElements inst;


	public  GameObject CommandDisplay;
	public  GameObject CommandText;
	public  GameObject UIQuest;
	public  GameObject Player;

	public  QuestManager questManager;
	public  GameObject UIQuestinfo;
	public  GameObject UIQuestname;
	public  GameObject QuestDisplayName;
	public  GameObject[] acceptDeclineButtons;

	void Awake()
    {
		if (inst != this) {
			if (inst == null) {
				inst = this;
			}
			else {
				DestroyImmediate(gameObject);
			}
		}
		
	}
}
