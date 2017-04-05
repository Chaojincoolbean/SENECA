using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dotsCircuit : MonoBehaviour {

	// Use this for initialization
	private GameObject clone;
	private LineRenderer line;
	private int i;
	public  GameObject tf;


	// Use this for initialization
	void Start () {
		tf = this.gameObject;
		i = 0;
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)) {
			clone = (GameObject)Instantiate(tf,tf.transform.position, transform.rotation);

			line = clone.GetComponent<LineRenderer>();
			line.SetColors(Color.white,Color.white);
			//line.SetWidth(0.2f,0.1f);
			i = 0;

		}

		if (Input.GetMouseButton(0)){
			i ++;
			line.SetVertexCount(i);
			line.SetPosition(i - 1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15)));
		}

	}
}
