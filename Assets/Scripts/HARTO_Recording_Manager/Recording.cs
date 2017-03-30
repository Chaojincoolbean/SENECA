using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recording : MonoBehaviour 
{
	public AudioClip audioRecording;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	public AudioClip LoadHARTORecording (string filename)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + filename) == null)
		{
			// Play empty audio here
			Debug.Log("Resource Not Found Error: " + "Audio/Recordings/" + filename + " not found!");
		}

		audioRecording = Resources.Load<AudioClip>("Audio/Recordings/" + filename);
		return audioRecording;
	}
}
