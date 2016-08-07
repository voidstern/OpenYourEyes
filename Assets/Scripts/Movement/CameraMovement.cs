using UnityEngine;
using System.Collections;

public class CameraMovement : InputController {
	[SerializeField] private float smooth = 2f;
	[SerializeField] private float maxTiltAngleZ = 30.0F;
	[SerializeField] private float maxTiltAngleX = 0F;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update();
	}
	
	void FixedUpdate() {
#if UNITY_STANDALONE
		Physics2D.gravity = Utility.Physics2D.gravity.normalized.y * transform.up * Utility.Physics2D.gravity.magnitude;
		
		float tiltAroundZ = inputHorizontal * maxTiltAngleZ;
		float tiltAroundX = inputVertical * maxTiltAngleX;
		Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
#endif
	}
}
