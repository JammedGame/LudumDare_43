using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public Slider healthbar;
    public float startingHealth = 100;
	public float currentHealth;
	public bool isDead;


	private void Awake() {
		isDead = false;
		currentHealth = startingHealth;
		UpdateHealthbar();
	}

	public void TakeDamage(float amount) {
		// Debug.Log("Took Damage");
		// Debug.Log(amount);
		// Debug.Log(gameObject.tag);
        currentHealth -= amount;
		UpdateHealthbar();
        if (currentHealth <= 0 && !isDead) {
            Death();
        }
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
		// TODO: Temporary until respwn time is implemented
		if (gameObject.tag == "Enemy") Destroy(gameObject);
	}

	void UpdateHealthbar() {
		healthbar.value = currentHealth / startingHealth;
	}
}
