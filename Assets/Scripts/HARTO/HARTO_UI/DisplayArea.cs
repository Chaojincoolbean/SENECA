using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayArea : MonoBehaviour 
{
	public Image displayIcon;
	public Sprite defaultSprite;

	// Use this for initialization
	void Awake () 
	{
		displayIcon = GetComponent<Image>();	
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
