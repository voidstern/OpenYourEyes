using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BoxMovement : MonoBehaviour {
	[SerializeField] private GameObject mainCamera;

	private Rigidbody2D rigidb;

	// Use this for initialization
	void Start () {
		rigidb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(mainCamera.GetComponent<Grayscale>().enabled) {
			rigidb.constraints = RigidbodyConstraints2D.FreezeAll;
		} else {
			rigidb.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
}
