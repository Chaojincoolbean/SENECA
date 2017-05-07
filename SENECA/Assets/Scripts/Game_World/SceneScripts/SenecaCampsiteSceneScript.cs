using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;
using SenecaEvents;

public class SenecaCampsiteSceneScript : Scene<TransitionData> 
{
	Player player;

	public bool startedGame;
	public bool hasPriyaSpoken;
	public bool begin;
	public bool inConversation;
	public static bool tabUIOnScreen;
	public bool waitingForInput;
	public bool completedOneTopic;
	public float nextTimeToSearch = 0;


	public static GameObject uiTAB;
	public GameObject uiMouse;
	public GameObject npc_Priya;

	public AudioSource audioSource;

	private TABUIButtonAppearEvent.Handler onTABUIButtionAppear;
	private ToggleHARTOEvent.Handler onToggleHARTO;

	void Start()
	{
		
	}

	internal override void OnEnter(TransitionData data)
	{
		Debug.Log ("Entered");
		player = GameManager.instance.player_Astrid;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 1.5f;

		startedGame = false;
		audioSource = GetComponent<AudioSource>();


		onTABUIButtionAppear = new TABUIButtonAppearEvent.Handler (OnTABUIButtonAppear);
		onToggleHARTO = new ToggleHARTOEvent.Handler (OnToggleHARTO);

		uiTAB = Resources.Load ("Prefabs/HARTO/UI/TAB_UI") as GameObject;
		uiMouse = Resources.Load ("Prefabs/HARTO/UI/MOUSE_UI") as GameObject;

		audioSource = GetComponent<AudioSource>();

		if (!TransitionData.Instance.SENECA_CAMPSITE.visitedScene) 
		{
			onTABUIButtionAppear = new TABUIButtonAppearEvent.Handler (OnTABUIButtonAppear);
			onToggleHARTO = new ToggleHARTOEvent.Handler (OnToggleHARTO);

			uiTAB = Resources.Load ("Prefabs/HARTO/UI/TAB_UI") as GameObject;
			uiMouse = Resources.Load ("Prefabs/HARTO/UI/MOUSE_UI") as GameObject;

			begin = true;
			npc_Priya = Instantiate(Resources.Load("Prefabs/Characters/Priya", typeof(GameObject))) as GameObject;
			npc_Priya.transform.parent = transform;
			npc_Priya.gameObject.transform.position = new Vector3 (-10f, -3.5f, 0);
			startedGame = true;
			audioSource.PlayOneShot(GameManager.instance.recordingManager.LoadHARTOVO("HARTO_VO1"));
			begin = false;

		} 
		else
		{
			player.transform.position = TransitionData.Instance.SENECA_CAMPSITE.position;
			player.transform.localScale = TransitionData.Instance.SENECA_CAMPSITE.scale;
		}
			
	}

	public static void MakeTabAppear()
	{
		if (!tabUIOnScreen)
		{

			tabUIOnScreen = true;
			Vector3 tabPosition = GameObject.Find("TAB_Button_Location").transform.localPosition;
			GameObject tab = Instantiate(uiTAB, tabPosition, Quaternion.identity);
			tab.transform.SetParent(GameObject.Find("HARTOCanvas").transform, false);
		}
	}

	void OnTABUIButtonAppear(GameEvent e)
	{
		Debug.Log("Count!!!");
		Debug.Log("Tab on Screen: " + tabUIOnScreen);
		if (!tabUIOnScreen)
		{

			tabUIOnScreen = true;
			Vector3 tabPosition = GameObject.Find("TAB_Button_Location").transform.localPosition;
			GameObject tab = Instantiate(uiTAB, tabPosition, Quaternion.identity);
			tab.transform.SetParent(GameObject.Find("HARTOCanvas").transform, false);
		}
	}

	void OnToggleHARTO(GameEvent e)
	{
		if(tabUIOnScreen)
		{
			Destroy(GameObject.Find("TAB_UI(Clone)"));
		}
	}

	void FindPlayer()
	{
		if (nextTimeToSearch <= Time.time)
		{
			GameObject result = GameObject.FindGameObjectWithTag ("Player");
			if (result != null)
			{
				player = result.GetComponent<Player>();
			}
			nextTimeToSearch = Time.time + 2.0f;
		}
	}

	void Update()
	{
		if(!begin && !audioSource.isPlaying && !TransitionData.Instance.SENECA_CAMPSITE.visitedScene)
		{
			GameEventsManager.Instance.Fire(new MoveMomEvent());
			begin = true;
		}
	}

	internal override void OnExit()
	{
		TransitionData.Instance.SENECA_CAMPSITE.position = player.transform.position;
		TransitionData.Instance.SENECA_CAMPSITE.scale = player.transform.localScale;
		TransitionData.Instance.SENECA_CAMPSITE.visitedScene = true;
	}
}
