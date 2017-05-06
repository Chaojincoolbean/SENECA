using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

public class witchlightmanager1 : MonoBehaviour {

	public Animator anim;
	public float x, y;
	public GameObject player;
	public Camera mainCamera;
	public float n;
	bool isCameraRotating;

	public GameObject Flare1;
	public GameObject Flare2;
	public GameObject Flare3;
	public GameObject Flare4;

	// Use this for initialization
	void Start () {
		x = this.gameObject.transform.position.x;
		y = this.gameObject.transform.position.y;
		isCameraRotating = false;
		anim = this.gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isCameraRotating == true) {

			mainCamera.transform.Rotate (Vector3.forward * n * Time.deltaTime);
			//Debug.Log ("CameraSize: " + Mathf.Lerp (5, 1,Time.deltaTime*15));


			if(mainCamera.transform.rotation.eulerAngles.z >= 180)
			{
				GameEventsManager.Instance.Fire(new SceneChangeEvent("Utan Meadow"));
				Services.Scenes.Swap<PrologueSceneScript>(new TransitionData("Seneca_Meadow", player.transform.position, player.transform.localScale));
			}

		}

//		if (x < 0) {
//
//			if (x - player.gameObject.transform.position.x < 2f) {
//
//				x = x + 0.05f;
//
//				this.gameObject.transform.position = new Vector3 (x, this.gameObject.transform.position.y, 0);
//			}
//
//			if (player.gameObject.transform.position.x - x > 0f) {
//
//				x = player.gameObject.transform.position.x + 2f;
//			}
//		}
//		else{
//			x = 0f;
//			y = 0.8f;
//
//			this.gameObject.transform.position = new Vector3 (x, y, 0);
//		}
	}

	void OnTriggerEnter2D(Collider2D col){
		
		if (col.gameObject.tag == "Player") {

			Flare1.gameObject.GetComponent<Animator> ().SetBool("Flare", true);
			Flare2.gameObject.GetComponent<Animator> ().SetBool("Flare", true);
			Flare3.gameObject.GetComponent<Animator> ().SetBool("Flare", true);
			Flare4.gameObject.GetComponent<Animator> ().SetBool("Flare", true);

			StartCoroutine (SizeLerp());
		}

	}

	IEnumerator SizeLerp(){

		yield return new WaitForSeconds(1);

		Flare1.transform.position = new Vector3 (-3.16f, 1.67f, 0);
		Flare2.transform.position = new Vector3 (3.16f, 1.67f, 0);
		Flare3.transform.position = new Vector3 (3.16f, -1.67f, 0);
		Flare4.transform.position = new Vector3 (-3.16f, -1.67f, 0);

		isCameraRotating = true;

		float t = 0;

		while (t < 1) {
			mainCamera.orthographicSize = Mathf.Lerp (5, 2, t/1.2f);
			t = t + Time.deltaTime;
			Debug.Log (mainCamera.orthographicSize);
		}

		yield return null;
	}


		
}
