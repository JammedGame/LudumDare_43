using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biomchanger : MonoBehaviour {

	public int prevBiom = 0;
	public int biom = 1;
	public GameObject background;

	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player" && collider.isTrigger == false) {
			background.GetComponent<BackgroundScroll>().changeBiom(biom, prevBiom);
		}
	}
}
