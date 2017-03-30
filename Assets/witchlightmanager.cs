using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witchlightmanager : MonoBehaviour {

	public float x;
	public GameObject player;

	// Use this for initialization
	void Start () {
		x = this.gameObject.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {

		if (x - player.gameObject.transform.position.x < 2f) {

			x = x + 0.05f;

			this.gameObject.transform.position = new Vector3 (x, this.gameObject.transform.position.y, 0);
		}

		if (player.gameObject.transform.position.x - x > 0f ){

			x = player.gameObject.transform.position.x + 2f;
		}
	}
}
