using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

	// Do not go to nuext scene until EndConvo topic has been played
	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Player") 
		{
			if(SceneManager.GetActiveScene().name == "Utan_Meadow")
			{
				//	Pass in the scene you are going to
				GameEventsManager.Instance.Fire(new SceneChangeEvent("Utan_ForkPath"));
				SceneManager.LoadScene("Utan_ForkPath");
			}
			else
			{
				GameEventsManager.Instance.Fire(new SceneChangeEvent("Seneca_ForestForkPath"));
				SceneManager.LoadScene ("Seneca_ForestForkPath");
			}

		}
	}
}
