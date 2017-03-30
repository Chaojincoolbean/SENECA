using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public const string RECORDING_MANAGER = "RecordingManager";

	public RecordingManager recordingManager;
	public GameObject Player;
	public GameObject Mom;
	float x;
	float y;


	// Use this for initialization
	void Start () {

		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		recordingManager = GameObject.Find(RECORDING_MANAGER).GetComponent<RecordingManager>();
		
		Mom = Instantiate(Resources.Load("Prefabs/Characters/Mom", typeof(GameObject))) as GameObject;

		Mom.gameObject.transform.position = new Vector3 (-10f, -3.5f, 0);

		x = Mom.gameObject.transform.position.x;
		y = Mom.gameObject.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		

//		if (x < Player.gameObject.transform.position.x - 1f) {
//
//			x = x + 0.05f;
//
//			Mom.gameObject.transform.position = new Vector3 (x, -3.5f, 0);
//
//			Debug.Log (Mom.gameObject.transform.position);
//		}



	}



}
