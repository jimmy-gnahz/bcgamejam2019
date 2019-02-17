using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    public int Trigger;
    public int Time;
    public GameObject[] QuestList;
    // Update is called once per frame
    void Update()
    {   
        Trigger = Random.Range(0, 100);
        if(Trigger > 10)
        {
            GameObject quest = QuestList[Random.Range(0, QuestList.Length)];
            quest.SetActive(true);
        }
       
    }
}
