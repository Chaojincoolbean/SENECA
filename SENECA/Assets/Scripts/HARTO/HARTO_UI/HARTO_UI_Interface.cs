using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;

public class HARTO_UI_Interface : MonoBehaviour 
{

	[System.Serializable]
	public class Action
	{
		public bool alreadySelected;
		public Color color;
		public Sprite sprite;
		public string title;
	}

	public bool inConversation;
	public bool isHARTOActive;
	public bool recordingFolderSelected;
	public bool topicSelected;
	public bool dialogueModeActive;
	private const string PLAYER_TAG = "Player";
	public KeyCode toggleHARTO = KeyCode.Tab;
	public AudioClip clip;
	public AudioSource audioSource;
	public Player player;

	public Action[] options;

	public Action[] topics;
	public Action[] emotions;
	public Action[] recordingFolders;
	public Action[] recordings_Dad;
	public Action[] recordings_Astrid;
	public Action[] recordings_Ruth;
	public Action[] recordings_ABC;
	public Action[] recordings_Note;


	//	If closing HARTO for the first time, wait until Exit dialouge event finishes.
	private bool closingHARTOForFirstTime;
	private bool closedTutorialUsingRecordingSwitch;
	public string currentNPC;
	private RecordingFolderSelectedEvent.Handler onRecordingFolderSelecetd;
	private TopicSelectedEvent.Handler onTopicSelecetd;
	private BeginDialogueEvent.Handler onBeginDialogueEvent;
	private EndDialogueEvent.Handler onDialogueEnded;
	private RecordingIsOverEvent.Handler onRecordingEnded;

	void Start()
	{
		closingHARTOForFirstTime = true;
		isHARTOActive = false;
		dialogueModeActive = true;
		recordingFolderSelected = false;
		topicSelected = true;
		closedTutorialUsingRecordingSwitch = false;
		audioSource = GetComponent<AudioSource>();

		player = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<Player>();

		options = emotions;

		onRecordingFolderSelecetd = new RecordingFolderSelectedEvent.Handler(OnRecordingFolderSelected);
		onTopicSelecetd = new TopicSelectedEvent.Handler(OnTopicSelected);
		onBeginDialogueEvent = new BeginDialogueEvent.Handler(OnBeginDialogueEvent);
		onDialogueEnded = new EndDialogueEvent.Handler(OnDialogueEnded);
		onRecordingEnded = new RecordingIsOverEvent.Handler(OnRecordingEnded);

		GameEventsManager.Instance.Register<RecordingFolderSelectedEvent>(onRecordingFolderSelecetd);
		GameEventsManager.Instance.Register<TopicSelectedEvent>(onTopicSelecetd);
		GameEventsManager.Instance.Register<BeginDialogueEvent>(onBeginDialogueEvent);
		GameEventsManager.Instance.Register<EndDialogueEvent>(onDialogueEnded);
		GameEventsManager.Instance.Register<RecordingIsOverEvent>(onRecordingEnded);
		
	}

	void ReloadMenu(Action[] newOptions)
	{
		RadialMenuSpawner.instance.DestroyMenu();
		options = newOptions;
		RadialMenuSpawner.instance.SpawnMenu(this, player,dialogueModeActive, topicSelected);
	}

	public void ToggleDialogueMode()
	{
		if (isHARTOActive)
		{
			clip = Resources.Load("Audio/SFX/HARTO_SFX/Technology Electronic Joystick Stick Moving 21") as AudioClip;

			if(!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(clip);
			}

			if(closingHARTOForFirstTime)
			{

				GameEventsManager.Instance.Fire(new ClosingHARTOForTheFirstTimeEvent());
				closingHARTOForFirstTime = false;
				closedTutorialUsingRecordingSwitch = true;
				// freeze mouse clicks here
			}
			dialogueModeActive = !dialogueModeActive;
			if(dialogueModeActive)
			{
				if (!topicSelected)
				{
					ReloadMenu(topics);
				}
				else
				{
					ReloadMenu(emotions);
				}
					
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
		Debug.Log(((TopicSelectedEvent)e).topicName);
		Debug.Log("!" + topics[2].title);
	

		if (currentNPC != ((TopicSelectedEvent)e).npcName)
		{
			currentNPC = ((TopicSelectedEvent)e).npcName;
			for(int i = 0; i < topics.Length; i++)
			{
				topics[i].alreadySelected = false;
				topics[i].color = Color.white;
			}
			topicSelected = true;
			ReloadMenu(emotions);
		}

			for (int i = 0; i < topics.Length; i++)
		{
			if (((TopicSelectedEvent)e).topicName == topics[i].title)
			{
				topics[i].alreadySelected = true;
				topics[i].color = new Color (0.5f, 0.5f,0.5f, 1.0f);
				topicSelected = true;
				GameManager.instance.completedOneTopic = true;
				ReloadMenu(emotions);
				break;
			}
		}
		
	}

	void OnBeginDialogueEvent(GameEvent e)
	{
		inConversation = true;
		GameManager.instance.inConversation = inConversation;
	}

	void OnDialogueEnded(GameEvent e)
	{
		if(closedTutorialUsingRecordingSwitch)
		{
			ReloadMenu(recordingFolders);
			closedTutorialUsingRecordingSwitch = false;
		}
		else
		{
			ReloadMenu(topics);
		}
		topicSelected = false;
		inConversation = false;
		GameManager.instance.inConversation = inConversation;
	}

	void OnRecordingEnded(GameEvent e)
	{
		recordingFolderSelected = false;
		ReloadMenu(recordingFolders);
	}

	IEnumerator WaitForExitScript()
	{
		yield return new WaitForSeconds(14.0f);
		RadialMenuSpawner.instance.DestroyMenu();
	}

	// Update is called once per frame
	void Update () 
	{


		if (Input.GetKeyDown(toggleHARTO) && !inConversation)
		{
			GameEventsManager.Instance.Fire(new ToggleHARTOEvent());
			isHARTOActive = !isHARTOActive;
			if(isHARTOActive)
			{
				RadialMenuSpawner.instance.SpawnMenu(this, player,dialogueModeActive, topicSelected);
			}
			else
			{
				if (closingHARTOForFirstTime)
				{
					GameEventsManager.Instance.Fire(new ClosingHARTOForTheFirstTimeEvent());
					closingHARTOForFirstTime = false;
					StartCoroutine(WaitForExitScript());
				}
				else
				{
					recordingFolderSelected = false;
					RadialMenuSpawner.instance.DestroyMenu();
				}
			}
		}

		// if(isHARTOActive)
		// {
		// 	//	dialogueModeActive = !dialogueModeActive;
		// 	if(!dialogueModeActive)
		// 	{
		// 		ReloadMenu(recordingFolders);
		// 	}
		// 	else
		// 	{
		// 		if(!topicSelected)
		// 		{
		// 			ReloadMenu(topics);
		// 		}
		// 		else
		// 		{
		// 			ReloadMenu(emotions);
		// 		}
		// 	}
		// }
	}
}
