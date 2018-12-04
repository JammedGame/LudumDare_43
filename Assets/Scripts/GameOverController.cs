using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {

	public float timeToFinishGame = 3f;
	public bool startAnimation = false;

	private GameObject player;

	private void Update() {
		if (startAnimation) {
			timeToFinishGame -= Time.deltaTime;
			if (timeToFinishGame <= 2f) {
				player.GetComponent<Animator>().SetBool("Win", true);
			}
			if (timeToFinishGame <= 0f) {
				startAnimation = false;
				FindObjectOfType<GameManager>().GameWin();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && other.isTrigger == false) {
			GetComponent<Animator>().enabled = true;
			player = other.gameObject;
			startAnimation = true;
			player.GetComponent<Animator>().SetFloat("Speed", 0f);
			player.GetComponent<PlayerMovement>().enabled = false;
			player.GetComponent<Health>().enabled = false;
			player.GetComponent<Rigidbody2D>().simulated = false;
		}
	}

	


}
