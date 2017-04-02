using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseScript : MonoBehaviour {

	[Range(0.0f, 1.0f)]
	public float volume = 1.0f;
	
	public float elapsedHARTOSeconds;
	public float elapsedGibberishSeconds;
	public VoiceOverLine myLine;

	public AudioSource characterAudioSource;
	public AudioSource gibberishAudioSource;
	public string characterName;

	protected const string HARTO = "HARTO";
	protected const string GIBBERISH = "Gibberish";
	private const string BROCA_PARTICLES = "BrocaParticles";

	// Use this for initialization
	protected void Start () 
	{
		characterAudioSource = transform.parent.GetComponent<AudioSource>();
		gibberishAudioSource = GameObject.Find(BROCA_PARTICLES).GetComponentInParent<AudioSource>();
		characterName = transform.parent.name;
		myLine = GetComponentInChildren<VoiceOverLine>();
	}

	virtual public void PlayLine(string dialogueType, string scene, string topic)
	{
		if (dialogueType == HARTO)
		{
			characterAudioSource.PlayOneShot(myLine.LoadAudioClip(characterName, scene, topic, transform.name), volume);
			elapsedHARTOSeconds = myLine.LoadAudioClip(characterName, scene, topic,transform.name).length;
		}
		else if (dialogueType == GIBBERISH)
		{
			gibberishAudioSource.PlayOneShot(myLine.LoadGibberishAudio(characterName, scene, topic, transform.name), volume);
			elapsedGibberishSeconds = myLine.LoadGibberishAudio(characterName, scene, topic, transform.name).length;
		}
	}

	virtual public void PlayLine(Emotions myEmotion)
	{
	}
	
}
