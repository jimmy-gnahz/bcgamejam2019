using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerGlidingPPChange : MonoBehaviour
{
	bool isFlying = false;
	public PostProcessVolume volume;
	[Range(0.01f,.1f)]
	public float increment = 0.1f;
	float focusDist;

	DepthOfField depthOfField;
    
	public void SetIsFlying(bool b) {
		isFlying = b;
	}

	private void Update() {
		volume.profile.TryGetSettings(out depthOfField);
		focusDist = depthOfField.focusDistance.value;
		if (isFlying) {
			float val = Mathf.Clamp(focusDist - increment, 2.6f, 3.05f);
			if (Mathf.Abs(val - focusDist) > 0.01f)
				depthOfField.focusDistance.value = val;
		}
		else {
			float val = Mathf.Clamp(focusDist + increment, 2.6f, 3.05f);
			if (Mathf.Abs(val - focusDist) > 0.01f)
				depthOfField.focusDistance.value = val;
		}
	}
}
