using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;

#region GameManager.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    This is a GameObject-Component based dialogue system where VO files are found based                               */
/*    the navigation of the Hierarchy tab of Unity. Think of each GameObject as a folder                                */
/*    that eventually leades to a file.                                                                                 */
/*                                                                                                                      */
/*   Where to Find in Unity:                                                                                            */
/*    In the Hierarchy Tab in Unity: SenecaSystem -> HARTO-> DialogueManager                                            */
/*                                                                                                                      */
/*    Dialogue Manager contains the list of scenes and each scene there is an event.                                    */
/*    In the case of Seneca, an event is a conversation topic.                                                          */
/*                                                                                                                      */
/*    The Dialogue Manager serves the the starting point for any 2 person conversation in the game                      */
/*    that is not a recording.                                                                                          */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*           private:                                                                                                   */
/*                 private void Start()                                                                                 */
/*                 private void OnDestroy()                                                                             */
/*                 private void OnEndGame(GameEvent e)                                                                  */
/*                 private void OnBeginGame(GameEvent e)                                                                */
/*                 private void OnBeginTutorial(GameEvent e)                                                            */
/*                 private void OnClosingHARTOForTheFirstTime(GameEvent e)                                              */
/*                 private void OnTopicSelected(GameEvent e)                                                            */
/*                 private void InitDialogueEvent(string topic, int sceneNumber, string npcName, bool astridTalksFirst) */
/*                                                                                                                      */
/************************************************************************************************************************/
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
    private void Start () 
	{
		SceneNumber = 1;

        //  Sets up delegates for events
        onEndGame = new EndGameEvent.Handler(OnEndGame);
		onBeginGame = new BeginGameEvent.Handler(OnBeginGame);
		onBeginTutorial = new BeginTutorialEvent.Handler(OnBeginTutorial);
		onTopicSelected = new TopicSelectedEvent.Handler(OnTopicSelected);
		onClosingHARTOForTheFirstTime = new ClosingHARTOForTheFirstTimeEvent.Handler(OnClosingHARTOForTheFirstTime);

        //  Registers delegates for events
        Services.Events.Register<EndGameEvent>(onEndGame);
        Services.Events.Register<BeginGameEvent>(onBeginGame);
		Services.Events.Register<BeginTutorialEvent>(onBeginTutorial);
		Services.Events.Register<TopicSelectedEvent>(onTopicSelected);
		Services.Events.Register<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
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
        Services.Events.Unregister<EndGameEvent>(onEndGame);
        Services.Events.Unregister<BeginGameEvent>(onBeginGame);
        Services.Events.Unregister<BeginTutorialEvent>(onBeginTutorial);
        Services.Events.Unregister<TopicSelectedEvent>(onTopicSelected);
        Services.Events.Unregister<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
    }

    #region Overview private void OnEndGame(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      This function is called when the EndGameEvent event is fired.                                                   */
    /*      EndGame event is fired in BackToSeneca.cs in void OnTriggerEnter2D(Collider2D col)                              */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnEndGame(GameEvent e)
    {
        SceneNumber = 2;
        InitDialogueEvent(EVENT_RUTH, SceneNumber, RUTH, GameManager.instance.whoTalksFirst["Event_Ruth2Ruth"]);
    }

    #region Overview private void OnBeginGame(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      This function is called when the BeginGame event is fired.                                                      */
    /*      BeginGame event is fired in Mom.cs in void OnTriggerEnter2D(Collider2D col)                                     */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnBeginGame(GameEvent e)
	{
		InitDialogueEvent(EVENT_START_GAME, SceneNumber, PRIYA, GameManager.instance.whoTalksFirst["Event_Start_Game1Priya"]);
    }

    #region Overview private void OnBeginTutorial(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*       This function is called when the BeginTutorial event is fired.                                                 */
    /*       BeginTutorial event is fired in Mom.cs in void OnToggleHARTO(GameEvent e)                                      */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnBeginTutorial(GameEvent e)
	{
		InitDialogueEvent(EVENT_MEETING_TUTORIAL, SceneNumber, PRIYA, GameManager.instance.whoTalksFirst["Event_Tutorial1Priya"]);
	}

    #region Overview private void OnClosingHARTOForTheFirstTim(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*       This function is called when the OnClosingHARTOForTheFirstTime event is fired.                                 */
    /*       OnClosingHARTOForTheFirstTime event is fired in HARTO_UI_Interface.cs in void Update()                         */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnClosingHARTOForTheFirstTime(GameEvent e)
	{
        Debug.Log("dialoguemanager");
		InitDialogueEvent(EVENT_EXIT, SceneNumber, PRIYA, GameManager.instance.whoTalksFirst["Event_Exit1Priya"]);
	}

    #region Overview private void OnTopicSelected(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*       This function is called when the OnTopicSelected event is fired.                                               */
    /*       OnTopicSelected event is fired in RadialMenu.cs in void DetermineEvent(RadialIcon icon)                        */
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
		string selectedEvent = EVENT_PREFIX + ((TopicSelectedEvent)e).topicName.Replace(TOPIC_PREFIX, "");
		bool astridTalksFirst =  GameManager.instance.whoTalksFirst[selectedEvent+SceneNumber +((TopicSelectedEvent)e).npcName];
		InitDialogueEvent(selectedEvent, SceneNumber,((TopicSelectedEvent)e).npcName, astridTalksFirst);		
	}

    #region Overview private void InitDialogueEvent(string topic, int sceneNumber ,string npcName, bool astridTalksFirst)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*       Initiates the dialogue based on the topic selected, scenenumber, NPC, and who should talk first                */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          string topic: The name of the topic                                                                         */
    /*          int sceneNumber: The current scene number                                                                   */
    /*          string npcName: Who Astrid is talking to                                                                    */
    /*          bool astridTalksFirst: Whether or not Astrid talks first                                                    */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void InitDialogueEvent(string topic, int sceneNumber ,string npcName, bool astridTalksFirst)
	{
		GameObject sceneFolder = GameObject.Find(SCENE + SceneNumber);
        
        if (sceneFolder != null)
		{
            
            EventScript thisEvent = sceneFolder.transform.FindChild(topic).GetComponent<EventScript>();
			if (thisEvent != null)
			{
                thisEvent.InitResponseScriptWith(npcName, astridTalksFirst);
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
