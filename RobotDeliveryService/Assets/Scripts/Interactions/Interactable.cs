using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

	[SerializeField] private Interaction interaction;

	public string InteractionText { get { return interaction.selectText; } }

	private void Awake() {
	}

	public void Interact (GameObject from) {
		interaction.Interact(gameObject, from);
	}

	public bool canInteract() {
		if (!interaction) return false;
		return interaction.canInteract();
	}

	public bool AcceptQuest(Quest q) {
		try {
			Interaction_Dialouge i = (Interaction_Dialouge) interaction;
			if (!i) return false;
			return i.AcceptsQuest(q);
		}
		catch (System.Exception) {

			throw;
		}
	}

	public void ClearInteraction() {
		interaction = null;
	}

	public bool ShowSignifier() {
		if (interaction) {
			return interaction.canInteract();
		}
		else
			return false;
	}
}
