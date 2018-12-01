using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private bool m_FacingRight = true;
	private Vector3 m_Velocity = Vector3.zero;

	private Rigidbody2D rb;
	[SerializeField] public float moveSpeed = 5f;

	private float dirX = 1;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate() {
		
        rb.velocity = new Vector2(1 * moveSpeed * dirX, 0);
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
		dirX = dirX * -1;
		transform.Rotate(0f, 180f, 0f);
	}

	private void OnTriggerExit2D(Collider2D other) {
		Debug.Log("test2");
		Flip();
	}

}
