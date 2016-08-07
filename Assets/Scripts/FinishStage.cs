using UnityEngine;
using System.Collections;

public class FinishStage : MonoBehaviour {
	[SerializeField] private float timer = 1;
	private PlayerMovement pm;
	private Animator anim;
	private bool hasEntered;
	private bool animFinished;


	// Use this for initialization
	void Start () {
		hasEntered = false;
		animFinished = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (hasEntered && timer > 0) {
			if (!animFinished) {
				pm.forceMove(1,0);
				timer -= Time.deltaTime;
			} else {
				anim.SetBool("isWinning", true);
			}
		} else if (timer <= 0) {
			pm.forceMove(0,0);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		pm = other.gameObject.GetComponent<PlayerMovement>();
		anim = other.gameObject.GetComponent<Animator>();
		pm.disableMovement();
		hasEntered = true;
		Debug.Log("Entered");
	}

	public void animationFinished() {
		anim.SetBool("isWinning", false);
		animFinished = true;
	}
}
