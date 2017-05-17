using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueCutscene : MonoBehaviour {

	Animator anim;
	public AudioClip vo1,vo2;
	private AudioSource audiosource;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audiosource = GetComponent<AudioSource> ();
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

	public void PrologueTransitionToIntro(){
		anim.SetTrigger ("3to4");
	}

	public void LoadCampSite(){
		GameObject.Find ("PrologueSceneScript").GetComponent<Prologue> ().LoadNext ();	
	}
	public void PlayVO1(){
		audiosource.clip = vo1;
		audiosource.Play ();
	}

	public void PlayVO2(){
		audiosource.clip = vo2;
		audiosource.Play ();
	}
}
