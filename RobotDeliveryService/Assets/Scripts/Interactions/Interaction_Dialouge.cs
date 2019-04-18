using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialouge Interaction", menuName = "Interactions/Dialouge", order = 0)]
public class Interaction_Dialouge : Interaction {

	[Header("NPC Interaction")]
	[SerializeField] string npcName = "new NPC";

	[SerializeField] private Quest receiveQuest;
	[SerializeField] [TextArea(2, 4)] private List<string> receiveQuestDialouge;
	private bool receiveQuestGiven = false;

	[SerializeField] private Quest giveQuest;
	[SerializeField] [TextArea(2, 8)] private List<string> dialouges;
	private bool giveQuestGiven = false;


	public void QueueDialouges (Interactable interactable) {
		if (receiveQuest) {
			LevelManager.instance.QueueDialouges(
				npcName, receiveQuestDialouge,
				false, receiveQuest, interactable);
		}
		else if (giveQuest) {
			LevelManager.instance.QueueDialouges(
				npcName, dialouges,
				true, giveQuest, interactable);
		}
	}

	public override void Interact(GameObject self, GameObject other) {
		QueueDialouges(self.GetComponent<Interactable>());
	}

	public override bool canInteract() {
		if ( giveQuest && !LevelManager.instance.player.CurrentQuest)
			return true;
		else if (receiveQuest && LevelManager.instance.player.CurrentQuest == receiveQuest) {
			return true;
		}
		return false;
	}
}
