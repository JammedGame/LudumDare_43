using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			other.gameObject.GetComponent<Health>().Death();
		} else {
			Destroy(gameObject);
		}
	}
}
