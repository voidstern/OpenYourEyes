using UnityEngine;
using System.Collections;

public class FinishStage : MonoBehaviour {
	[SerializeField] private float timer = 1;
	private PlayerMovement pm;
	private Animator anim;
	private bool hasEntered;
	private bool animFinished;
	private bool loading;
	private float startTimer;

	// Use this for initialization
	void Start () {
		hasEntered = false;
		animFinished = false;
		loading = false;
		startTimer = timer;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (hasEntered && timer > 0) {
			if (animFinished) {
				pm.forceMove(1,0);
				timer -= Time.deltaTime;
				if (!loading && timer < (startTimer / 2)) {
					loading = true;
					GetComponent<LevelLoader>().LoadNextLevel();
				}
			} else {
				anim.SetBool("isWinning", true);
			}
		} else if (timer <= 0) {
			pm.forceMove(0,0);
		}
    }

	void OnTriggerEnter2D(Collider2D other) {
		pm = GetComponent<PlayerMovement>();
		anim = GetComponent<Animator>();
		pm.disableMovement();
		pm.forceMove(0,0);
		hasEntered = true;
	}

	public void animationFinished() {
		Debug.Log(animFinished);
		anim.SetBool("isWinning", false);
		animFinished = true;
	}
}
