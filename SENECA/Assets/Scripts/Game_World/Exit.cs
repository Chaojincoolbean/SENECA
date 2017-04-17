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

	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Player") 
		{
			if(SceneManager.GetActiveScene().name == "Utan2")
			{
				//	Pass in the scene you are going to
				GameEventsManager.Instance.Fire(new SceneChangeEvent("Utan3"));
				SceneManager.LoadScene("Utan3");
			}
			else
			{
				GameEventsManager.Instance.Fire(new SceneChangeEvent("Forest2"));
				SceneManager.LoadScene (1);
			}

		}
	}
}
