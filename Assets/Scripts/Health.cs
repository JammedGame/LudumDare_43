using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {


   [SerializeField] private Healthbar hb;
   [SerializeField] public float startingHealth = 1;
	public float currentHealth;
	public bool isDead;


	private void Awake() {
		isDead = false;
		currentHealth = startingHealth;
        hb.setHealth(currentHealth);
	}

	public void TakeDamage(float amount) {
        currentHealth -= amount;
        hb.setHealth(currentHealth);
        if (currentHealth <= 0 && !isDead) {
            Death();
        }
	}

	public void healHealth(float amount) {
        currentHealth += amount;
        hb.setHealth(currentHealth);
        if (currentHealth > startingHealth) {
			currentHealth = startingHealth;
		}
	}

	void Death() {
		isDead = true;
	}

   
}
