using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;

public class Mom : MonoBehaviour 
{

	bool beginGame;
	public bool moveMom;
	public bool tutorialBegan;
	float x;
	bool onposition;
	private MoveMomEvent.Handler onMoveMomEvent;
	private ToggleHARTOEvent.Handler onToggleHARTO;
	private ClosingHARTOForTheFirstTimeEvent.Handler onClosingHARTOForTheFirstTime;

	// Use this for initialization
	void Start () 
	{
		beginGame = false;
		gameObject.name = "Priya";
		moveMom = false;
		tutorialBegan = false;
		x = -10f;
		onposition = false;

		onMoveMomEvent = new MoveMomEvent.Handler(OnMoveMom);
		onToggleHARTO = new ToggleHARTOEvent.Handler(OnToggleHARTO);
		onClosingHARTOForTheFirstTime = new ClosingHARTOForTheFirstTimeEvent.Handler(OnClosingHARTOForTheFirstTime);

		GameEventsManager.Instance.Register<MoveMomEvent>(onMoveMomEvent);
		GameEventsManager.Instance.Register<ToggleHARTOEvent>(onToggleHARTO);
		GameEventsManager.Instance.Register<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
	}

	void OnClosingHARTOForTheFirstTime(GameEvent e)
	{
		Collider2D[] colliders = GetComponents<Collider2D>();
		for(int i = 0;i < colliders.Length; i++)
		{
			if(colliders[i].isTrigger)
			{
				colliders[i].enabled = false;
			}
		}
		
	}

	void OnMoveMom(GameEvent e)
	{
		moveMom = true;
	}

	void OnToggleHARTO(GameEvent e)
	{
		if (!tutorialBegan && !GameManager.instance.isTestScene)
		{
			tutorialBegan = true;
			GameEventsManager.Instance.Fire(new BeginTutorialEvent());	
		}
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
		if(col.gameObject.tag == "Player" && !beginGame)
		{
			beginGame = true;
			GameEventsManager.Instance.Fire(new BeginGameEvent());
			GameManager.instance.hasPriyaSpoken = true;
			onposition = true;
		}
		//onposition = true;
	}
}
