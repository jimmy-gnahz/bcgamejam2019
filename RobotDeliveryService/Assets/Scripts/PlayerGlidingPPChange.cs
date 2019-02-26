using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerGlidingPPChange : MonoBehaviour
{
	bool isFlying = false;
	// public PostProcessVolume volume;
	public Camera cam;
	//[Range(0.01f,.1f)]
	public float increment = 1f;
	float currentFov;

	DepthOfField depthOfField;
    
	public void SetIsFlying(bool b) {
		isFlying = b;
	}
	
	void Update() {
/*		//volume.profile.TryGetSettings(out depthOfField);
		currentFov = cam.fieldOfView;
		if (isFlying) {
			//Debug.Log("currentFov + increment = " + (currentFov + increment));
			float val = Mathf.Clamp(currentFov + increment, 40f, 50f);
			if (Mathf.Abs(val - currentFov) > 0.01f)
				Debug.Log(val);
				Camera.main.fieldOfView = 50;
		}
		else {
			float val = Mathf.Clamp(currentFov - increment, 40f, 50f);
			if (Mathf.Abs(val - currentFov) > 0.01f)
				depthOfField.focusDistance.value = val;
		}
*/	}
}
