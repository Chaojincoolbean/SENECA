using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour {

	public float n;
	bool isChangeSize;
	bool isCameraRotating;
	public Camera FlipCamera;
	public Camera MainCamera;
	public GameObject Flare1;
	public GameObject Flare2;
	public GameObject Flare3;
	public GameObject Flare4;


	// Use this for initialization
	void Start () {

		isChangeSize = true;
		isCameraRotating = true;

		Flare1.gameObject.GetComponent<Animator> ().SetBool("FlareReverse", true);
		Flare2.gameObject.GetComponent<Animator> ().SetBool("FlareReverse", true);
		Flare3.gameObject.GetComponent<Animator> ().SetBool("FlareReverse", true);
		Flare4.gameObject.GetComponent<Animator> ().SetBool("FlareReverse", true);

		MainCamera = Camera.main;

		MainCamera.enabled = false;
		FlipCamera.enabled = true;

	}
	
	// Update is called once per frame
	void Update () {

		if (isChangeSize == true) {

			StartCoroutine (SizeLerp());

			isChangeSize = false;
		}

		if (isCameraRotating == true) {


			transform.Rotate (Vector3.forward * n * Time.deltaTime);
			//Debug.Log (transform.rotation.eulerAngles.z);
	
		}

		if (transform.rotation.eulerAngles.z <= 5) {
			
			isCameraRotating = false;

			transform.eulerAngles = new Vector3(0,0,0);
		
		}
			


		
	}

	IEnumerator SizeLerp(){

		yield return new WaitForSeconds(1);

		float t = 0;

		while (t < 1) {
			
			FlipCamera.orthographicSize = Mathf.Lerp (2, 3.5f, t);

			t = t + Time.deltaTime;

			Debug.Log (FlipCamera.orthographicSize);

			if (FlipCamera.orthographicSize >= 3f) {

				FlipCamera.orthographicSize = 5f;

				Debug.Log ("camerachange");
				
				Flare1.transform.position = new Vector3 (-8.64f, 4.66f, 0);
				Flare2.transform.position = new Vector3 (8.64f, 4.66f, 0);
				Flare3.transform.position = new Vector3 (8.64f, -4.66f, 0);
				Flare4.transform.position = new Vector3 (-8.64f, -4.66f, 0);

//			
			}
		}


		FlipCamera.enabled = false;
		MainCamera.enabled = true;
		MainCamera.orthographicSize = 5f;
		MainCamera.transform.eulerAngles = new Vector3(0,0,0);

		yield return null;
	}
}
