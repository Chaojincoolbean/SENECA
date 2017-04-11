using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class witchlightmanager1 : MonoBehaviour {

	public float x, y;
	public GameObject player;

	// Use this for initialization
	void Start () {
		x = this.gameObject.transform.position.x;
		y = this.gameObject.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		if (x < 0) {

			if (x - player.gameObject.transform.position.x < 2f) {

				x = x + 0.05f;

				this.gameObject.transform.position = new Vector3 (x, this.gameObject.transform.position.y, 0);
			}

			if (player.gameObject.transform.position.x - x > 0f) {

				x = player.gameObject.transform.position.x + 2f;
			}
		}
		else{
			x = 0f;
			y = 0.8f;

			this.gameObject.transform.position = new Vector3 (x, y, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			SceneManager.LoadScene (3);

		}
	}
}
