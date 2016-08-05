using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	protected bool inputJump;
	protected float inputHorizontal;
	protected float inputVertical;

	protected virtual void Awake () {
		inputJump = false;
		inputHorizontal = 0f;
	}

	// Update is called once per frame
	protected virtual void Update() {
		
#if UNITY_STANDALONE
		inputJump = Input.GetButtonDown("Jump");
		inputHorizontal = Input.GetAxis("Horizontal");
		inputVertical = Input.GetAxis("Vertical");
#elif UNITY_IOS || UNITY_ANDROID
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			inputJump = true;
		} else {
			inputJump = false;
		}

		inputHorizontal = Input.acceleration.x;
		inputVertical = Input.acceleration.y;
		//Input.gyro.gravity
#endif
	}
}
