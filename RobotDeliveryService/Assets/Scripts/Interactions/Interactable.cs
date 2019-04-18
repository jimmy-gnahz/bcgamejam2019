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
