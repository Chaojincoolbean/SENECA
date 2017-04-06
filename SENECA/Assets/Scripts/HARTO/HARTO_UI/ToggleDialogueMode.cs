using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDialogueMode : MonoBehaviour 
{
	public HARTO_UI_Interface ui;
	public Button thisButton;
	public RectTransform dialoguePosition;
	public Vector3 dialogueRotation;
	public RectTransform recordingPosition;
	public Vector3 recordingRotation;
	// Use this for initialization
	void Start () 
	{
		ui = GameObject.Find("HARTO_UI_Interface").GetComponent<HARTO_UI_Interface>();
		thisButton = GetComponent<Button>();
		thisButton.onClick.AddListener(TaskOnClick);	

		dialoguePosition = GetComponent<RectTransform>();
		dialogueRotation = transform.localRotation.eulerAngles;

		recordingPosition = GameObject.Find("RecordingSwitchPos").GetComponent<RectTransform>();
		recordingRotation = GameObject.Find("RecordingSwitchPos").transform.localRotation.eulerAngles;
	}

	void Update()
	{
		if(dialoguePosition == null || recordingPosition == null)
		{
			dialoguePosition = GetComponent<RectTransform>();
			recordingPosition = GameObject.Find("RecordingSwitchPos").GetComponent<RectTransform>();
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
