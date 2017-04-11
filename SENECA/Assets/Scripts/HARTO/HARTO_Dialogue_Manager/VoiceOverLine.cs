using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverLine : MonoBehaviour 
{
	public string voiceOverLine = "";
	public const string GIBBERISH = "Gibberish";
	public const string BROCA_PARTICLES = "BrocaParticles";
	public AudioClip voiceOverGibberish;
	public AudioClip voiceOverHARTO;
	public BufferShuffler gibberishGenerator;

	// Use this for initialization
	void Start () 
	{
		
		gibberishGenerator = GameObject.Find(BROCA_PARTICLES).GetComponent<BufferShuffler>();
	}

	public AudioClip LoadGibberishAudio (string characterName, string scene, string topic, string filename, string emotionalResponse)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse) == null)
		{
			// Play empty audio here
			Debug.Log("Resource Not Found Error4: " + "Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse + " not found!");
		}

		voiceOverGibberish = Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse);
		gibberishGenerator.ClipToShuffle = voiceOverGibberish;
		return gibberishGenerator.ClipToShuffle;
	}

	public AudioClip LoadAudioClip(string characterName, string scene, string topic, string filename, string emotionalResponse)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse) == null)
		{
			Debug.Log("Resource Not Found Error3: " + "Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse + " not found!");
		}
		
		voiceOverHARTO = Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse);

		return voiceOverHARTO;
	}

	public AudioClip LoadGibberishAudio (string characterName, string scene, string topic, string filename)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename) == null)
		{
			Debug.Log("Resource Not Found Error2: " + "Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + " not found!");
		}

		voiceOverGibberish = Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename);
		gibberishGenerator.ClipToShuffle = voiceOverGibberish;
		return gibberishGenerator.ClipToShuffle;
	}
	public AudioClip LoadAudioClip(string characterName, string scene, string topic, string filename)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename) == null)
		{
			Debug.Log("Resource Not Found Error1: " + "Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + " not found!");
		}

		voiceOverHARTO = Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename);
		return voiceOverHARTO;
	}
}
