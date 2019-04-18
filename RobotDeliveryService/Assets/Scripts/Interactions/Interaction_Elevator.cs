using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Elevator Interaction", menuName = "Interactions/Elevator", order = 1)]
public class Interaction_Elevator : Interaction {

	private Transform rooftop;
	private void MoveOtherToRoofTop(Transform self, GameObject other) {
		for (int i = 0; i < self.childCount; i++) {
			Transform t = self.GetChild(i);
			if (t.tag == "Roof") rooftop = t;
		}
		if (!rooftop) {
			Debug.LogError("Transform does not have child tagged 'Roof'!");
			return;
		}

		other.GetComponent<Rigidbody>().MovePosition(rooftop.transform.position);
	}

	public override void Interact(GameObject self, GameObject other) {
		MoveOtherToRoofTop(self.transform, other);
	}

	public override bool canInteract() {
		return true;
	}
}
