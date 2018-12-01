﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public float runSpeed = 60f;
	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		if (Input.GetButtonDown("Jump")) {
			jump = true;
		}
		
	}

	void FixedUpdate () {

		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
		
	}
}
