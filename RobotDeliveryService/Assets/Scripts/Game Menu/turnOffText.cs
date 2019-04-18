using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffText : MonoBehaviour
{

    public GameObject _CommandDisplay;
    public GameObject _CommandText;

    public GameObject _UIquest;
    public GameObject _UIQuestinfo;
    public GameObject _UIQuestname;
    public GameObject _Questdisplayname;

    private void Awake()
    {
        _CommandDisplay = GameObject.FindGameObjectWithTag("ActionText");
        _CommandText = GameObject.FindGameObjectWithTag("KeyText");

        //quest
        _UIquest = GameObject.FindGameObjectWithTag("UIquest");
        _UIQuestinfo = GameObject.FindGameObjectWithTag("UIQuestinfo");
        _UIQuestname = GameObject.FindGameObjectWithTag("UIQuestname");

    }
    // Start is called before the first frame update
    void Start()
    {
        _CommandDisplay.SetActive(false);
        _CommandText.SetActive(false);
        _UIquest.SetActive(false);
        _UIQuestinfo.SetActive(false);
        _UIQuestname.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
