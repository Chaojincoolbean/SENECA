﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	float x;
	float y;

	// Use this for initialization
	void Start () {
		x = this.gameObject.transform.position.x;
		y = this.gameObject.transform.position.y;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.W)){
			y = y + 0.1f;
		}
		if(Input.GetKey(KeyCode.S)){
			y = y - 0.1f;
		
		}
		if(Input.GetKey(KeyCode.A)){
			x = x - 0.1f;
		}
		if(Input.GetKey(KeyCode.D)){
			x = x + 0.1f;

		}

		this.gameObject.transform.position = new Vector3 (x, y, 0);
	}
}