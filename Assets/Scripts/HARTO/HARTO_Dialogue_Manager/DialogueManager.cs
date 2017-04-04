using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;

public class DialogueManager : MonoBehaviour 
{

	public static DialogueManager instance;	
	public HARTO astridHARTO;
	public EventScript[] Events;

	
	private int RelationLevel; 
	private int _scene;
	public int SceneNumber
	{
		get {	return _scene;	}
		set {	_scene = value;	}
	}

	public const string SCENE = "SCENE_";
	public const string TOPIC_PREFIX = "Topic_";
	public const string EVENT_PREFIX = "Event_";
	public const string EVENT_ASTRID_TALKS_FIRST = "@";
	public const string EVENT_MEETING_TUTORIAL = "Event_Tutorial";
	public const string EVENT_MEETING_STARTS = "Event_Meeting";
	public const string EVENT_BROCA_STARTS = "Event_BrocaParticles";
	public const string EVENT_RUTH_STARTS = "Event_Ruth";
	public const string EVENT_EXIT_STARTS = "Event_Ruth";
	
	public const string EVENT_UTAN_ASTRID_STARTS = "Event_Utan@";
	public const string PLAYER_ASTRID = "Astrid";

	public const string NPC_TAG = "NPC_";
	public const string NPC_PRIYA = "Priya";
	public const string NPC_MALI = "Mali";

	private TopicSelectedEvent.Handler onTopicSelected;

	// Use this for initialization
	void Start () 
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		astridHARTO = GameObject.FindGameObjectWithTag("HARTO").GetComponent<HARTO>();

		SceneNumber = 1;
		onTopicSelected = new TopicSelectedEvent.Handler(OnTopicSelected);

		GameEventsManager.Instance.Register<TopicSelectedEvent>(onTopicSelected);

	}

	void OnTopicSelected(GameEvent e)
	{
		string selectedEvent = EVENT_PREFIX + ((TopicSelectedEvent)e).topicName.Replace(TOPIC_PREFIX, "");
		InitDialogueEvent(selectedEvent, SceneNumber,((TopicSelectedEvent)e).npcName);
		
		try
		{
			
		}
		catch (Exception ex)
		{
			Debug.Log("You are not talking to an NPC or the current NPC is not attached to this event. Stack Trace: " + ex.StackTrace);
		}
		
	}



	void InitDialogueEvent(string topic, int sceneNumber ,string npcName)
	{
		GameObject sceneFolder = GameObject.Find(SCENE + SceneNumber);

		if (sceneFolder != null)
		{
			EventScript thisEvent = sceneFolder.transform.FindChild(topic).GetComponent<EventScript>();
			if (thisEvent != null)
			{
				thisEvent.InitResponseScriptWith(npcName);
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
