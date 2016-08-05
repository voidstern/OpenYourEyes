using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	// reference to object that should be followed
	public GameObject ObjectToFollow;
	// y offset to Player
	public float XOffset = 0;
	public float YOffset = 0;
	public float ZOffset = 0;

	public float UnderBounderyY = 0;

	public bool FollowX = true;
	public bool FollowY = true;
	public bool FollowZ = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// position of the object that is following
		var sourcePosition = transform.position;
		// get position of the follow object
		var targetPosition = ObjectToFollow.transform.position;
		// calculate the offsets
		targetPosition.x += XOffset;
		targetPosition.y += YOffset;
		targetPosition.z += ZOffset;

		// copy position values only if we follow them
		if(FollowX) sourcePosition.x = targetPosition.x;
		if(FollowY && targetPosition.y >= UnderBounderyY) sourcePosition.y = targetPosition.y;
		if(FollowZ) sourcePosition.z = targetPosition.z;

		// set the new position to the following object
		transform.position = sourcePosition;
	}
}
