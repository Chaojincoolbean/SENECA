using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleWallMover : MonoBehaviour 
{

	public Animator anim;
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();	
	}
}
