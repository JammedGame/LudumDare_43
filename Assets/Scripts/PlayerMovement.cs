﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float runSpeed = 80f;
  
    public float jumpForce = 1200f;
	public float moveSpeed = 5f;
	public float fireSpeed = 600f;

    // Skill costs
    public float doubleJumpCost = 3.5f;
    public float glideCostPerSecond = 2f;
    public float rangeAttackCost = 24f;
    public float clearScreenCost = 49f;

    private float glideTime = 0f;

    public GameObject firePosition;

	public int jumpsAllowed = 2;

    public Animator animator;

    float dirX;

    private bool doFlip = false;
    private bool fire = false;
    private bool m_FacingRight = false;

    Rigidbody2D rb;

    public int jumpCount = 0;
    bool glide = false;
    bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2")) {
            fire = true;
        }
        if (jumpCount < jumpsAllowed && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
			jumpCount++;
        }

        dirX = Input.GetAxis("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Glide"))
        {
            glide = true;
        }
        if (Input.GetButtonUp("Glide"))
        {
            glide = false;
        }
        if (dirX > 0 && !m_FacingRight) {
            doFlip = true;
        } else if (dirX < 0 && m_FacingRight) {
            doFlip = true;
        }
    }

    void FixedUpdate() {
        if (fire) Fire();
        if (doFlip) Flip();
        if (isJumping) {
	        Jump();
            isJumping = false;
        }

        if (glide && rb.velocity.y < 0) {
            rb.gravityScale = 0.6f;
            TakeDamage(glideCostPerSecond * Time.deltaTime);
        } else {
            rb.gravityScale = 3;
        }
        
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    }

    private void Fire() {
		
		var bullet = Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject;
		
		bullet.transform.position = firePosition.transform.position;
		bullet.transform.forward = firePosition.transform.forward;
        TakeDamage(rangeAttackCost);
		bullet.GetComponent<Rigidbody2D>().AddForce(fireSpeed * bullet.transform.right);
        GetComponent<Health>().TakeDamage(rangeAttackCost);
        fire = false;
    }

    void Flip() {
        m_FacingRight = !m_FacingRight;
		transform.Rotate(0f, 180f, 0f);
        doFlip = false;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); 
        rb.AddForce(Vector2.up * jumpForce);
        if (jumpCount == 2) TakeDamage(doubleJumpCost);
    }

	void OnTriggerEnter2D(Collider2D collider)
	{
		jumpCount = 0;
	}

    void TakeDamage(float damageAmount) {
        GetComponent<Health>().TakeDamage(damageAmount);
    }
}