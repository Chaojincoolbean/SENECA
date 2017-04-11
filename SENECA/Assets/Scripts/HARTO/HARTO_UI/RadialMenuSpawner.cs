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

	public GameObject uiMouse;
	public RadialMenu menuPrefab;

	public RadialMenu newMenu;

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

		clip = Resources.Load("Audio/SFX/HARO_SFX/LV-HTIS Beeps Simple 03") as AudioClip;

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
			//GameEventsManager.Instance.Fire(new BeginTutorialEvent());
			Vector3 tabPosition = GameObject.Find("Mouse_Location").transform.localPosition;
			GameObject mouse = Instantiate(uiMouse, tabPosition, Quaternion.identity);
			mouse.transform.SetParent(GameObject.Find("HARTOCanvas").transform, false);
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
