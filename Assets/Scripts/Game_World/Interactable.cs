using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour 
{
	public Collider myCollider;
	
	// Use this for initialization
	void Start () {
		
	}

	void OnMouseDown()
	{
		Debug.Log("Clicked!");
	}
	
}
