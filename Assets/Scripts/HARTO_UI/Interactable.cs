using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;

public class Interactable : MonoBehaviour 
{

	[System.Serializable]
	public class Action
	{
		public Color color;
		public Sprite sprite;
		public string title;
	}

	public bool inConversation;
	public bool isHARTOActive;
	public bool recordingFolderSelected;
	public bool topicSelected;
	public bool dialogueModeActive;
	public KeyCode toggleHARTO = KeyCode.Tab;
	public KeyCode toggleDialogueMode = KeyCode.BackQuote;

	public Action[] options;

	public Action[] topics;
	public Action[] emotions;
	public Action[] recordingFolders;
	public Action[] recordings_Dad;
	public Action[] recordings_Astrid;
	public Action[] recordings_Ruth;
	public Action[] recordings_ABC;
	public Action[] recordings_Note;

	private RecordingFolderSelectedEvent.Handler onRecordingFolderSelecetd;
	private TopicSelectedEvent.Handler onTopicSelecetd;
	private BeginDialogueEvent.Handler onBeginDialogueEvent;
	private EndDialogueEvent.Handler onDialogueEnded;

	void Start()
	{
		isHARTOActive = false;
		dialogueModeActive = false;
		recordingFolderSelected = false;
		topicSelected = false;

		options = recordingFolders;

		onRecordingFolderSelecetd = new RecordingFolderSelectedEvent.Handler(OnRecordingFolderSelected);
		onTopicSelecetd = new TopicSelectedEvent.Handler(OnTopicSelected);
		onBeginDialogueEvent = new BeginDialogueEvent.Handler(OnBeginDialogueEvent);
		onDialogueEnded = new EndDialogueEvent.Handler(OnDialogueEnded);

		GameEventsManager.Instance.Register<RecordingFolderSelectedEvent>(onRecordingFolderSelecetd);
		GameEventsManager.Instance.Register<TopicSelectedEvent>(onTopicSelecetd);
		GameEventsManager.Instance.Register<BeginDialogueEvent>(onBeginDialogueEvent);
		GameEventsManager.Instance.Register<EndDialogueEvent>(onDialogueEnded);
		
	}

	void ReloadMenu(Action[] newOptions)
	{
		RadialMenuSpawner.instance.DestroyMenu();
		options = newOptions;
		RadialMenuSpawner.instance.SpawnMenu(this, dialogueModeActive, topicSelected);
	}

	public void ToggleDialogueMode()
	{
		if (isHARTOActive)
		{
			dialogueModeActive = !dialogueModeActive;
			if(dialogueModeActive)
			{
				ReloadMenu(topics);
			}
			else
			{
				ReloadMenu(recordingFolders);
			}
		}
	}

	void OnRecordingFolderSelected(GameEvent e)
	{
		RadialMenuSpawner.instance.DestroyMenu();
		if(((RecordingFolderSelectedEvent)e).folder.Contains("Dad"))
		{
			ReloadMenu(recordings_Dad);
		}
		else if (((RecordingFolderSelectedEvent)e).folder.Contains("Ruth"))
		{
			ReloadMenu(recordings_Ruth);
		}
		else if (((RecordingFolderSelectedEvent)e).folder.Contains("Astrid"))
		{
			ReloadMenu(recordings_Astrid);
		}
		else if (((RecordingFolderSelectedEvent)e).folder.Contains("ABC"))
		{
			ReloadMenu(recordings_ABC);
		}
		else if (((RecordingFolderSelectedEvent)e).folder.Contains("Note"))
		{
			ReloadMenu(recordings_Note);
		}

		recordingFolderSelected = true;
	}

	void OnTopicSelected(GameEvent e)
	{
		topicSelected = true;
		ReloadMenu(emotions);
		
	}

	void OnBeginDialogueEvent(GameEvent e)
	{
		inConversation = true;
	}

	void OnDialogueEnded(GameEvent e)
	{
		RadialMenuSpawner.instance.DestroyMenu();
		options = topics;
		topicSelected = false;
		inConversation = false;
		
	}

	// Update is called once per frame
	void Update () 
	{


		if (Input.GetKeyDown(toggleHARTO) && !inConversation)
		{
			isHARTOActive = !isHARTOActive;
			if(isHARTOActive)
			{
				RadialMenuSpawner.instance.SpawnMenu(this, dialogueModeActive, topicSelected);
			}
			else
			{
				RadialMenuSpawner.instance.DestroyMenu();
			}
		}

		if(Input.GetKeyDown(toggleDialogueMode) && isHARTOActive)
		{
			dialogueModeActive = !dialogueModeActive;
			if(!dialogueModeActive)
			{
				ReloadMenu(recordingFolders);
			}
			else
			{
				ReloadMenu(topics);
			}
		}
	}
}
