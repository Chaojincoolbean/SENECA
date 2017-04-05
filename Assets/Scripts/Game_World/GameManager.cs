using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

public class GameManager : MonoBehaviour {

/*
		When ever you click doubles of the audio is played.
		Why?
 */

	public static GameManager instance;

	public Dictionary<string, bool> whoTalksFirst;

	public HARTO astridHARTO;
	public DialogueManager dialogueManager;
	public HARTO_UI_Interface HARTOInterface;
	public RecordingManager recordingManager;
	public AudioSource audioSource;
	public Player player_Astrid;
	public GameObject npc_Priya;
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
	private const string ASTRID = "Player";
	public bool begin;
	// Use this for initialization
	void Start () 
	{
		CurrentSceneNumber = 1;
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		whoTalksFirst = new Dictionary<string, bool>();
		whoTalksFirst.Add("Event_Start_Game1Priya",true);
		whoTalksFirst.Add("Event_Tutorial1Priya", false);
		whoTalksFirst.Add("Event_Meeting1Priya", true);

		astridHARTO = GameObject.FindGameObjectWithTag(HARTO_TAG).GetComponent<HARTO>();
		dialogueManager = GameObject.FindGameObjectWithTag(DIALOUGE_MANAGER_TAG).GetComponent<DialogueManager>();
		recordingManager = GameObject.FindGameObjectWithTag(RECORDING_MANAGER_TAG).GetComponent<RecordingManager>();
		HARTOInterface = GameObject.FindGameObjectWithTag(HARTO_UI_INTERFACE_TAG).GetComponent<HARTO_UI_Interface>();

		audioSource = GetComponent<AudioSource>();

		player_Astrid = GameObject.FindGameObjectWithTag(ASTRID).GetComponent<Player>();

		npc_Priya = Instantiate(Resources.Load("Prefabs/Characters/Mom", typeof(GameObject))) as GameObject;

		npc_Priya.gameObject.transform.position = new Vector3 (-10f, -3.5f, 0);



		GameEventsManager.Instance.Fire(new DisablePlayerMovementEvent(true));

		audioSource.PlayOneShot(recordingManager.LoadHARTOVO("HARTO_VO1"));
		begin = false;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!begin && !audioSource.isPlaying)
		{
			GameEventsManager.Instance.Fire(new MoveMomEvent());
			begin = true;
		}

	}
}
