using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

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
			if(GameManager.instance.sceneName == "Utan_Meadow") // Find a new way to determine the scene
			{
				//	Pass in the scene you are going to
				GameEventsManager.Instance.Fire(new SceneChangeEvent("Utan_ForkPath"));
				Services.Scenes.Swap<PrologueSceneScript>(new TransitionData("Utan_Meadow", coll.transform.position, coll.transform.localScale));
			}
			else
			{
				GameEventsManager.Instance.Fire(new SceneChangeEvent("Seneca_ForestForkPath"));
				Services.Scenes.Swap<PrologueSceneScript>(new TransitionData("Seneca_Campsite", coll.transform.position, coll.transform.localScale));
			}

		}
	}
}
