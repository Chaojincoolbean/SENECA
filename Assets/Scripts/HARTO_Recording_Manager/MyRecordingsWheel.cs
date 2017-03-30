using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;

public class MyRecordingsWheel : MonoBehaviour 
{

	public Icon[] recordingIcons;
	private RecordingFolderSelectedEvent.Handler onRecordingFolderSelecetdEvent;


	// Use this for initialization
	void Start () 
	{
		recordingIcons = transform.GetComponentsInChildren<Icon>();
		onRecordingFolderSelecetdEvent = new RecordingFolderSelectedEvent.Handler(OnRecordingFolderSelecetdEvent);
		GameEventsManager.Instance.Register<RecordingFolderSelectedEvent>(onRecordingFolderSelecetdEvent);

		
	}

	void OnRecordingFolderSelecetdEvent(GameEvent e)
	{
		// based on the folder selected change the name of the recordings
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
