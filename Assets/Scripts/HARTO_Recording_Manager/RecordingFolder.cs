using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingFolder : MonoBehaviour 
{

	[Range(0.0f, 1.0f)]
	public float volume = 1.0f;
	public Recording[] myRecordings;
	public AudioSource recordingAudioSource;

	// Use this for initialization
	void Start () 
	{
			myRecordings = GetComponentsInChildren<Recording>();
			
	}

	public void PlayRecording(string filename)
	{
		for(int i = 0; i < myRecordings.Length; i++)
		{
			if(myRecordings[i].name == filename)
			{
				recordingAudioSource.PlayOneShot(myRecordings[i].LoadHARTORecording(filename), volume);
			}
		}
	}


}
