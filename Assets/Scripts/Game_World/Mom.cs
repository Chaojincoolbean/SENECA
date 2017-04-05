using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;

public class Mom : MonoBehaviour 
{

	public bool moveMom;
	float x;
	bool onposition;
	private MoveMomEvent.Handler onMoveMomEvent;
	private ToggleHARTOEvent.Handler onToggleHARTO;

	// Use this for initialization
	void Start () 
	{
		gameObject.name = "Priya";
		moveMom = false;
		x = -10f;
		onposition = false;

		onMoveMomEvent = new MoveMomEvent.Handler(OnMoveMom);
		onToggleHARTO = new ToggleHARTOEvent.Handler(OnToggleHARTO);

		GameEventsManager.Instance.Register<MoveMomEvent>(onMoveMomEvent);
		GameEventsManager.Instance.Register<ToggleHARTOEvent>(onToggleHARTO);
	}

	void OnMoveMom(GameEvent e)
	{
		moveMom = true;
	}

	void OnToggleHARTO(GameEvent e)
	{
		Debug.Log("Ok");
		//GameEventsManager.Instance.Fire(new TopicSelectedEvent("Event_Tutorial", "Priya"));
		//GameEventsManager.Instance.Fire(new BeginTutorialEvent());
	}
	
	// Update is called once per frame
	void Update () {

		//	Mom waits until HARTO finishes talking
		if (onposition == false && moveMom) 
		{

			x = x + 0.05f;

			this.gameObject.transform.position = new Vector3 (x, -3.5f, 0);

		}

		if (x < -10f) {
			Destroy (this);
		
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{	
		if(col.gameObject.tag == "Player")
		{
			GameEventsManager.Instance.Fire(new BeginGameEvent());
		
			onposition = true;
		}
		Debug.Log (x);
		//onposition = true;
	}
}
