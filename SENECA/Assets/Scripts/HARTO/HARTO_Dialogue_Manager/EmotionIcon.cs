using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionIcon : Icon 
{

	public Emotions emotion;
	// Use this for initialization
	void Start () 
	{
		base.Start();
		try 
		{
			if (System.Enum.IsDefined(typeof(Emotions), emotion))
			{
				emotion = (Emotions)System.Enum.Parse(typeof(Emotions), transform.name, true);
			}
		}
		catch (Exception e)
		{
			Debug.Log ("Emotion " + transform.name + " not found. Has " + transform.name + " been added to the Enum in HARTO.cs? Error: " + e.Message);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update();

		if (selected && astridHARTO.isHARTOActive)
		{
			myIcon.color = activeColor;
		}
	}
}
