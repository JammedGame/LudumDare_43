using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public float runSpeed = 80f;
  
    float dirX;

    [SerializeField]
    float jumpForce = 600f, moveSpeed = 5f;

    Rigidbody2D rb;

    bool doubleJumpAllowed = false;
    bool onTheGround = false;
    bool glide = false;
    bool isJumping = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rb.velocity.y == 0) {
            onTheGround = true;
            doubleJumpAllowed = true;
        } else {
            onTheGround = false;
        }
        
        if (doubleJumpAllowed && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            if (!onTheGround && doubleJumpAllowed) {
                doubleJumpAllowed = false;
            }
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

    }

    void FixedUpdate() {
        if (isJumping) {
            Jump();
            isJumping = false;
        }

        if (glide && rb.velocity.y < 0) {
            rb.gravityScale = 0.6f;
        } else {
            rb.gravityScale = 3;
        }
        
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); 
        rb.AddForce(Vector2.up * jumpForce);
    }
}
/*	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		if (Input.GetButtonDown("Jump")) {
			jump = true;
		}
		
	}

	void FixedUpdate () {

		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
		
	}
}
*/