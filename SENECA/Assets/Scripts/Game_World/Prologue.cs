using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem;
using ChrsUtils.ChrsEventSystem.EventsManager;
using UnityEngine.SceneManagement;
using SenecaEvents;

public class Prologue : MonoBehaviour 
{
	public AudioClip clip;
	private AudioSource audioSource;
	// Use this for initialization
	void Start () 
	{
		clip = Resources.Load("Audio/VO/Beorn/BEORN_VO_GAMEINTRO") as AudioClip;
		audioSource = GetComponent<AudioSource>();

		audioSource.PlayOneShot(clip);
		GameManager.instance.inConversation = true;	
	}
	
	IEnumerator LoadNextScene()
	{
		yield return new WaitForSeconds(4.0f);
		Services.Events.Fire(new SceneChangeEvent("Seneca_Campsite"));
		TransitionData.Instance.TITLE.visitedScene = true;
		TransitionData.Instance.TITLE.position = Vector3.zero;
		TransitionData.Instance.TITLE.scale = Vector3.zero;
		Services.Scenes.Swap<SenecaCampsiteSceneScript>(TransitionData.Instance);
	}

	// Update is called once per frame
	void Update () 
	{
		/*
		//change this to animation event
		if(!audioSource.isPlaying)
		{
			GameManager.instance.inConversation = false;
			StartCoroutine(LoadNextScene());	
		}
		*/

		if (Input.GetKey (KeyCode.Space)) {
			GameManager.instance.inConversation = false;
			StartCoroutine(LoadNextScene());
		}
	}

	public void LoadNext(){
		GameManager.instance.inConversation = false;
		StartCoroutine(LoadNextScene());
	}


}
