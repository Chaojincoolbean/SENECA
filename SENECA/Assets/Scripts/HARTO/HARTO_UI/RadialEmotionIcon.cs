using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RadialEmotionIcon : RadialIcon {

	public Emotions emotion;
	// Use this for initialization
	void Start () 
	{
		GetComponent<Image> ().color = transform.parent.GetChild (0).gameObject.GetComponent<Image> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Image> ().color = transform.parent.GetChild (0).gameObject.GetComponent<Image> ().color;
	}
}
