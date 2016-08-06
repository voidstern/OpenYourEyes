using System.Net.NetworkInformation;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animation))]
public class PlayerMovement : InputController {
	[SerializeField] protected float jumpForce;
	[SerializeField] protected float moveForce;
	[SerializeField] protected int maxNumberOfJumps = 1;
	// [SerializeField] protected AudioClip jumpingSound;
	[SerializeField] private float maxVelocity = 10f;
	[SerializeField] private bool reverseSpriteX = false;
	[SerializeField] private Transform groundPointLeft;
	[SerializeField] private Transform groundPointRight;
	[SerializeField] private float groundPointRadius;
	[SerializeField] private LayerMask groundMask;

	protected Rigidbody2D rigidb;
	protected Animator animator;
	protected int availableJumps;
	protected bool didJump;
	protected float faceDirection = 1f;
	protected float reverseModX;

	protected bool isGroundedLeft;
	protected bool isGroundedRight;
	
	protected override void Awake() {
		base.Awake();
		rigidb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		availableJumps = 0;
		didJump = false;
		reverseModX = reverseSpriteX ? -1f : 1f;
	}
	// Update is called once per frame
	protected override void Update() {
		base.Update();
		isGroundedLeft = Physics2D.OverlapCircle(groundPointLeft.position, groundPointRadius, groundMask);
		isGroundedRight = Physics2D.OverlapCircle(groundPointRight.position, groundPointRadius, groundMask);
		if (inputJump && availableJumps > 0) {
			didJump = true;
		}
	}
	// FixedUpdate is called once per timeinterval (Edit->ProjSetting->Time), use for Physics
	void FixedUpdate() {
		// modify available jumps if player falls of edge, etc...
		// RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f);
		// if (hit.collider == null && availableJumps == maxNumberOfJumps) {
		if (!isGroundedLeft && !isGroundedRight) {
			availableJumps = maxNumberOfJumps - 1;
		}

		// handle if player did jump
		if (didJump) {
			rigidb.AddForce(Vector2.up * Utility.Physics2D.realForce(jumpForce));
			availableJumps--;
			didJump = false;
            animator.SetBool("isGrounded", false);
            animator.SetTrigger("isJumping");
			// GetComponent<AudioSource>().PlayOneShot(jumpingSound);
		}

		if (Mathf.Abs(inputHorizontal) > 0) {
			animator.SetBool("isWalking", true);
			faceDirection = inputHorizontal > 0 ? 1 : -1;
			faceDirection *= reverseModX;
		} else {
			animator.SetBool("isWalking", false);
		}

		transform.localScale = new Vector3(faceDirection, 1, 1);

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(inputHorizontal * rigidb.velocity.x < maxVelocity && inputHorizontal != 0) {
			// ... add a force to the player.
			rigidb.AddForce(Vector2.right * inputHorizontal * Utility.Physics2D.realForce(moveForce));

			// If the player's horizontal velocity is greater than the maxSpeed...
			if (Mathf.Abs(rigidb.velocity.x) > maxVelocity)
				// ... set the player's velocity to the maxSpeed in the x axis.
				rigidb.velocity = new Vector2(Mathf.Sign(rigidb.velocity.x) * maxVelocity, rigidb.velocity.y);
		}

		if (isGroundedRight || isGroundedLeft) {
			availableJumps = maxNumberOfJumps;
			animator.SetBool("isGrounded", true);
		}
		else
		{
			animator.SetBool("isGrounded", false);
		}

	}
	// handle Collision with Ground and restore available jumps
	/* void OnCollisionEnter2D(Collision2D collision) {
		//TODO: shorten the raycast so player cant jump when rolling over a hill and in air
		// RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2.5f);

		// if (hit.collider != null) {

	} */

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(groundPointLeft.position,groundPointRadius);
		Gizmos.DrawSphere(groundPointRight.position,groundPointRadius);
	}
}
