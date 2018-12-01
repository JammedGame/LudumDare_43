using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float runSpeed = 80f;
  
    public float jumpForce = 600f;
	public float moveSpeed = 5f;

	public int jumpsAllowed = 2;

    float dirX;

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

	void OnTriggerEnter2D(Collider2D collider)
	{
		jumpCount = 0;
	}
}