﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : InputController {
	[SerializeField] protected float jumpForce;
	[SerializeField] protected int maxNumberOfJumps = 1;
	// [SerializeField] protected AudioClip jumpingSound;
	[SerializeField] private float maxSpeed = 10f;
	[SerializeField] private bool reverseSpriteX = false;

	protected Rigidbody2D rigidb;
	//protected Animator animator;
	protected int availableJumps;
	protected bool didJump;
	protected float faceDirection = 1f;
	protected float reverseModX;
	
	protected override void Awake() {
		base.Awake();
		rigidb = GetComponent<Rigidbody2D>();
		//animator = GetComponent<Animator>();
		availableJumps = 0;
		didJump = false;
		reverseModX = reverseSpriteX ? -1f : 1f;
	}
	// Update is called once per frame
	protected override void Update() {
		base.Update();
		if (inputJump && availableJumps > 0) {
			didJump = true;
		}
	}
	// FixedUpdate is called once per timeinterval (Edit->ProjSetting->Time), use for Physics
	void FixedUpdate() {
		// modify available jumps if player falls of edge, etc...
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
		if (hit.collider == null && availableJumps == maxNumberOfJumps) {
			availableJumps = maxNumberOfJumps - 1;
		}

		// handle if player did jump
		if (didJump) {
			rigidb.AddForce(Vector2.up * Utility.Physics2D.realForce(jumpForce));
			availableJumps--;
			didJump = false;
			// GetComponent<Animator>().SetTrigger("isJumping");
			// GetComponent<AudioSource>().PlayOneShot(jumpingSound);
		}

		if (Mathf.Abs(inputHorizontal) > 0) {
			// animator.SetBool("isWalking", true);
			faceDirection = inputHorizontal > 0 ? 1 : -1;
			faceDirection *= reverseModX;
		} else {
			// animator.SetBool("isWalking", false);
		}

		transform.localScale = new Vector3(faceDirection, 1, 1);
		rigidb.velocity = new Vector2(inputHorizontal * maxSpeed, rigidb.velocity.y);
	}
	// handle Collision with Ground and restore available jumps
	void OnCollisionEnter2D(Collision2D collision) {
		//TODO: shorten the raycast so player cant jump when rolling over a hill and in air
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);

		if (hit.collider != null) {
			availableJumps = maxNumberOfJumps;
		}
	}
}
