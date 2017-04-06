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
	public KeyCode toggleDialogueMode = KeyCode.BackQuote;

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


	//	If closing HARTo for the first time, wait until Exit dialouge event finishes.
	private bool closingHARTOForFirstTime;
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
			clip = Resources.Load("Audio/SFX/FUTURE_BEEPS_LITE/R2D2/R2D2_Low_0004") as AudioClip;

			if(!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(clip);
			}

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
		ReloadMenu(topics);
		topicSelected = false;
		inConversation = false;
		
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
