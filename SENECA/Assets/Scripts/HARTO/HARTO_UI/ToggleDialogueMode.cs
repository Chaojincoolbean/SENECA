using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;

public class ToggleDialogueMode : MonoBehaviour 
{
	public HARTO_UI_Interface ui;
	public Button thisButton;
	public RectTransform dialoguePosition;
	public Vector3 dialogueRotation;
	public RectTransform recordingPosition;
	public Vector3 recordingRotation;

	private BeginDialogueEvent.Handler onBeginDialogueEvent;
	private EndDialogueEvent.Handler onEndDialogueEvent;
	// Use this for initialization
	void Start () 
	{
		ui = GameObject.Find("HARTO_UI_Interface").GetComponent<HARTO_UI_Interface>();
		thisButton = GetComponent<Button>();
		thisButton.onClick.AddListener(TaskOnClick);	
		thisButton.interactable = false;
		dialoguePosition = GetComponent<RectTransform>();
		dialogueRotation = transform.localRotation.eulerAngles;

		recordingPosition = GameObject.Find("RecordingSwitchPos").GetComponent<RectTransform>();
		recordingRotation = GameObject.Find("RecordingSwitchPos").transform.localRotation.eulerAngles;

		onBeginDialogueEvent = new BeginDialogueEvent.Handler(OnBeginDialogueEvent);
		onEndDialogueEvent = new EndDialogueEvent.Handler(OnEndDialogueEvent);

		GameEventsManager.Instance.Register<BeginDialogueEvent>(onBeginDialogueEvent);
		GameEventsManager.Instance.Register<EndDialogueEvent>(onEndDialogueEvent);
	}

	void OnDestroy()
	{
		Debug.Log("E!!!!!");
		GameEventsManager.Instance.Unregister<BeginDialogueEvent>(onBeginDialogueEvent);
		GameEventsManager.Instance.Unregister<EndDialogueEvent>(onEndDialogueEvent);
	}

	void OnBeginDialogueEvent(GameEvent e)
	{
		thisButton.interactable = false;
	}

	void OnEndDialogueEvent(GameEvent e)
	{
		thisButton.interactable = true;
	}

	void Update()
	{
		if(thisButton == null)
		{
			ui = GameObject.Find("HARTO_UI_Interface").GetComponent<HARTO_UI_Interface>();
			thisButton = GetComponent<Button>();
			thisButton.onClick.AddListener(TaskOnClick);

			onBeginDialogueEvent = new BeginDialogueEvent.Handler(OnBeginDialogueEvent);

			GameEventsManager.Instance.Register<BeginDialogueEvent>(onBeginDialogueEvent);
			
		}

		if(GameManager.instance.inConversation)
		{
			thisButton.interactable = false;
		}
		else if (GameManager.instance.completedOneTopic)
		{
			thisButton.interactable = true;
		}

		if(dialoguePosition == null || recordingPosition == null)
		{
			dialoguePosition = GetComponent<RectTransform>();
			dialogueRotation = transform.localRotation.eulerAngles;
			recordingPosition = GameObject.Find("RecordingSwitchPos").GetComponent<RectTransform>();
			recordingRotation = GameObject.Find("RecordingSwitchPos").transform.localRotation.eulerAngles;
		}
		
		if(ui.dialogueModeActive)
		{
			// Some kind of easing function between the two positons!
			GetComponent<RectTransform>().position = new Vector3(dialoguePosition.position.x, dialoguePosition.position.y, dialoguePosition.position.z);
			GetComponent<RectTransform>().rotation = Quaternion.Euler(dialoguePosition.rotation.x, dialoguePosition.rotation.y, dialoguePosition.rotation.z);
		}
		else
		{
			GetComponent<RectTransform>().position = new Vector3(recordingPosition.position.x, recordingPosition.position.y, recordingPosition.position.z);
			GetComponent<RectTransform>().rotation = Quaternion.Euler(recordingPosition.rotation.x, recordingPosition.rotation.y, recordingPosition.rotation.z - 20.0f);
		}
	}
	
	void TaskOnClick()
	{

		ui.ToggleDialogueMode();
		
	}

}
