using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject Player;
	GameObject Mom;
	float x;
	float y;



	// Use this for initialization
	void Start () {
		Mom = Instantiate(Resources.Load("Prefabs/Characters/Mom", typeof(GameObject))) as GameObject;

		Mom.gameObject.transform.position = new Vector3 (-10f, -3.5f, 0);

		x = Mom.gameObject.transform.position.x;
		y = Mom.gameObject.transform.position.y;
		Debug.Log ("x:"+x+"y"+y);
	}
	
	// Update is called once per frame
	void Update () {

		x = x + 0.01f;

		Mom.gameObject.transform.position = new Vector3 (x, -3.5f, 0);

		//Debug.Log (x);

	}



}
