using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour {

	public int activeBiomIndex = 0;
	public int currentBiomIndex = 0;

	public GameObject follow;

	private float[] y;

	private RectTransform[] rectTransforms;

	public float fadeTime = 1f;
	private float timeToFade;
	private bool startTranstion = false;
	void Start() {
		rectTransforms = GetComponentsInChildren<RectTransform>(true);
		y = new float[rectTransforms.Length];

		for (int i = 1; i < rectTransforms.Length; i++) {
			y[i] = follow.transform.position.y - rectTransforms[i].position.y;
		}		
	}

	void Update () {
		for (int i = 1; i < rectTransforms.Length; i++) {
			Vector3 _new = rectTransforms[i].position;
			_new.y = (follow.transform.position.y - y[i]) * 0.5f;
			rectTransforms[i].position = _new;
		}

	}

	private void FixedUpdate() {
		if (startTranstion) {
			timeToFade -= Time.deltaTime;
			Image[] images = GetComponentsInChildren<Image>();
			Image biom = GetComponentsInChildren<Image>()[currentBiomIndex];
			Debug.Log(biom.color);
			Color temp_biom_color = biom.color;
			temp_biom_color.a = timeToFade;
			biom.color = temp_biom_color;
			
			Image biom2 = GetComponentsInChildren<Image>()[activeBiomIndex];
			Color temp_biom_color2 = biom2.color;
			temp_biom_color2.a = 1f - timeToFade;
			biom2.color = temp_biom_color2;

			if (timeToFade < 0f) {
				startTranstion = false;
			}
		}
	}

	public void changeBiom(int biomIndex, int prevBiomIndex) {
		currentBiomIndex = activeBiomIndex;
		if (biomIndex == activeBiomIndex) {
			activeBiomIndex = prevBiomIndex;
		} else {
			activeBiomIndex = biomIndex;
		}

		timeToFade = fadeTime;
		startTranstion = true;
	}
}
