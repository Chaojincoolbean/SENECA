using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueCutscene : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PrologueTransitionTo1(){
		anim.SetTrigger ("0to1");
	}

	public void PrologueTransitionTo2(){
		anim.SetTrigger ("1to2");
	}

	public void PrologueTransitionToMid(){
		anim.SetTrigger ("1tomid");
	}

	public void PrologueTransitionTo3(){
		anim.SetTrigger ("2to3");
	}
}
