using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionalResponseScript : ResponseScript {

	private const string HARTO_REF = "HARTO";
	private const string DELIMITER = "Line_";
	private HARTO astridHARTO;

	public VoiceOverLine[] possibleLines;

	// Use this for initialization
	void Start () 
	{
		base.Start();	
		astridHARTO = GameObject.FindGameObjectWithTag(HARTO_REF).GetComponent<HARTO>();
		possibleLines = GetComponentsInChildren<VoiceOverLine>();
	}

	public Emotions GetEmotionalInput()
	{
		return astridHARTO.CurrentEmotion;
	}

	public void PlayEmotionLine(Emotions emotion, string dialogueType)
	{		
		for (int i  = 0; i < possibleLines.Length; i++)
		{
			if (possibleLines[i].name.Contains(emotion.ToString()))
			{	
				if (dialogueType == HARTO)
				{
					characterAudioSource.PlayOneShot(possibleLines[i].LoadAudioClip(characterName, dialogueType, transform.name, emotion.ToString()), volume);
					elapsedHARTOSeconds = possibleLines[i].LoadAudioClip(characterName, dialogueType, transform.name, emotion.ToString()).length;
				}
				else if (dialogueType == GIBBERISH)
				{
					gibberishAudioSource.PlayOneShot(possibleLines[i].LoadGibberishAudio(characterName, dialogueType, transform.name, emotion.ToString()), volume);
					elapsedGibberishSeconds = possibleLines[i].LoadGibberishAudio(characterName, dialogueType, transform.name, emotion.ToString()).length;
				}
				
			}
		}	
	}
}
