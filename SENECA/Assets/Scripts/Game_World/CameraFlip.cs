using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour {

	public float n;
	bool isCameraRotating;

	// Use this for initialization
	void Start () {

		isCameraRotating = true;
		
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isCameraRotating == true) {


			transform.Rotate (Vector3.forward * n * Time.deltaTime);
			Debug.Log (transform.rotation.eulerAngles.z);

		}

		if (transform.rotation.eulerAngles.z <= 2) {
			
			isCameraRotating = false;

			transform.eulerAngles = new Vector3(0,0,0);
		
		}
		
	}
}
