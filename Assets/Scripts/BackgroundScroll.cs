using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

	public GameObject follow;

	private float y;

	private RectTransform rectTransform;
	void Start() {
		rectTransform = GetComponent<RectTransform>();
		y = follow.transform.position.y - rectTransform.position.y;
	}

	void Update () {
		Vector3 _new = rectTransform.position;
		_new.y = (follow.transform.position.y - y) * 0.5f;
		rectTransform.position = _new;
	}
}
