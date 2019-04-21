using System.Collections.Generic;
using UnityEngine;

public class InteractionOperator : MonoBehaviour {
	
	[SerializeField] private float reachRangeSquared;

	public static List<Interactable> Interactables { get; set; }
	private Interactable selectedInteractable;
	
	private void Update() {
		selectedInteractable = null;
		float farthestDist = reachRangeSquared;
		float distSq = 0;
		foreach (Interactable i in Interactables) {
			Collider iCollider = i.GetComponent<Collider>();
			Vector3 iClosestPoint = iCollider.ClosestPoint(transform.position);
			distSq = Vector3.Distance(transform.position, iClosestPoint);
			if (i.isActiveAndEnabled && 
				(transform.position.y - iClosestPoint.y < 0.08f) &&
				distSq < reachRangeSquared &&
				(!selectedInteractable || distSq < farthestDist) ) {
				
				farthestDist = distSq;
				selectedInteractable = i;
			}
		}
		DisplayInteractionUI();
	}

	public bool HasInteractableInRange() {
		return (selectedInteractable != null);
	}

	public void DisplayInteractionUI() {
		if (selectedInteractable && selectedInteractable.canInteract()) {
			UIElements.inst.UpdateInteractionUI("[SPACE]", selectedInteractable.InteractionText);
		}
		else {
			UIElements.inst.HideInteractionUI();
		}
	}

	public void Interact() {
		if(selectedInteractable && selectedInteractable.canInteract())
			selectedInteractable.Interact(gameObject);
	}

	public Transform FindAcceptQuestTarget(Quest q) {
		foreach (Interactable i in Interactables) {
			if (i.AcceptQuest(q))
				return i.transform;
		}
		return null;
	}
}