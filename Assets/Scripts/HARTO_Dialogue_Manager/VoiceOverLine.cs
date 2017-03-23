using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverLine : MonoBehaviour 
{
	public string voiceOverLine = "";
	public const string GIBBERISH = "Gibberish";
	public AudioClip voiceOverGibberish;
	public AudioClip voiceOverHARTO;

	// Use this for initialization
	void Start () 
	{
	
	}

	public AudioClip LoadGibberishAudio (string npcName, string dialogueType, string filename, string emotionalResponse)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType + "_" + emotionalResponse) == null)
		{
			Debug.Log("Resource Not Found Error: " + "Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType + "_" + emotionalResponse + " not found!");
		}

		voiceOverGibberish = Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType + "_" + emotionalResponse);
		return voiceOverGibberish;
	}

	public AudioClip LoadAudioClip(string npcName, string dialogueType, string filename, string emotionalResponse)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType + "_" + emotionalResponse) == null)
		{
			Debug.Log("Resource Not Found Error: " + "Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType + "_" + emotionalResponse + " not found!");
		}
		
		voiceOverHARTO = Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType + "_" + emotionalResponse);

		return voiceOverHARTO;
	}

	public AudioClip LoadGibberishAudio (string npcName, string dialogueType, string filename)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType) == null)
		{
			Debug.Log("Resource Not Found Error: " + "Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType + " not found!");
		}

		voiceOverGibberish = Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType);
		return voiceOverGibberish;
	}
	public AudioClip LoadAudioClip(string npcName, string dialogueType, string filename)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType) == null)
		{
			Debug.Log("Resource Not Found Error: " + "Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType + " not found!");
		}

		voiceOverHARTO = Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType);
		return voiceOverHARTO;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
