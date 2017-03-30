using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDialogueMode : MonoBehaviour 
{
	public Interactable ui;
	public Button thisButton;
	// Use this for initialization
	void Start () 
	{
		ui = GameObject.Find("HARTO_UI_Interface").GetComponent<Interactable>();
		thisButton = GetComponent<Button>();
		thisButton.onClick.AddListener(TaskOnClick);	
	}
	
	void TaskOnClick()
	{
		ui.ToggleDialogueMode();
	}

}
