using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;

public class DialogueManager : MonoBehaviour 
{
	public HARTO astridHARTO;
	public EventScript[] Events;

	
	private int _scene;
	public int SceneNumber
	{
		get {	return _scene;	}
		set {	_scene = value;	}
	}

	public const string SCENE = "SCENE_";
	public const string TOPIC_PREFIX = "Topic_";
	public const string EVENT_PREFIX = "Event_";
	
	public const string EVENT_START_GAME = "Event_Start_Game";
	public const string EVENT_MEETING_TUTORIAL = "Event_Tutorial";
	public const string EVENT_EXIT = "Event_Exit";

	public const string NPC_PRIYA = "Priya";

	private BeginGameEvent.Handler onBeginGame;
	private BeginTutorialEvent.Handler onBeginTutorial;
	private TopicSelectedEvent.Handler onTopicSelected;
	private ClosingHARTOForTheFirstTimeEvent.Handler onClosingHARTOForTheFirstTime;
	

	// Use this for initialization
	void Start () 
	{
		

		astridHARTO = GameObject.FindGameObjectWithTag("HARTO").GetComponent<HARTO>();

		SceneNumber = 1;
		onBeginGame = new BeginGameEvent.Handler(OnBeginGame);
		onBeginTutorial = new BeginTutorialEvent.Handler(OnBeginTutorial);
		onTopicSelected = new TopicSelectedEvent.Handler(OnTopicSelected);
		onClosingHARTOForTheFirstTime = new ClosingHARTOForTheFirstTimeEvent.Handler(OnClosingHARTOForTheFirstTime);

		Services.Events.Register<BeginGameEvent>(onBeginGame);
		Services.Events.Register<BeginTutorialEvent>(onBeginTutorial);
		Services.Events.Register<TopicSelectedEvent>(onTopicSelected);
		Services.Events.Register<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);

	}

	void OnBeginGame(GameEvent e)
	{
		InitDialogueEvent(EVENT_START_GAME, 1, NPC_PRIYA, true);
	}

	void OnBeginTutorial(GameEvent e)
	{
		InitDialogueEvent(EVENT_MEETING_TUTORIAL, 1, NPC_PRIYA, false);
	}

	void OnClosingHARTOForTheFirstTime(GameEvent e)
	{
		InitDialogueEvent(EVENT_EXIT, 1, NPC_PRIYA, true);
	}

	void OnTopicSelected(GameEvent e)
	{
		string selectedEvent = EVENT_PREFIX + ((TopicSelectedEvent)e).topicName.Replace(TOPIC_PREFIX, "");
		bool astridTalksFirst =  GameManager.instance.whoTalksFirst[selectedEvent+SceneNumber +((TopicSelectedEvent)e).npcName];
		InitDialogueEvent(selectedEvent, SceneNumber,((TopicSelectedEvent)e).npcName, astridTalksFirst);
		
		try
		{
			
		}
		catch (Exception ex)
		{
			Debug.Log("You are not talking to an NPC or the current NPC is not attached to this event. Stack Trace: " + ex.StackTrace);
		}
		
	}



	void InitDialogueEvent(string topic, int sceneNumber ,string npcName, bool astridTralksFirst)
	{
		GameObject sceneFolder = GameObject.Find(SCENE + SceneNumber);

		if (sceneFolder != null)
		{
			EventScript thisEvent = sceneFolder.transform.FindChild(topic).GetComponent<EventScript>();
			if (thisEvent != null)
			{
				thisEvent.InitResponseScriptWith(npcName, astridTralksFirst);
			}
			else
			{
				Debug.Log("Error: " + topic + "'s EventScript Component not found");
			}
		}
		else
		{
			Debug.Log("Error: " + SCENE + SceneNumber + " not found");
		}
	}
}
