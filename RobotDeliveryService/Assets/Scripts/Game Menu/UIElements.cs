using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElements : MonoBehaviour
{
	public static UIElements inst;

	[Header("Interaction")]
	public GameObject commandTextUI;
	public GameObject commandKeyUI;

	public  GameObject UIQuest;
	public  GameObject Player;

	public  QuestManager questManager;

	public  GameObject QuestInfoUI;
	public  GameObject QuestNameUI;
	public  GameObject QuestDisplayNameUI;
	public GameObject AcceptButtonUI;
	public GameObject DeclineButtonUI;
	public GameObject RewardButtonUI;

	[Header("Health Display")]
	public Image healthBar;
	public Text healthText;
	[SerializeField] private Color regularColor;
	[SerializeField] private Color redColor;

	public void ShowDialougeUI() {
		UIQuest.SetActive(true);
		QuestNameUI.SetActive(true);
		QuestInfoUI.SetActive(true);
		AcceptButtonUI.SetActive(false);
		DeclineButtonUI.SetActive(false);
		RewardButtonUI.SetActive(false);
	}
	public void ShowGiveQuestUI() {
		ShowDialougeUI();
		AcceptButtonUI.SetActive(true);
		DeclineButtonUI.SetActive(true);
	}
	public void ShowFinishQuestUI() {
		ShowDialougeUI();
		RewardButtonUI.SetActive(true);
	}
	public void HideQuestUI() {
		QuestNameUI.SetActive(false);
		QuestInfoUI.SetActive(false);
		AcceptButtonUI.SetActive(false);
		DeclineButtonUI.SetActive(false);
		RewardButtonUI.SetActive(false);
		UIQuest.SetActive(false);
	}
	public void UpdateHealthUI(int energy, float ratio) {
		healthBar.fillAmount = energy * ratio;
		if (energy <= 15)
			healthText.color = redColor;
		else
			healthText.color = regularColor;
		healthText.text = energy.ToString();
	}
	public void UpdateInteractionUI (string key, string text) {
		commandTextUI.SetActive(true);
		commandKeyUI.SetActive(true);
		commandTextUI.GetComponent<Text>().text = text;
		commandKeyUI.GetComponent<Text>().text = key;
	}
	public void HideInteractionUI() {
		commandTextUI.SetActive(false);
		commandKeyUI.SetActive(false);
	}

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
