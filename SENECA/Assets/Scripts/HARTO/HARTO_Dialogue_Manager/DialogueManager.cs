using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;

#region DialogueManager Overview
/*
    This is a GameObject-Component based dialogue system where VO files are found based
    the navigation of the Hierarchy tab of Unity. Think of each GameObject as a folder
    that eventually leades to a file.

    Where to Find in Unity:
    In the Hierarchy Tab in Unity: SenecaSystem -> HARTO-> DialogueManager

    Dialogue Manager contains teh list of scenes and each scene there is an event.
    In the case of Seneca, an event is a conversation topic.

    The Dialogue Manager serves the the starting point for any 2 person conversation in the game 
    that is not a recording.
  
 */ 
#endregion 

public class DialogueManager : MonoBehaviour 
{
	public EventScript[] Events;                                    //  List of events. Populates automatically
	
	private int _scene;                                             //  Reference to the current SceneNumber
	public int SceneNumber
	{
		get {	return _scene;	}
		set {	_scene = value;	}
	}

	public const string SCENE = "SCENE_";                           //  Delimiter to ready input for File tree traversal
	public const string TOPIC_PREFIX = "Topic_";                    //  Delimiter to ready input for File tree traversal
    public const string EVENT_PREFIX = "Event_";                    //  Delimiter to ready input for File tree traversal

    public const string EVENT_START_GAME = "Event_Start_Game";      //  Hard coded key for the first dialogue exchange
	public const string EVENT_MEETING_TUTORIAL = "Event_Tutorial";  //  Hard coded key for the dialogue exchanges before topics can be choosen
	public const string EVENT_EXIT = "Event_Exit";                  //  Hard coded key for the dialogue exchange before the player can explore
    public const string EVENT_RUTH = "Event_Ruth";

	public const string PRIYA = "Priya";                            //  Hard coded key for Priya
    public const string RUTH = "Ruth";
    public const string BEORN = "Beorn";

    private EndGameEvent.Handler onEndGame;
	private BeginGameEvent.Handler onBeginGame;                                             //  A delegate connected to the Event System.
	private BeginTutorialEvent.Handler onBeginTutorial;                                     //  A delegate connected to the Event System.
    private TopicSelectedEvent.Handler onTopicSelected;                                     //  A delegate connected to the Event System.
    private ClosingHARTOForTheFirstTimeEvent.Handler onClosingHARTOForTheFirstTime;         //  A delegate connected to the Event System.


    // Use this for initialization
    void Start () 
	{
		SceneNumber = 1;

        //  Sets up delegates for events
        onEndGame = new EndGameEvent.Handler(OnEndGame);
		onBeginGame = new BeginGameEvent.Handler(OnBeginGame);
		onBeginTutorial = new BeginTutorialEvent.Handler(OnBeginTutorial);
		onTopicSelected = new TopicSelectedEvent.Handler(OnTopicSelected);
		onClosingHARTOForTheFirstTime = new ClosingHARTOForTheFirstTimeEvent.Handler(OnClosingHARTOForTheFirstTime);

        #region Quick Overview of Event System
        /*
                Event System is found in: ChrsUtils -> Events=Manager

                Overview:
                If we want a script to respond to a thing that happens in another script without coupling the scripts together,
                We use the Events Manager.

                How it works:
                We register a delegate to a GameEvent  (GameEvents are found in SenecaEvent.cs). When we fire that event using the
                Event Manager, the delegate is executed by the computer
                
         */
        #endregion

        //  Registers delegates for events
        Services.Events.Register<EndGameEvent>(onEndGame);
        Services.Events.Register<BeginGameEvent>(onBeginGame);
		Services.Events.Register<BeginTutorialEvent>(onBeginTutorial);
		Services.Events.Register<TopicSelectedEvent>(onTopicSelected);
		Services.Events.Register<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
	}

    private void OnDestroy()
    {
        Services.Events.Unregister<EndGameEvent>(onEndGame);
        Services.Events.Unregister<BeginGameEvent>(onBeginGame);
        Services.Events.Unregister<BeginTutorialEvent>(onBeginTutorial);
        Services.Events.Unregister<TopicSelectedEvent>(onTopicSelected);
        Services.Events.Unregister<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
    }

    void OnEndGame(GameEvent e)
    {
        SceneNumber = 2;
        InitDialogueEvent(EVENT_RUTH, SceneNumber, RUTH, GameManager.instance.whoTalksFirst["Event_Ruth2Ruth"]);
    }

    #region OnBeginGame Overview
    /*
            This function is called when the BeginGame event is fired.

            BeginGame event is fired in Mom.cs in void OnTriggerEnter2D(Collider2D col)
     */
    #endregion
    void OnBeginGame(GameEvent e)
	{
		InitDialogueEvent(EVENT_START_GAME, SceneNumber, PRIYA, GameManager.instance.whoTalksFirst["Event_Start_Game1Priya"]);
	}

    #region OnBeginTutorial Overview
    /*
            This function is called when the BeginTutorial event is fired.

            BeginTutorial event is fired in Mom.cs in void OnToggleHARTO(GameEvent e)
     */
    #endregion
    void OnBeginTutorial(GameEvent e)
	{
		InitDialogueEvent(EVENT_MEETING_TUTORIAL, SceneNumber, PRIYA, GameManager.instance.whoTalksFirst["Event_Tutorial1Priya"]);
	}

    #region OnClosingHARTOForTheFirstTime Overview
    /*
            This function is called when the OnClosingHARTOForTheFirstTime event is fired.

            OnClosingHARTOForTheFirstTime event is fired in HARTO_UI_Interface.cs in void Update()
     */
    #endregion
    void OnClosingHARTOForTheFirstTime(GameEvent e)
	{
        Debug.Log("dialoguemanager");
		InitDialogueEvent(EVENT_EXIT, SceneNumber, PRIYA, GameManager.instance.whoTalksFirst["Event_Exit1Priya"]);
	}

    #region OnTopicSelected Overview
    /*
            This function is called when the OnTopicSelected event is fired.

            OnTopicSelected event is fired in RadialMenu.cs in void DetermineEvent(RadialIcon icon)
     */
    #endregion
    void OnTopicSelected(GameEvent e)
	{
		string selectedEvent = EVENT_PREFIX + ((TopicSelectedEvent)e).topicName.Replace(TOPIC_PREFIX, "");
		bool astridTalksFirst =  GameManager.instance.whoTalksFirst[selectedEvent+SceneNumber +((TopicSelectedEvent)e).npcName];
		InitDialogueEvent(selectedEvent, SceneNumber,((TopicSelectedEvent)e).npcName, astridTalksFirst);		
	}

    #region InitDialogueEvent Overview
    /*
            Initiates the dialogue based on the topic selected, scenenumber, NPC, and who should talk first
     */
    #endregion
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
