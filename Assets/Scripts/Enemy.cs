using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour {

	private Rigidbody2D rb;
	[SerializeField] private float moveSpeed = 3f;
	[SerializeField] public float defaultMoveSpeed = 3f;

	private float dirX = 1;
	[SerializeField] private BoxCollider2D boxColliderTiles;
	[SerializeField] private EdgeCollider2D edgeColliderWallEnemy;

	private bool doFlip = false;

	public Animator animator;

	private bool inRangeOfPlayer;
	private Health playerHealth;
	private bool startAttack = false;

	public float attackSpeed = 3f;
	private float timeRemainingToAttack = 0f;

	public float dealDamage = 5f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
		boxColliderTiles = GetComponent<BoxCollider2D>();
		edgeColliderWallEnemy = GetComponent<EdgeCollider2D>();
		moveSpeed = defaultMoveSpeed;
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		timeRemainingToAttack -= Time.deltaTime;
		


		ContactFilter2D filter = new ContactFilter2D();
		LayerMask mask = LayerMask.GetMask("Tiles");
		filter.SetLayerMask(mask);
		filter.useLayerMask = true;
		Collider2D[] resultsBoxCollider =  new Collider2D[1];
		int walkingTiles = boxColliderTiles.OverlapCollider(filter, resultsBoxCollider);

		ContactFilter2D filter2 = new ContactFilter2D();
		LayerMask mask2 = LayerMask.GetMask("Tiles", "Enemy", "Player");
		filter2.SetLayerMask(mask2);
		filter2.useLayerMask = true;
		Collider2D[] resultsEdgeCollider =  new Collider2D[10];
		int colidedWithWallOrEnemy = edgeColliderWallEnemy.OverlapCollider(filter2, resultsEdgeCollider);

		LayerMask characterMask = LayerMask.GetMask("Player", "Enemy");
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(0.7f * dirX, 0, 0), Vector2.right * dirX, 10f, characterMask);
		Debug.DrawRay(transform.position + new Vector3(0.7f * dirX, 0, 0), Vector2.right * dirX, Color.green); // TODO: Delete DrawRay
		
		bool found_player = false;
		if (hits.Any(hit => hit.collider.gameObject.CompareTag("Player"))) {
			found_player = true;
			inRangeOfPlayer = resultsEdgeCollider.Any(collider => collider && collider.gameObject.CompareTag("Player"));
			// animator.SetBool("Attack", inRangeOfPlayer);
			// StartAttack();
			
			if (!hits[0].collider.gameObject.CompareTag("Player") || inRangeOfPlayer || startAttack) {
				moveSpeed = 0f;
			} else {
				moveSpeed = defaultMoveSpeed;
			}
		} else {
			moveSpeed = defaultMoveSpeed;
		}
		doFlip = (
			walkingTiles == 0 || (!found_player && (
				resultsEdgeCollider.Any(collider => 
					collider && (
						collider.gameObject.CompareTag("Enemy") || 
						collider.gameObject.CompareTag("Tiles")
					)
				)
			))
		);
		animator.SetFloat("Speed", moveSpeed);
	}

	void FixedUpdate() {
		if (doFlip) Flip();
        rb.velocity = new Vector2(1 * moveSpeed * dirX, 0);


		if (startAttack && animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .9) {
			Attack();
		}

		if (inRangeOfPlayer && timeRemainingToAttack <= 0f) {
			timeRemainingToAttack = attackSpeed;
			if (inRangeOfPlayer) StartAttack();
		}
	}

	private void Flip() {
		// Switch the way the player is labelled as facing.
		dirX = dirX * -1;
		transform.Rotate(0f, 180f, 0f);
	}

	private void StartAttack() {
		animator.SetBool("Attack", true);
		startAttack = true;
	}

	private void Attack() {
		animator.SetBool("Attack", false);
		if (inRangeOfPlayer) playerHealth.TakeDamage(dealDamage);
		startAttack = false;
	}
}
