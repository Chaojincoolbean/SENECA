﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChrsUtils.ChrsEventSystem;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;
using SenecaEvents;

public class BGM_Singleton : MonoBehaviour 
{
	public static BGM_Singleton instance;
	public string sceneName;

	public float volume;
	public AudioSource audioSource;
	public AudioClip clip;
	private SceneChangeEvent.Handler onSceneChange;
	// Use this for initialization
	void Start () 
	{
		if(instance == null)
		{
			instance = this;
			sceneName = GameManager.instance.sceneName;
			audioSource = GetComponent<AudioSource>();
			volume = 0.5f;


			if(sceneName.Contains("Utan"))
			{
				clip = Resources.Load("Audio/Music/Utan_Theme") as AudioClip;
			}
			else if(sceneName.Contains("Seneca"))
			{
				clip = Resources.Load("Audio/Music/Seneca_Theme") as AudioClip;
			}
			else if(sceneName.Contains("Title"))
			{
				clip = Resources.Load("Audio/Music/Title_Theme") as AudioClip;
			}
			else if(sceneName.Contains("Credits"))
			{
				clip = Resources.Load("Audio/Music/Credits_Theme") as AudioClip;
			}

			onSceneChange = new SceneChangeEvent.Handler(OnSceneChange);
			Services.Events.Register<SceneChangeEvent>(onSceneChange);
			audioSource.PlayOneShot(clip, volume);
			audioSource.volume = volume;
		}
	
		onSceneChange = new SceneChangeEvent.Handler(OnSceneChange);
		Services.Events.Register<SceneChangeEvent>(onSceneChange);
		audioSource.PlayOneShot(clip, volume);
		audioSource.volume = volume;
	}

    private void OnDestroy()
    {
        Services.Events.Unregister<SceneChangeEvent>(onSceneChange);
    }



    void OnSceneChange(GameEvent e)
	{
		string newScene = ((SceneChangeEvent)e).sceneName;
		
		if (!sceneName.Contains ("Seneca") && newScene.Contains ("Seneca")) {

			audioSource.loop = true;
			clip = Resources.Load ("Audio/Music/Seneca_Theme") as AudioClip;
			audioSource.Stop ();
			audioSource.PlayOneShot (clip, volume);
		} else if (!sceneName.Contains ("Utan") && newScene.Contains ("Utan")) {
			audioSource.loop = true;
			clip = Resources.Load ("Audio/Music/Utan_theme") as AudioClip;
			audioSource.Stop ();
			audioSource.PlayOneShot (clip, volume);
			
		} else if (sceneName.Contains ("Title")) {
			clip = Resources.Load ("Audio/Music/Title_Theme") as AudioClip;
			audioSource.Stop ();
			audioSource.loop = false;
			audioSource.PlayOneShot (clip, volume);
		} else if (sceneName.Contains ("Credits")) {
			audioSource.loop = true;
			clip = Resources.Load ("Audio/Music/Title_Theme") as AudioClip;
			audioSource.Stop ();
			audioSource.PlayOneShot (clip, volume);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		sceneName = GameManager.instance.sceneName;

		if (GameManager.instance.inConversation)
		{
			volume = 0.2f;
			
		}
		else if(!GameManager.instance.endGame)
		{
			volume = 0.3f;
		}
		audioSource.volume = volume;

		if (!audioSource.isPlaying && audioSource.loop) 
		{
			audioSource.PlayOneShot (clip, volume);
		}
	}
}
