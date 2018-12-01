using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	[SerializeField] public int startingHealth = 100;
	public int currentHealth;
	public bool isDead;

	private void Awake() {
		isDead = false;
		currentHealth = startingHealth;
	}

	public void TakeDamage(int amount) {
        currentHealth -= amount;
		if (currentHealth <= 0 && !isDead) {
            Death();
        }
	}

	public void healHealth(int amount) {
        currentHealth += amount;
		if (currentHealth > startingHealth) {
			currentHealth = startingHealth;
		}
	}

	void Death() {
		isDead = true;
	}
}
