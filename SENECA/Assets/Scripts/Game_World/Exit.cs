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
			if(GameManager.instance.sceneName == "UtanMeadow") // Find a new way to determine the scene
			{
				//	Pass in the scene you are going to
				Services.Events.Fire(new SceneChangeEvent("Utan_ForkPath"));
				TransitionData.Instance.UTAN_MEADOW.position = coll.transform.position;
				TransitionData.Instance.UTAN_MEADOW.scale = coll.transform.localScale;
				Services.Scenes.Swap<UtanRoadSceneScript>(TransitionData.Instance);
			}
			else
			{
				Services.Events.Fire(new SceneChangeEvent("Seneca_ForestForkPath"));
				TransitionData.Instance.SENECA_CAMPSITE.position = coll.transform.position;
				TransitionData.Instance.SENECA_CAMPSITE.scale = coll.transform.localScale;
				Services.Scenes.Swap<SenecaForestForkSceneScript>(TransitionData.Instance);
			}

		}
	}
}
