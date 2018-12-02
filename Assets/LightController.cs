using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

	public GameObject follow;
	private Light lt;

	// private float defaultRange;
	// private float defaultSpotAngle;
	// private float defaultIntensity;

	// public float flickerTime = 2f;
	// private float timeToFlick = 0f;

	// Use this for initialization
	void Start () {
		lt = GetComponent<Light>();
		// defaultIntensity = lt.intensity;
		// defaultSpotAngle = lt.spotAngle;
		// defaultRange = lt.range;
		// float rangeRandom = Random.Range(-0.2f, 0.2f);
		// float spotAngleRandom = Random.Range(-1, 1);
		// float intensityRandom = Random.Range(-0.2f, 0.2f);
	}

	void Update() {

		// lt.intensity = defaultIntensity + intensityRandom;
		// lt.spotAngle = defaultSpotAngle + spotAngleRandom;
		// lt.range = defaultRange + rangeRandom;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 position = follow.transform.position;
        position.z = transform.position.z;
        transform.position = position;
	}
}
