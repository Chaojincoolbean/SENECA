using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : MonoBehaviour {

	float x;
	bool onposition;

	// Use this for initialization
	void Start () {

		x = -10f;
		onposition = false;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (onposition == false) {

			x = x + 0.05f;

			this.gameObject.transform.position = new Vector3 (x, -3.5f, 0);

		
		}

//		if (onposition == true) {
//		
//			x = x - 0.05f;
//
//			this.gameObject.transform.position = new Vector3 (x, -3.5f, 0);
//
//		}

		if (x < -10f) {
			Destroy (this);
		
		}
	}

	void OnTriggerEnter2D(Collider2D col){
			

		this.gameObject.GetComponent<AudioSource> ().Play ();

		onposition = true;


		Debug.Log (x);
		//onposition = true;


	}



}
