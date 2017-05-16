using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ChrsUtils.ChrsEventSystem.GameEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;

	public Dictionary<string, bool> whoTalksFirst;

    public KeyCode RestartGame = KeyCode.Backspace;
	public string sceneName;
	public HARTO astridHARTO;
	public DialogueManager dialogueManager;
	public HARTO_UI_Interface HARTOInterface;
	public RecordingManager recordingManager;
	public AudioSource audioSource;
	public Player player_Astrid;
	public GameObject npc_Priya;
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
	public bool isTestScene;
    public bool tutorialIsDone;
    public bool playerAnimationLock;
	public bool inUtan;
	public bool hasPriyaSpoken;
	public bool begin;
	public bool inConversation;
	public bool tabUIOnScreen;
	public bool waitingForInput;
	public bool completedOneTopic;
    public bool endGame;

	public bool startedGame;
	public float nextTimeToSearch = 0;				//	How long unitl the camera searches for the target again


	// Use this for initialization
	void Start () 
	{
		
		startedGame = false;
        playerAnimationLock = false;
        tutorialIsDone = false;
		inConversation = false;
		hasPriyaSpoken = false;
		completedOneTopic = false;
		CurrentSceneNumber = 1;
        endGame = false;

		if (instance == null)
		{
			instance = this;
			//DontDestroyOnLoad(this.gameObject);
		}
			
		whoTalksFirst = new Dictionary<string, bool>();
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
//		if (!isTestScene) 
//		{
			sceneName = GameObject.Find ("Root").transform.GetChild (0).tag;
		//}

		//onToggleHARTO = new ToggleHARTOEvent.Handler(OnToggleHARTO);

//		if (sceneName.Contains("Test"))
//		{
//			isTestScene = true;
//		}
//		else
//		{
//			isTestScene = false;
//		}

		if(!isTestScene)
		{
		}
		
		if (sceneName.Contains("Seneca_Campsite") && !startedGame)
		{
		}
		
	}


    void FindPlayer()
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

	// Update is called once per frame
	void Update () 
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

		if(player_Astrid == null)
		{
			FindPlayer();
			return;
		}
	}
}
