using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="New Quest", menuName = "New Quest")]
public class Quest : ScriptableObject
{
	[SerializeField] private string questName;
	[TextArea(3, 10)]
	[SerializeField] private string packageDescription;
	//public Texture2D icon; TODO

	[Header("Rewards")]
	[SerializeField] private int energy;
	// add other rewards here and in PlayerController.RewardQuest(Quest q);

	public int Energy { get { return energy; } }

	[SerializeField] private bool doGiveRewards = true;
}
