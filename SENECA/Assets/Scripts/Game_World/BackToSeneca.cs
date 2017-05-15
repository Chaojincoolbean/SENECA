using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

public class BackToSeneca : MonoBehaviour 
{
    public bool fireOnce;
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
        fireOnce = false;
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
		if (collider.tag == "Player" && !fireOnce)
		{
            fireOnce = true;
            Debug.Log("Hit");
            //	Play Ruth Audio and flash the screen.
            Services.Events.Fire(new EndGameEvent());
			
		}
	}

	public void RollCredits()
	{
		Services.Events.Fire(new SceneChangeEvent("_CreditScene"));

        Services.Scenes.Swap<CreditSceneScript>(TransitionData.Instance);
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (GameManager.instance.endGame)
		{
            // put this in an event. probably EndGameEvent
			anim.SetBool("Flash", true);
		}

		
	}
}
