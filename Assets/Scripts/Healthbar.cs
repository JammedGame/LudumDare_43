using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour {

    private Transform bar;
	// Use this for initialization
	void Start () {
        bar = transform.Find("Bar");
        bar.localScale = new Vector3(.4f, 1f);
    }

    public void setHealth(float hp)
    {
        bar.localScale = new Vector3(hp, 1f);
    }



}
