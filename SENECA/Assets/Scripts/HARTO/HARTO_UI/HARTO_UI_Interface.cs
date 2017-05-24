using System.Collections;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;

#region HARTO_UI_Interface.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for all the icons on HARTO and removing and creating the menus using the RadialMenuSpawner            */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void OnDestroy()                                                                             */
/*                 private void ReloadMenu(Action[] newOptions)                                                         */
/*                 private void OnRecordingFolderSelected(GameEvent e)                                                  */
/*                 private void OnTopicSelected(GameEvent e)                                                            */
/*                 private void OnBeginDialogueEvent(GameEvent e)                                                       */
/*                 private void OnDialogueEnded(GameEvent e)                                                            */
/*                 private void OnRecordingSelected(GameEvent e)                                                        */
/*                 private void OnRecordingEnded(GameEvent e)                                                           */
/*                 private void FindPlayer()                                                                            */
/*                 private void FindRadialMenuSpawner()                                                                 */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/*          public:                                                                                                     */
/*                 public void ToggleDialogueMode()                                                                     */
/*                 public IEnumerator WaitForExitScript()                                                               */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class HARTO_UI_Interface : MonoBehaviour 
{
	public static HARTO_UI_Interface HARTOSystem;

    //This class shows the editable value of a Radial icon in the inspector
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
	public float nextTimeToSearch = 0;				 //	How long unitl the camera searches for the target again
    public float nextTimeToSearch2 = 0;              //	How long unitl the camera searches for the target again
    public string currentNPC;

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

	private bool closingHARTOForFirstTime;
	private bool closedTutorialUsingRecordingSwitch;
    private const string PLAYER_TAG = "Player";

    private RecordingFolderSelectedEvent.Handler onRecordingFolderSelecetd;
    private RecordingSelectedEvent.Handler onRecordingSelected;
	private TopicSelectedEvent.Handler onTopicSelected;
	private BeginDialogueEvent.Handler onBeginDialogueEvent;
	private EndDialogueEvent.Handler onDialogueEnded;
	private RecordingIsOverEvent.Handler onRecordingEnded;

    #region Overview private void Start()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Initalizing variables. Runs once at the beginning of the program                                                */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void Start()
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

        //  Setting delegates for these events
		onRecordingFolderSelecetd = new RecordingFolderSelectedEvent.Handler(OnRecordingFolderSelected);
        onRecordingSelected = new RecordingSelectedEvent.Handler(OnRecordingSelected);
        onTopicSelected = new TopicSelectedEvent.Handler(OnTopicSelected);
		onBeginDialogueEvent = new BeginDialogueEvent.Handler(OnBeginDialogueEvent);
		onDialogueEnded = new EndDialogueEvent.Handler(OnDialogueEnded);
		onRecordingEnded = new RecordingIsOverEvent.Handler(OnRecordingEnded);

        //  Registering the events to excute when the event is fired
		Services.Events.Register<RecordingFolderSelectedEvent>(onRecordingFolderSelecetd);
        Services.Events.Register<RecordingSelectedEvent>(onRecordingSelected);
        Services.Events.Register<TopicSelectedEvent>(onTopicSelected);
		Services.Events.Register<BeginDialogueEvent>(onBeginDialogueEvent);
		Services.Events.Register<EndDialogueEvent>(onDialogueEnded);
		Services.Events.Register<RecordingIsOverEvent>(onRecordingEnded);
	}

    #region Overview private void OnDestroy()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Unregistering for events when being destroyed to stop any null reference errors                                 */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnDestroy()
    {
        Services.Events.Unregister<RecordingFolderSelectedEvent>(onRecordingFolderSelecetd);
        Services.Events.Unregister<RecordingSelectedEvent>(onRecordingSelected);
        Services.Events.Unregister<TopicSelectedEvent>(onTopicSelected);
        Services.Events.Unregister<BeginDialogueEvent>(onBeginDialogueEvent);
        Services.Events.Unregister<EndDialogueEvent>(onDialogueEnded);
        Services.Events.Unregister<RecordingIsOverEvent>(onRecordingEnded);
    }

    #region Overview private void ReloadMenu(Action[] newOptions)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Reloading the HARTO screen by deleting the old one and making a new one in its place                            */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          Action[] newOptions: the icons on the reloaded HARTO screen                                                 */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void ReloadMenu(Action[] newOptions)
	{
		menuSpawner.DestroyMenu();
		options = newOptions;
		if(player.npcAstridIsTalkingTo == null)
		{
			newOptions = empty;
		}
		menuSpawner.SpawnMenu(this, player,dialogueModeActive, topicSelected, true);   
	}

    #region Overview public void ToggleDialogueMode()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*          Visual changes when in toggling dialogue and recording modes.                                               */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
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

    #region Overview private void OnRecordingFolderSelected(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*       Reloading the HARTO screen with the appropriate recordings                                                     */
    /*       OnRecordingelected event is fired in RadialMenu.cs in void DetermineEvent(RadialIcon icon)                     */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnRecordingFolderSelected(GameEvent e)
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

    #region Overview private void OnTopicSelected(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*       Reloading the HARTO screen with emotion icons                                                                  */
    /*       OnRecordingelected event is fired in RadialMenu.cs in void DetermineEvent(RadialIcon icon)                     */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnTopicSelected(GameEvent e)
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

    #region Overview private void OnBeginDialogueEvent(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*       Reloading the HARTO screen with topic icons                                                                    */
    /*       BeginDialogueEvent event is fired in EventScript.cs in                                                         */
    /*       void InitResponseScriptWith(string characterName, bool astridTalksFirst)                                       */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnBeginDialogueEvent(GameEvent e)
	{
		inConversation = true;
		GameManager.instance.inConversation = inConversation;
	}

    #region Overview private void OnDialogueEnded(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*       Reloading the HARTO screen with topic icons                                                                    */
    /*       DialogueEnded event is fired in EventScript.cs in IEnumerator PlayEventDialogue(string characterName)          */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnDialogueEnded(GameEvent e)
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
			Debug.Log ("reloading menu on dialogue end");
			ReloadMenu(topics);
		}
		topicSelected = false;
		inConversation = false;
		GameManager.instance.inConversation = inConversation;
	}

    #region Overview private void OnRecordingSelected(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*       Contorlling which recording you can listen to                                                                  */
    /*       OnRecordingelected event is fired in RadialMenu.cs in void DetermineEvent(RadialIcon icon)                     */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnRecordingSelected(GameEvent e)
    {
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

    #region Overview private void OnRecordingEnded(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*       Resetting the recording menu allowing the player to play another                                               */
    /*       OnRecordingelected event is fired in RecordingManager.cs in                                                    */
    /*       IEnumerator RecordingIsPlaying(float recordingLength)                                                          */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnRecordingEnded(GameEvent e)
	{
		recordingFolderSelected = false;
        canDisableHARTO = true;
	}

    #region Overview IEnumerator WaitForExitScript()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Freezing player's the End conversation is played between Astrid and Priya				                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The type of objects to enumerate.                                                                           */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public IEnumerator WaitForExitScript()
	{
		yield return new WaitForSeconds(14.0f);
        menuSpawner.DestroyMenu();
        Services.Events.Fire(new DisablePlayerMovementEvent(false));
    }

    #region Overview private void FindPlayer()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Finding the player if the player reference is null                                 				            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void FindPlayer()
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

    #region Overview private void FindRadialMenuSpawner()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Finding the RadialMenuSpawner if the RadialMenuSpawner reference is null                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void FindRadialMenuSpawner()
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

    #region Overview private void Update()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Running once per frame					                                                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void Update () 
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
                    menuSpawner.SpawnMenu(this, player,dialogueModeActive, topicSelected, false);
                    clip = Resources.Load("Audio/SFX/HARTO_SFX/OpenHARTO") as AudioClip;

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
