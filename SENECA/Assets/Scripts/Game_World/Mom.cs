using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;

public class Mom : MonoBehaviour 
{

	public bool beginGame;
	public bool moveMom;
	public bool tutorialBegan;
	float x;
	float y;
	public bool onposition;
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

		Services.Events.Register<MoveMomEvent>(onMoveMomEvent);
		Services.Events.Register<ToggleHARTOEvent>(onToggleHARTO);
		Services.Events.Register<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
	}

    private void OnDestroy()
    {
        Services.Events.Unregister<MoveMomEvent>(onMoveMomEvent);
        Services.Events.Unregister<ToggleHARTOEvent>(onToggleHARTO);
        Services.Events.Unregister<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
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

		Debug.Log("mom");
		
	}

	void OnMoveMom(GameEvent e)
	{
		moveMom = true;
	}

	void OnToggleHARTO(GameEvent e)
	{
		if (!tutorialBegan && !GameManager.instance.isTestScene)
		{
            GameManager.instance.hasPriyaSpoken = true;
			tutorialBegan = true;
			Services.Events.Fire(new BeginTutorialEvent());	
		}
	}
	
	// Update is called once per frame
	void Update () {

		//	Mom waits until HARTO finishes talking
		if (onposition == false && moveMom) 
		{

			x = x + 0.05f;

			if (y > 1f) {
				y -= 0.1f;
			} else if(y > -1f){
				y += 0.1f;
			}

			this.gameObject.transform.position = new Vector3 (x, -3.5f, 0);
			this.gameObject.transform.position += new Vector3 (0f, y, 0f);

		}

		if (x < -20f) {
			Destroy (this);
		
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.name == "Marker" && !beginGame)
		{
			beginGame = true;
			Services.Events.Fire(new BeginGameEvent());
			SenecaCampsiteSceneScript.hasPriyaSpoken = true;
			onposition = true;
		}
	}
}
