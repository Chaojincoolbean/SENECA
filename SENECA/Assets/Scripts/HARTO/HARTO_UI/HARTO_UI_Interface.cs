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

    public bool usingBeornsHARTO;
    public bool allBeornRecordingsPlayed;
    public bool clipHasBeenPlayed;
    public bool canDisableHARTO;
    public bool allRuthRecordingsPlayed;
	public bool isHARTOOn;
	public bool inConversation;
	public bool isHARTOActive;
	public bool recordingFolderSelected;
	public bool topicSelected;
	public bool dialogueModeActive;
	public float nextTimeToSearch = 0;				//	How long unitl the camera searches for the target again
    public float nextTimeToSearch2 = 0;              //	How long unitl the camera searches for the target again
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
    public Action[] recordingFoldersBeorn;
	public Action[] recordings_Ruth;
	public Action[] recordings_Beorn;

    public RadialMenuSpawner menuSpawner;


	//	If closing HARTO for the first time, wait until Exit dialouge event finishes.
	private bool closingHARTOForFirstTime;
	private bool closedTutorialUsingRecordingSwitch;
	public string currentNPC;
	private RecordingFolderSelectedEvent.Handler onRecordingFolderSelecetd;
    private RecordingSelectedEvent.Handler onRecordingSelected;
    private TopicSelectedEvent.Handler onTopicSelecetd;
	private BeginDialogueEvent.Handler onBeginDialogueEvent;
	private EndDialogueEvent.Handler onDialogueEnded;
	private RecordingIsOverEvent.Handler onRecordingEnded;

	void Start()
	{
		if(HARTOSystem == null)
		{
			HARTOSystem = this;
		}

        clipHasBeenPlayed = false;
        closingHARTOForFirstTime = true;
        allRuthRecordingsPlayed = false;
        allBeornRecordingsPlayed = false;
		isHARTOActive = false;
		dialogueModeActive = true;
		recordingFolderSelected = false;
		closedTutorialUsingRecordingSwitch = false;
        canDisableHARTO = true;
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
        onRecordingSelected = new RecordingSelectedEvent.Handler(OnRecordingSelected);
        onTopicSelecetd = new TopicSelectedEvent.Handler(OnTopicSelected);
		onBeginDialogueEvent = new BeginDialogueEvent.Handler(OnBeginDialogueEvent);
		onDialogueEnded = new EndDialogueEvent.Handler(OnDialogueEnded);
		onRecordingEnded = new RecordingIsOverEvent.Handler(OnRecordingEnded);

		Services.Events.Register<RecordingFolderSelectedEvent>(onRecordingFolderSelecetd);
        Services.Events.Register<RecordingSelectedEvent>(onRecordingSelected);
        Services.Events.Register<TopicSelectedEvent>(onTopicSelecetd);
		Services.Events.Register<BeginDialogueEvent>(onBeginDialogueEvent);
		Services.Events.Register<EndDialogueEvent>(onDialogueEnded);
		Services.Events.Register<RecordingIsOverEvent>(onRecordingEnded);
		
	}

    private void OnDestroy()
    {
        Services.Events.Unregister<RecordingFolderSelectedEvent>(onRecordingFolderSelecetd);
        Services.Events.Unregister<RecordingSelectedEvent>(onRecordingSelected);
        Services.Events.Unregister<TopicSelectedEvent>(onTopicSelecetd);
        Services.Events.Unregister<BeginDialogueEvent>(onBeginDialogueEvent);
        Services.Events.Unregister<EndDialogueEvent>(onDialogueEnded);
        Services.Events.Unregister<RecordingIsOverEvent>(onRecordingEnded);
    }

    void ReloadMenu(Action[] newOptions)
	{
		menuSpawner.DestroyMenu();
		options = newOptions;
		if(player.npcAstridIsTalkingTo == null)
		{
				newOptions = empty;
		}
        menuSpawner.SpawnMenu(this, player,dialogueModeActive, topicSelected);
	}

	public void ToggleDialogueMode()
	{
		if (isHARTOActive)
		{
			clip = Resources.Load("Audio/SFX/HARTO_Click_To_Recording") as AudioClip;

			if(!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(clip);
			}

			if(closingHARTOForFirstTime && !usingBeornsHARTO)
			{
				
				Services.Events.Fire(new ClosingHARTOForTheFirstTimeEvent());
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
                if(usingBeornsHARTO)
                {
                    ReloadMenu(recordings_Beorn);
                }
                else
                {
                    ReloadMenu(recordingFolders);
                }
				
			}
		}
	}

	void OnRecordingFolderSelected(GameEvent e)
	{
        menuSpawner.DestroyMenu();
        if (((RecordingFolderSelectedEvent)e).folder.Contains("Ruth"))
		{
			ReloadMenu(recordings_Ruth);
		}
		else if (((RecordingFolderSelectedEvent)e).folder.Contains("Beorn"))
		{
			ReloadMenu(recordings_Beorn);
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
		if(((EndDialogueEvent)e).topicName.Contains("Exit") && !closedTutorialUsingRecordingSwitch)
		{
			isHARTOActive = false;
            menuSpawner.DestroyMenu();
			Services.Events.Fire(new DisablePlayerMovementEvent(false));
			inConversation = false;
			GameManager.instance.inConversation = inConversation;
			GameManager.instance.wasdUIOnScreen = true;
			Services.Events.Fire (new WASDUIAppearEvent());
			return;
		}
		
		if(closedTutorialUsingRecordingSwitch)
		{
            if (!usingBeornsHARTO)
            {
                ReloadMenu(recordingFolders);
            }
            else
            {
                ReloadMenu(recordings_Beorn);
            }
			Services.Events.Fire(new DisablePlayerMovementEvent(false));
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

    void OnRecordingSelected(GameEvent e)
    {
        //canDisableHARTO = false;
        if (!usingBeornsHARTO)
        {
            if (allRuthRecordingsPlayed)
            {

            }
            else
            {
                if (((RecordingSelectedEvent)e).recording.Contains(recordings_Ruth[0].title))
                {
                    recordings_Ruth[0].alreadySelected = true;
                    recordings_Ruth[0].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Ruth[1].alreadySelected = false;
                    recordings_Ruth[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    recordings_Ruth[2].alreadySelected = true;
                    recordings_Ruth[2].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);

                }
                else if (((RecordingSelectedEvent)e).recording.Contains(recordings_Ruth[1].title))
                {
                    recordings_Ruth[0].alreadySelected = true;
                    recordings_Ruth[0].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Ruth[1].alreadySelected = true;
                    recordings_Ruth[1].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Ruth[2].alreadySelected = false;
                    recordings_Ruth[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else if (((RecordingSelectedEvent)e).recording.Contains(recordings_Ruth[2].title))
                {
                    recordings_Ruth[0].alreadySelected = false;
                    recordings_Ruth[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    recordings_Ruth[1].alreadySelected = false;
                    recordings_Ruth[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    recordings_Ruth[2].alreadySelected = false;
                    recordings_Ruth[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

                    allRuthRecordingsPlayed = true;
                }

                ReloadMenu(recordings_Ruth);
            }
        }
        else
        {
            if (allBeornRecordingsPlayed)
            {

            }
            else
            {
                if (((RecordingSelectedEvent)e).recording.Contains(recordings_Beorn[0].title))
                {
                    recordings_Beorn[0].alreadySelected = true;
                    recordings_Beorn[0].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[1].alreadySelected = false;
                    recordings_Beorn[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    recordings_Beorn[2].alreadySelected = true;
                    recordings_Beorn[2].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[3].alreadySelected = true;
                    recordings_Beorn[3].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[4].alreadySelected = true;
                    recordings_Beorn[4].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);

                }
                else if (((RecordingSelectedEvent)e).recording.Contains(recordings_Beorn[1].title))
                {
                    recordings_Beorn[0].alreadySelected = true;
                    recordings_Beorn[0].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[1].alreadySelected = true;
                    recordings_Beorn[1].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[2].alreadySelected = false;
                    recordings_Beorn[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    recordings_Beorn[3].alreadySelected = true;
                    recordings_Beorn[3].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    recordings_Beorn[4].alreadySelected = true;
                    recordings_Beorn[4].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                }
                else if (((RecordingSelectedEvent)e).recording.Contains(recordings_Beorn[2].title))
                {
                    recordings_Beorn[0].alreadySelected = true;
                    recordings_Beorn[0].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[1].alreadySelected = true;
                    recordings_Beorn[1].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[2].alreadySelected = true;
                    recordings_Beorn[2].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[3].alreadySelected = false;
                    recordings_Beorn[3].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    recordings_Beorn[4].alreadySelected = true;
                    recordings_Beorn[4].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                }
                else if (((RecordingSelectedEvent)e).recording.Contains(recordings_Beorn[3].title))
                {
                    recordings_Beorn[0].alreadySelected = false;
                    recordings_Beorn[0].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[1].alreadySelected = false;
                    recordings_Beorn[1].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[2].alreadySelected = false;
                    recordings_Beorn[2].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[3].alreadySelected = false;
                    recordings_Beorn[3].color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    recordings_Beorn[4].alreadySelected = true;
                    recordings_Beorn[4].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

                }
                else if (((RecordingSelectedEvent)e).recording.Contains(recordings_Beorn[4].title))
                {
                    recordings_Beorn[0].alreadySelected = true;
                    recordings_Beorn[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    recordings_Beorn[1].alreadySelected = true;
                    recordings_Beorn[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    recordings_Beorn[2].alreadySelected = true;
                    recordings_Beorn[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    recordings_Beorn[3].alreadySelected = true;
                    recordings_Beorn[3].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    recordings_Beorn[4].alreadySelected = true;
                    recordings_Beorn[4].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

                    allBeornRecordingsPlayed = true;
                }

                ReloadMenu(recordings_Beorn);
            }
        }
    }

    void OnRecordingEnded(GameEvent e)
	{
		recordingFolderSelected = false;
        canDisableHARTO = true;
		//ReloadMenu(recordingFolders);
	}

	public IEnumerator WaitForExitScript()
	{
		yield return new WaitForSeconds(14.0f);
        menuSpawner.DestroyMenu();
        Services.Events.Fire(new DisablePlayerMovementEvent(false));
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

    void FindRadialMenuSpawner()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject result = GameObject.Find("HARTOCanvas");
            if (result != null)
            {
                menuSpawner = result.GetComponent<RadialMenuSpawner>();
            }
            nextTimeToSearch2 = Time.time + 2.0f;
        }
    }

    // Update is called once per frame
    void Update () 
	{
        if(GameManager.instance.pickedUpBeornsHARTO == true)
        {
            usingBeornsHARTO = true;
        }

		if (player == null)
		{
			FindPlayer ();
			return;
		}

        if(menuSpawner == null)
        {
            FindRadialMenuSpawner();
            return;
        }

		if (Input.GetKeyDown(toggleHARTO) && !inConversation && (GameManager.instance.hasPriyaSpoken || GameManager.instance.isTestScene) && canDisableHARTO)
		{
			
			Services.Events.Fire(new ToggleHARTOEvent());
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
                    menuSpawner.SpawnMenu(this, player,dialogueModeActive, topicSelected);
                    clip = Resources.Load("Audio/SFX/HARTO_SFX/OpenHARTO") as AudioClip;

                    if (!audioSource.isPlaying && !clipHasBeenPlayed)
                    {
                        //audioSource.PlayOneShot(clip);
                    }
                    Services.Events.Fire(new DisablePlayerMovementEvent(true));
                    if(!audioSource.isPlaying)
                    {
                        clipHasBeenPlayed = true;
                        audioSource.PlayOneShot(clip);
                    }
					isHARTOOn = true;
				}
				else
				{
					if (closingHARTOForFirstTime && !GameManager.instance.isTestScene)
					{
						Services.Events.Fire(new ClosingHARTOForTheFirstTimeEvent());
						closingHARTOForFirstTime = false;
						isHARTOOn = false;
						StartCoroutine(WaitForExitScript());
					}
					else
					{
						recordingFolderSelected = false;
						isHARTOOn = false;
                        menuSpawner.DestroyMenu();
                        Services.Events.Fire(new DisablePlayerMovementEvent(false));
                    }


                }
			}
		}
	}
}
