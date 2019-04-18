using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : ScriptableObject {

	public string selectText; // text for when interractable within range

	public virtual void Interact(GameObject self, GameObject other) {

	}

	public virtual bool canInteract() {
		return false;
	}
}
