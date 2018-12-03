using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public Slider healthbar;
    public float startingHealth = 100;
	public float currentHealth;
	public bool isDead;

	public float reviveTime = 30f;
	private float timeToRevive;
	private float timeToFinishDying;

	private bool animationStart = false;
	private bool animationComplete = false;

	private GameManager gameManager;

	private void Awake() {
		isDead = false;
		currentHealth = startingHealth;
		UpdateHealthbar();
		gameManager = FindObjectOfType<GameManager>();
	}

	private void Update() {
		if (isDead && gameObject.tag == "Enemy") {
			timeToRevive -= Time.deltaTime;
			if (timeToRevive <= 0) {
				animationStart = true;
				gameObject.GetComponent<Animator>().SetFloat("Speed", 0f);
				gameObject.GetComponent<Animator>().SetBool("Attack", false);
				gameObject.GetComponent<Animator>().SetBool("Dead", false);
			}
		}
		if (isDead && gameObject.tag == "Player") {
			timeToFinishDying -= Time.deltaTime;
			if (timeToFinishDying < 1f) {
				gameObject.GetComponent<Animator>().SetBool("Dead", false); // Stop Player Death Animation
			}
			if (timeToFinishDying <= 0) {
				gameManager.GameOver();
			}
		}
		
		if (animationStart && animationComplete) {
			animationStart = false;
			animationComplete = false;
			Revive();
		}
		if (
			animationStart &&
			gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1
		) {
			animationComplete = true;
		}
	}

	public void TakeDamage(float amount) {
        currentHealth -= amount;
		UpdateHealthbar();
        if (currentHealth <= 0 && !isDead) Death();
	}

	public void healHealth(float amount) {
        currentHealth += amount;
		UpdateHealthbar();
        if (currentHealth > startingHealth) {
			currentHealth = startingHealth;
		}
	}

	public void Death() {
		isDead = true;
		if (gameObject.tag == "Enemy") {
			timeToRevive = reviveTime;
			gameObject.GetComponent<Animator>().SetBool("Dead", true); // Trigger Animation
			gameObject.GetComponent<Rigidbody2D>().simulated = false; // Disable Rigidbody also disabling all colliders
			gameObject.GetComponentInChildren<Canvas>().enabled = false; // Disable Healtbar
		} else if (gameObject.tag == "Player") {
			gameObject.GetComponent<Animator>().SetBool("Dead", true); // Trigger Animation
			timeToFinishDying = 1.6f;
		}
	}

	private void Revive() {
		isDead = false;
		currentHealth = startingHealth;
		UpdateHealthbar();
		gameObject.GetComponent<Rigidbody2D>().simulated = true;
		gameObject.GetComponentInChildren<Canvas>().enabled = true;
	}

	void UpdateHealthbar() {
		healthbar.value = currentHealth / startingHealth;
	}
}
