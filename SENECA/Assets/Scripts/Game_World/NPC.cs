using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	private Vector3 startPos;
	private Vector3 endPos;
	public float distance;
	public float lerptime;
	private float currentLerptime;
	private bool n;


	// Use this for initialization
	void Start () {

		startPos = this.gameObject.transform.position;
		endPos = this.gameObject.transform.position + Vector3.right * distance;

	}

	// Update is called once per frame
	void Update () {

		wheretostart ();

		if (n == true) {

			currentLerptime += Time.deltaTime/0.5f;
			if (currentLerptime >= lerptime) {
				currentLerptime = lerptime;
			}

			float Perc = currentLerptime / lerptime;
			this.transform.position = Vector3.Lerp (startPos, endPos, Perc);
			//Debug.Log ("currenttime:" + currentLerptime);
		}

		if (n == false) {

			currentLerptime += Time.deltaTime;
			if (currentLerptime >= lerptime) {
				currentLerptime = lerptime;
			}

			float Perc = currentLerptime / lerptime;
			this.transform.position = Vector3.Lerp (endPos, startPos, Perc);
			
		}

			
		
	}

	void wheretostart(){

		if (this.gameObject.transform.position == startPos) {
			n = true;
            if (Random.Range(0, 100) < 80.0f)
            {
                RandomizeNPCMovement();
            }
			currentLerptime = 0;
		}

        if (this.gameObject.transform.position == endPos)
        {
            n = false;
            if (Random.Range(0, 100) < 10.0f)
            {
                RandomizeNPCMovement();
            }
            currentLerptime = 0;
        }
		
	}

    void RandomizeNPCMovement()
    {
        lerptime = Random.Range(0.75f, 3.0f);
        distance = Random.Range(0.75f, 8.0f);
    }
}
