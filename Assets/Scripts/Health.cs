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

	void Death() {
		isDead = true;
	}

	void UpdateHealthbar() {
		healthbar.value = currentHealth / startingHealth;
	}
}
