using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSignifier : MonoBehaviour
{
	private Interactable interactable;
	[SerializeField] private GameObject signifier;

	private void Awake() {
		interactable = GetComponent<Interactable>();
	}
	private void Update() {
		signifier.SetActive(interactable.ShowSignifier());
	}
}
