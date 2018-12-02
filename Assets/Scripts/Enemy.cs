﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour {

	private bool m_FacingRight = true;
	private Vector3 m_Velocity = Vector3.zero;

	private Rigidbody2D rb;
	[SerializeField] private float moveSpeed = 3f;
	[SerializeField] public float defaultMoveSpeed = 3f;

	private float dirX = 1;
	[SerializeField] private BoxCollider2D boxCollider;

	private bool doFlip = false;
	private bool collisionEnter = false;

	public Animator animator;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
		moveSpeed = defaultMoveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		ContactFilter2D filter = new ContactFilter2D();
		LayerMask mask = LayerMask.GetMask("Tiles", "Enemy");
		filter.SetLayerMask(mask);
		filter.useLayerMask = true;
		Collider2D[] result =  new Collider2D[10];
		int numOfColiders = boxCollider.OverlapCollider(filter, result);

		LayerMask characterMask = LayerMask.GetMask("Player", "Enemy");
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(0.7f * dirX, 0, 0), Vector2.right * dirX, 10f, characterMask);
		Debug.DrawRay(transform.position + new Vector3(0.7f * dirX, 0, 0), Vector2.right * dirX, Color.green); // TODO: Delete DrawRay
		
		bool found_player = false;
		if (hits.Any(hit => hit.collider.gameObject.CompareTag("Player"))) {
			found_player = true;
			if (!hits[0].collider.gameObject.CompareTag("Player")) {
				moveSpeed = 0f;
			}
		} else {
			moveSpeed = defaultMoveSpeed;
		}
		doFlip = (
			!found_player && 
			(numOfColiders == 0 || 
			result.Any(collider => collider && collider.gameObject.CompareTag("Enemy")))			
		);
		animator.SetFloat("Speed", moveSpeed);
	}

	void FixedUpdate() {
		if (doFlip) Flip();
        rb.velocity = new Vector2(1 * moveSpeed * dirX, 0);
	}

	private void Flip() {
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
		dirX = dirX * -1;
		transform.Rotate(0f, 180f, 0f);
	}
}
