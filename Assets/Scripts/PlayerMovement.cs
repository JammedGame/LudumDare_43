using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float runSpeed = 80f;
  
    public float jumpForce = 1200f;
	public float moveSpeed = 5f;
	public float fireSpeed = 600f;

    //skill unlocks

   
    public bool canFire = false;
    public bool canClearScreen = false;
    public bool canGlide = false;

    // Skill costs
    public float doubleJumpCost = 3.5f;
    public float glideCostPerSecond = 2f;
    public float rangeAttackCost = 25f;
    public float clearScreenCost = 50f;

    public GameObject clearScreenCanvas;
    private float clearScreenTimeout = 0f;

    private float glideTime = 0f;

    public GameObject firePosition;

	public int jumpsAllowed = 1;

    public Animator animator;

    float dirX;

    private bool doFlip = false;
    private bool fire = false;
    private bool m_FacingRight = false;

    Rigidbody2D rb;

    public int jumpCount = 0;
    bool glide = false;
    bool isJumping = false;
    bool clearScreen = false;
    
	private bool doAttack = false;
	private bool isAttacking = false;
    public float attackSpeed = 1f; 
	private float timeRemainingToAttack = 0f;
    public float dealDamage = 50f;

    private EdgeCollider2D edgeCollider;
    public Camera camera;
    private float clearAnimTime = 0f;

    public bool animStart = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    void Update() {
        timeRemainingToAttack -= Time.deltaTime;
        if (rb.velocity.y < -1) {
            animator.SetBool("Fall", true);
            animator.SetBool("Jump", false);
        } else if (rb.velocity.y >= -1 && rb.velocity.y <= 1) {
            animator.SetBool("Fall", false);
            animator.SetBool("Jump", false);
        } else if (rb.velocity.y > 1) {
            animator.SetBool("Jump", true);
            animator.SetBool("Fall", false);
        }

        if (Input.GetButtonDown("Fire2") && canFire) {
            animator.SetBool("Attack", true);
            fire = true;
        }
        if (Input.GetButtonDown("ClearScreen")) {
            clearScreen = true;
        }
        if (jumpCount < jumpsAllowed && Input.GetButtonDown("Jump"))
        {
            // animator.SetBool("Jump", true);
         
          
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

        if (!isAttacking && Input.GetButtonDown("Fire3")) {
            doAttack = true;
        }
		animator.SetFloat("Speed", Mathf.Abs(dirX));
    }

    void FixedUpdate() {
        if (fire) Fire();
        if (clearScreen && !animStart && canClearScreen) {
            animator.SetBool("Clear", true);
            clearAnimTime = 0.5f;
            animStart = true;
        }

        if (clearScreen && animStart && canClearScreen) {
            if (clearAnimTime > 0) {
                clearAnimTime -= Time.deltaTime;
            } else {
                animStart = false;
                ClearScreen();
            }
        }
        if (doFlip) Flip();
        if (isJumping) {
	        Jump();
            // animator.SetBool("Jump", false);
            isJumping = false;
        }

        if (doAttack) {
            animator.SetBool("Attack", true);
            isAttacking = true;
            doAttack = false;
        }

        
		if (isAttacking && animator.GetCurrentAnimatorStateInfo(0).IsName("Shaman_Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .9) {
			Attack();
		}

        if (glide && canGlide && rb.velocity.y < 0) {
            animator.SetBool("Glide", true);
            rb.gravityScale = 0.3f;
            TakeDamage(glideCostPerSecond * Time.deltaTime);
            FindObjectOfType<AudioManager>().PlayContinuous("PlayerGlide");
        } else {
            rb.gravityScale = 3;
            animator.SetBool("Glide", false);
            FindObjectOfType<AudioManager>().Stop("PlayerGlide");
        }

        if (clearScreenTimeout > 0) {
            clearScreenTimeout -= Time.deltaTime;
        } else {
            clearScreenCanvas.SetActive(false);
        }
        if (dirX == 0f || isJumping || glide) {
            FindObjectOfType<AudioManager>().Stop("PlayerWalk");
        } else {
            FindObjectOfType<AudioManager>().PlayContinuous("PlayerWalk");
        }
        rb.velocity = new Vector2(dirX, rb.velocity.y);


    }

    private void ClearScreen()
    {
        GameObject[] enemies;
        clearScreenCanvas.SetActive(true);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Vector3 point = camera.WorldToViewportPoint(enemy.transform.position);
            if (point.x > 0 && point.x < 1 && point.y > 0 && point.y < 1) {
                enemy.GetComponent<Health>().Death();
            }
        }
        FindObjectOfType<AudioManager>().Play("PlayerClearScreen");
        TakeDamage(clearScreenCost);
        clearScreenTimeout = 0.4f;
        clearScreen = false;
        animator.SetBool("Clear", false);
        
    }

    private void Fire() {
		var bullet = Instantiate(Resources.Load("Prefabs/SwordProjectile")) as GameObject;
        FindObjectOfType<AudioManager>().Play("PlayerRangeAttack");
		
		bullet.transform.position = firePosition.transform.position;
		bullet.transform.forward = firePosition.transform.forward;
        TakeDamage(rangeAttackCost);
		bullet.GetComponent<Rigidbody2D>().AddForce(fireSpeed * bullet.transform.right);
        fire = false;
        
        animator.SetBool("Attack", false);
    }

    void Flip() {
        m_FacingRight = !m_FacingRight;
		transform.Rotate(0f, 180f, 0f);
        doFlip = false;
    }

    void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, 0f); 
        rb.AddForce(Vector2.up * jumpForce);
        FindObjectOfType<AudioManager>().Play("PlayerJump");
        if (jumpCount == 2) TakeDamage(doubleJumpCost);
    }

	void OnTriggerEnter2D(Collider2D collider) {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
		ContactFilter2D filter = new ContactFilter2D();
		LayerMask mask = LayerMask.GetMask("Tiles");
		filter.SetLayerMask(mask);
		filter.useLayerMask = true;
		Collider2D[] resultsBoxCollider =  new Collider2D[1];
		int tiles = boxCollider.OverlapCollider(filter, resultsBoxCollider);
        if (tiles > 0) {
            jumpCount = 0;
            FindObjectOfType<AudioManager>().Play("PlayerLand");
        }
	}

    void TakeDamage(float damageAmount) {
        GetComponent<Health>().TakeDamage(damageAmount);
    }

    void Attack() {
        animator.SetBool("Attack", false);
        
		ContactFilter2D filter = new ContactFilter2D();
		LayerMask mask = LayerMask.GetMask("Enemy");
		filter.SetLayerMask(mask);
		filter.useLayerMask = true;
		Collider2D[] resultsEdgeCollider =  new Collider2D[1];
		int enemies = edgeCollider.OverlapCollider(filter, resultsEdgeCollider);


        if (enemies > 0) {
            resultsEdgeCollider[0].gameObject.GetComponent<Health>().TakeDamage(dealDamage);
			FindObjectOfType<AudioManager>().Play("PlayerAttack");
        }
        isAttacking = false;
    }
}
