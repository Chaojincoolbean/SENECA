using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;
using SenecaEvents;


public  enum  Emotions
{
	None,
	Happy,
	Question,
	Depressed,
	Angry
}
public class HARTO : MonoBehaviour 
{

	public static HARTO instance;
	[SerializeField]
	private Emotions emotion;
	public Emotions CurrentEmotion
	{
		get
		{
			return emotion;
		}
		set
		{
			emotion = value;
		}
	}

	private EmotionSelectedEvent.Handler onEmotionSelected;
	// Use this for initialization
	void Start () 
	{

		if(instance == null)
		{
			instance = this;
		}

		onEmotionSelected = new EmotionSelectedEvent.Handler(OnEmotionSelected);
		Services.Events.Register<EmotionSelectedEvent>(onEmotionSelected);
	}
	
	void OnEmotionSelected(GameEvent e)
	{
		 emotion = ((EmotionSelectedEvent)e).emotion;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
