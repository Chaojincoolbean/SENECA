using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#region GameManager.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    This script holds onto game logic that persists between scenes                                                    */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*           private:                                                                                                   */
/*                 private void Start()                                                                                 */
/*                 private void FindPlayer()                                                                            */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class GameManager : MonoBehaviour 
{
	public static GameManager instance;

    public KeyCode RestartGame = KeyCode.Backspace;     //  Restarts the game from any scene

    //  Some of the bools arre duplicated in SenecaCampsiteSceneScript
    //  The ones in SenecaCampsiteSceneScript actually control the game
    //  I didn't remove thse because not enough time was given to test
    //  would happend after I removed these bools

    public bool cheatSpace;                             //  Toggle this in Start() to toggle dialogue cheat
    public bool trackProgressInHARTO;                   //  Bool check for when Astrid should say "I'll track my progress in HARTO..."
    public bool HARTOinUtan;                            //  Bool check to see if HARTO is in Utan
    public bool pickedUpBeornsHARTO;                    //  Bool check to see if we already picked up Beorn's HARTO
    public bool HARTOIsTalking;                         //  Bool check to see if HARTO is talking right now
    public bool isTestScene;                            //  Bool check to see if this is a test scene
    public bool tutorialIsDone;                         //  Bool check to see if Tutorial script is done
    public bool playerAnimationLock;                    //  Bool check for whether there is an animation lock on Astrid
    public bool inUtan;                                 //  Bool check to see if we are in Utan
    public bool hasPriyaSpoken;                         //  Bool check to see if Priya has siad one line yet
    public bool begin;                                  //  Bool check to see if the game has begun (HARTO started talking)
    public bool inConversation;                         //  Bool check to see if we are in conversation
    public bool tabUIOnScreen;                          //  Bool check to see if the TAB UI icon is on screen
    public bool wasdUIOnScreen;                         //  Bool check to see if WASD UI is on screen
    public bool waitingForInput;                        //  Bool check that is true when waiting for input
    public bool completedOneTopic;                      //  Bool check to see if player completed one conversation topic
    public bool endGame;                                //  Has the end of the game started
    public bool startedGame;                            //  Game has started when Priya has spawned off screen
    public float nextTimeToSearch = 0;                  //	How long unitl the camera searches for the target again
    public string sceneName;                            //  Current scene's name
    public string currentScene;

    public Dictionary<string, bool> whoTalksFirst;      //  Who talks first in conversation
	
	public HARTO astridHARTO;
	public DialogueManager dialogueManager;
	public HARTO_UI_Interface HARTOInterface;
	public RecordingManager recordingManager;
	public AudioSource audioSource;
	public Player player_Astrid;
	public GameObject uiTAB;

	[SerializeField]
	private int _sceneNumber;
	public int CurrentSceneNumber
	{
		get {	return _sceneNumber;	}
		private set { _sceneNumber = value;	}
	}

	private const string RECORDING_MANAGER_TAG = "RecordingManager";
	private const string DIALOUGE_MANAGER_TAG = "DialogueManager";
	private const string HARTO_TAG = "HARTO";
	private const string HARTO_UI_INTERFACE_TAG = "HARTO_Interface";

    #region Overview public void Start()
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
        trackProgressInHARTO = false;
        HARTOinUtan = false;
        pickedUpBeornsHARTO = false;
        HARTOIsTalking = false;
		currentScene = "PrologueSceneScript";
		startedGame = false;
        playerAnimationLock = false;
        tutorialIsDone = false;
		inConversation = false;
		hasPriyaSpoken = false;
		completedOneTopic = false;
		CurrentSceneNumber = 1;
		wasdUIOnScreen = false;
        endGame = false;

		if (instance == null)
		{
			instance = this;
		}

        //toggle this to speed through dialogue
        cheatSpace = false;
			
		whoTalksFirst = new Dictionary<string, bool>();

        //  True = Astrid talks first || False = Other character talks first
        //  The key is the Event as named in the Unity Hierarchy, the scene number and the character you are talking to
		whoTalksFirst.Add("Event_Start_Game1Priya",true);
		whoTalksFirst.Add("Event_Tutorial1Priya", true);
		whoTalksFirst.Add("Event_Exit1Priya", true);
		whoTalksFirst.Add("Event_Meeting1Priya", true);
		whoTalksFirst.Add("Event_Broca1Priya", true);
		whoTalksFirst.Add("Event_Ruth1Priya", true);
        whoTalksFirst.Add("Event_Beorn2Beorn", true);
        whoTalksFirst.Add("Event_Ruth2Ruth", true);


        astridHARTO = GameObject.FindGameObjectWithTag(HARTO_TAG).GetComponent<HARTO>();
		dialogueManager = GameObject.FindGameObjectWithTag(DIALOUGE_MANAGER_TAG).GetComponent<DialogueManager>();
		recordingManager = GameObject.FindGameObjectWithTag(RECORDING_MANAGER_TAG).GetComponent<RecordingManager>();
		HARTOInterface = GameObject.FindGameObjectWithTag(HARTO_UI_INTERFACE_TAG).GetComponent<HARTO_UI_Interface>();

		audioSource = GetComponent<AudioSource>();

		sceneName = GameObject.Find ("Root").transform.GetChild (0).tag;
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
			GameObject result = GameObject.FindGameObjectWithTag ("Player");
			if (result != null)
			{
				player_Astrid = result.GetComponent<Player>();
			}
				nextTimeToSearch = Time.time + 2.0f;
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
        if(Input.GetKeyDown(RestartGame))
        {
            TransitionData.Instance = null;
            SceneManager.LoadScene("_Main");

        }
		sceneName = GameObject.Find ("Root").transform.GetChild (0).tag;

		if (sceneName.Contains("Test"))
		{
			isTestScene = true;
		}
		else
		{
			isTestScene = false;
		}

		if (sceneName.Contains("Utan"))
		{
			inUtan = true;
		}
		else
		{
			inUtan = false;
		}

        if(!sceneName.Contains("SenecaCampsite") && !sceneName.Contains("Title") && !sceneName.Contains("Prologue"))
        {
            hasPriyaSpoken = true;
        }

		if(player_Astrid == null)
		{
			FindPlayer();
			return;
		}
	}
}
