using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;
using ChrsUtils.EasingEquations;
using ChrsUtils;

public class HARTO_UI_Interface : MonoBehaviour 
{

	public static HARTO_UI_Interface HARTOSystem;

	[System.Serializable]
	public class Action
	{
		public bool alreadySelected;
		public Color color;
		public Sprite sprite;
		public string title;
	}

	public bool isHARTOOn;
	public bool inConversation;
	public bool isHARTOActive;
	public bool recordingFolderSelected;
	public bool topicSelected;
	public bool dialogueModeActive;
	public float nextTimeToSearch = 0;				//	How long unitl the camera searches for the target again
	private const string PLAYER_TAG = "Player";
	public KeyCode toggleHARTO = KeyCode.Tab;
	public AudioClip clip;
	public AudioSource audioSource;
	public Player player;

	public Action[] options;
	public Action[] titleMenu;

	public Action[] empty;
	public Action[] topics;
	public Action[] updatedTopics = new Action[4];
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

		//	TODO: HARTO SCREEN
		//			When not talking to anyone and opening HARTO only make end convo button appear
		if(HARTOSystem == null)
		{
			HARTOSystem = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		closingHARTOForFirstTime = true;
		isHARTOActive = false;
		dialogueModeActive = true;
		recordingFolderSelected = false;
		closedTutorialUsingRecordingSwitch = false;
		audioSource = GetComponent<AudioSource>();

		if (!GameManager.instance.isTestScene)
		{
			topicSelected = true;
			options = emotions;
		}
		else
		{
			options = topics;
		}

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
		Debug.Log(newOptions[0].title);
		Debug.Log("Talking To: " + player.npcAstridIsTalkingTo);
		if(player.npcAstridIsTalkingTo == null)
		{
				newOptions = empty;
		}
		RadialMenuSpawner.instance.SpawnMenu(this, player,dialogueModeActive, topicSelected);
		Debug.Log("Done");
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
				if(((TopicSelectedEvent)e).topicName == "Topic_Exit")
				{
					closingHARTOForFirstTime = false;
				}
				topics[i].alreadySelected = true;
				topics[i].color = new Color (0.5f, 0.5f,0.5f, 1.0f);
				topicSelected = true;
				GameManager.instance.completedOneTopic = true;
				for(int j = 0; j < topics.Length; j++)
				{
					updatedTopics[j] = topics[j];
				}
				ReloadMenu(emotions);
				break;
			}
		}

		if (GameManager.instance.completedOneTopic)
		{
			topics = updatedTopics;
		}
		
	}

	void OnBeginDialogueEvent(GameEvent e)
	{
		inConversation = true;
		GameManager.instance.inConversation = inConversation;
	}

	void OnDialogueEnded(GameEvent e)
	{
		Debug.Log(((EndDialogueEvent)e).topicName);
		if(((EndDialogueEvent)e).topicName.Contains("Exit") && !closedTutorialUsingRecordingSwitch)
		{
			isHARTOActive = false;
			RadialMenuSpawner.instance.DestroyMenu();
			GameEventsManager.Instance.Fire(new DisablePlayerMovementEvent(false));
			inConversation = false;
			GameManager.instance.inConversation = inConversation;
			return;
		}
		
		if(closedTutorialUsingRecordingSwitch)
		{
			ReloadMenu(recordingFolders);
			GameEventsManager.Instance.Fire(new DisablePlayerMovementEvent(false));
			closedTutorialUsingRecordingSwitch = false;
		}
		else
		{
			Debug.Log("In here");
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

	public IEnumerator WaitForExitScript()
	{
		yield return new WaitForSeconds(14.0f);
		RadialMenuSpawner.instance.DestroyMenu();
	}

	void FindPlayer()
	{
		if (nextTimeToSearch <= Time.time)
		{
			GameObject result = GameObject.FindGameObjectWithTag (PLAYER_TAG);
			if (result != null)
			{
				player = result.GetComponent<Player>();
			}
			nextTimeToSearch = Time.time + 2.0f;
		}
	}

	// Update is called once per frame
	void Update () 
	{

		if (player == null)
		{
			FindPlayer ();
			return;
		}

		if (Input.GetKeyDown(toggleHARTO) && !inConversation && (GameManager.instance.hasPriyaSpoken || GameManager.instance.isTestScene))
		{
			GameEventsManager.Instance.Fire(new ToggleHARTOEvent());
			isHARTOActive = !isHARTOActive;
			if (!isHARTOActive && !GameManager.instance.completedOneTopic && !GameManager.instance.isTestScene)
			{
				isHARTOActive = true;
			}
			else
			{
				if(isHARTOActive)
				{
					
					if(player.npcAstridIsTalkingTo == null)
					{
						topics = empty;
					}
					RadialMenuSpawner.instance.SpawnMenu(this, player,dialogueModeActive, topicSelected);
					GameEventsManager.Instance.Fire(new DisablePlayerMovementEvent(true));
					isHARTOOn = true;
				}
				else
				{
					if (closingHARTOForFirstTime && !GameManager.instance.isTestScene)
					{
						GameEventsManager.Instance.Fire(new ClosingHARTOForTheFirstTimeEvent());
						closingHARTOForFirstTime = false;
						isHARTOOn = false;
						StartCoroutine(WaitForExitScript());
					}
					else
					{
						recordingFolderSelected = false;
						isHARTOOn = false;
						RadialMenuSpawner.instance.DestroyMenu();
					}
					
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
