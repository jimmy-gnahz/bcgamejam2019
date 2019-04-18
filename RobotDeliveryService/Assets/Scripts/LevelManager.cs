using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public static LevelManager instance;

	public PlayerController player;

	private float levelStartTime;

	//[Header("Dialouge Manager")]
	[SerializeField] private UIElements uiMaster;
	private string npcName;
	private List<string> dialouges;
	private Quest receiveQuest;
	private Quest giveQuest;
	private Interactable currentInteractableObject;
	private bool dialougeManagerBusy = false;

	public void QueueDialouges(string npcName, List<string> dialouges) {
		giveQuest = null;
		receiveQuest = null;
		this.npcName = npcName;
		this.dialouges = dialouges;
		//Queue to UI; 
	}
	public void QueueDialouges(string npcName, 
		List<string> dialouges, bool isGivingQuest, Quest quest,
		Interactable interaction) {
		if (dialougeManagerBusy) {
			return;
		}
		currentInteractableObject = interaction;
		if (isGivingQuest) {
			receiveQuest = null;
			giveQuest = quest;
		}
		else {
			receiveQuest = quest;
			giveQuest = null;
		}
		this.npcName = npcName;
		this.dialouges = new List<string>();
		this.dialouges.AddRange(dialouges);
		dialougeManagerBusy = true;
		QueueNextDialouge();
	}

	public void QueueNextDialouge() {
		if (dialouges.Count > 0) {
			uiMaster.ShowDialougeUI();
			string currentLine = dialouges[0];

			uiMaster.QuestNameUI.GetComponent<Text>().text = npcName;
			uiMaster.QuestInfoUI.GetComponent<Text>().text = currentLine;

			dialouges.Remove(currentLine);
		}
		else if (receiveQuest) {
			uiMaster.ShowFinishQuestUI();
			uiMaster.QuestNameUI.GetComponent<Text>().text = npcName;
		}
		else if (giveQuest) {
			uiMaster.ShowGiveQuestUI();
		}
		else {
			EndConversation();
		}
	}
	public void EndConversation() {
		uiMaster.HideQuestUI();
		dialougeManagerBusy = false;
	}
	public void AcceptQuest() {
		player.CurrentQuest = giveQuest;
		currentInteractableObject.ClearInteraction();
		QueueNextDialouge();
		EndConversation();
	}
	public void RejectQuest() {
		QueueNextDialouge();
		EndConversation();
	}
	public void CollectRewards() {
		if (player.CurrentQuest == receiveQuest) {
			player.RewardQuest(receiveQuest);
			player.CurrentQuest = null;
			currentInteractableObject.ClearInteraction();
			EndConversation();
		}
	}


	private void Awake() {
		if (!instance) instance = this;
		else DestroyImmediate(this);

		player = FindObjectOfType<PlayerController>();

		levelStartTime = Time.time;
		SetUpInteractionColliderControl();
	}

	private void SetUpInteractionColliderControl() {
		List<Interactable> list = new List<Interactable>();
		list.AddRange(FindObjectsOfType<Interactable>());
		InteractionOperator.Interactables = list;
	}

	public float getTimeSinceStart() {
		return (Time.time - levelStartTime);
	}
}
