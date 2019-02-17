using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElements : MonoBehaviour
{
	public static UIElements inst;


	public  GameObject CommandDisplay;
	public  GameObject CommandText;
	public  GameObject UIquest;
	public  GameObject Player;

	public  QuestManager questManager;
	public  GameObject UIQuestinfo;
	public  GameObject UIQuestname;
	public  GameObject Questdisplayname;
	public  GameObject[] acceptDeclineButtons;

	void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
