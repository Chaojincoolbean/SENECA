using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

public class ExitForest2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Player") 
		{
			GameEventsManager.Instance.Fire(new SceneChangeEvent("Seneca_Meadow"));
			SceneManager.LoadScene ("Seneca_Meadow");

		}
	}
}
