using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour 
{
	public static RadialMenuSpawner instance;

	public RadialMenu menuPrefab;

	private RadialMenu newMenu;


	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void SpawnMenu(HARTO_UI_Interface obj, Player player, bool dialogueModeActive, bool topicSelected)
	{
		newMenu = Instantiate(menuPrefab) as RadialMenu;
		newMenu.transform.SetParent(transform, false);
		newMenu.Init(player);
		newMenu.SpawnIcons(obj, topicSelected);
		// Courtine to fade in image here!
	}

	public void DestroyMenu()
	{
		//	Courtine to fade out image here
		Destroy(newMenu.gameObject);
	}
}
