using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witchlightmanager : MonoBehaviour {

	public float x,y;

	// Use this for initialization
	void Start () {
		x = this.gameObject.transform.position.x;
		y = this.gameObject.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {


		x = x + 0.05f;

			

		if (x >= 0f) {

			x = x + 0.02f;

			y = y - 0.03f;
		}

		if (x >= 2f) {
			x = x + 0.01f;
			y = y - 0.03f;
		}

		this.gameObject.transform.position = new Vector3 (x, y, 0);


	}
}
