using UnityEngine;

public class NPCPointer : MonoBehaviour
{
	public GameObject pointerObject;

	private PlayerController player;
	private InteractionOperator io;

	private Transform target;

	private void Start() {
		player = GetComponent<PlayerController>();
		io = GetComponent<InteractionOperator>();
	}
	private void Update() {
		if (player.CurrentQuest) {
			target = io.FindAcceptQuestTarget(player.CurrentQuest);

			pointerObject.SetActive(true);

			Vector3 deltaPos = target.position - transform.position;
			float a = Mathf.Atan2(deltaPos.x, deltaPos.z) - transform.eulerAngles.y;
			pointerObject.transform.rotation = Quaternion.Euler(0, a, 0);

			
		}
	}
}
