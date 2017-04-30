using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

public class BackToSeneca : MonoBehaviour 
{

	public Animator anim;
	public AudioClip clip;
	public AudioSource audioSource;
	public BoxCollider2D characterCollider;
	public BoxCollider2D triggerArea;
	private bool beganTalking;
	private BoxCollider2D[] colliders;
	// Use this for initialization
	void Start () 
	{
		beganTalking = false;
		audioSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		colliders = GetComponents<BoxCollider2D>();

		for(int i = 0; i < colliders.Length; i++)
		{
			if(colliders[i].isTrigger)
			{
				triggerArea = colliders[i];
			}
			else
			{
				characterCollider = colliders[i];
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			//	Play Ruth Audio and flash the screen.
			clip = Resources.Load("Audio/VO/Ruth/Ruth-AUDITION-AbigailWahl") as AudioClip;

			if(!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(clip);
				beganTalking = true;
			}
			
		}
	}

	public void RollCredits()
	{
		GameEventsManager.Instance.Fire(new SceneChangeEvent("_CreditScene"));
		SceneManager.LoadScene("Utan_ForkPath");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (beganTalking && !audioSource.isPlaying)
		{
			anim.SetBool("Flash", true);
		}

		
	}
}
