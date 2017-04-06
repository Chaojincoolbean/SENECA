using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

public class RadialMenuSpawner : MonoBehaviour 
{

	public AudioClip clip;
	public AudioSource audioSource;
	public static RadialMenuSpawner instance;

	public RadialMenu menuPrefab;

	private RadialMenu newMenu;

	private static bool firstPass = true;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		audioSource = GetComponent<AudioSource>();
	}

	public void SpawnMenu(HARTO_UI_Interface obj, Player player, bool dialogueModeActive, bool topicSelected)
	{

		clip = Resources.Load("Audio/SFX/FUTURE_BEEPS_LITE/R2D2/R2D2_Low_0014") as AudioClip;

		if(!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(clip);
		}
		newMenu = Instantiate(menuPrefab) as RadialMenu;
		newMenu.transform.SetParent(transform, false);
		newMenu.Init(player);
		newMenu.SpawnIcons(obj, topicSelected);
		
		if(firstPass)
		{
			GameEventsManager.Instance.Fire(new BeginTutorialEvent());
			firstPass = false;
		}
		// Courtine to fade in image here!
	}

	public void DestroyMenu()
	{
		//	Courtine to fade out image here
		if (newMenu != null)
		{
			Destroy(newMenu.gameObject);
		}
	}
}
