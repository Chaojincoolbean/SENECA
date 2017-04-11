﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionalResponseScript : ResponseScript {

	private const string HARTO_REF = "HARTO";
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

	public void PlayEmotionLine(Emotions emotion, string dialogueType, string scene, string topic)
	{		
		for (int i  = 0; i < possibleLines.Length; i++)
		{
			if (possibleLines[i].name.Contains(emotion.ToString()))
			{	
				Debug.Log(emotion.ToString());
				if (dialogueType == HARTO)
				{
					characterAudioSource.PlayOneShot(possibleLines[i].LoadAudioClip(characterName, scene, topic, transform.name, emotion.ToString()), volume);
					elapsedHARTOSeconds = possibleLines[i].LoadAudioClip(characterName, scene, topic, transform.name, emotion.ToString()).length;
				}
				else if (dialogueType == GIBBERISH)
				{
					possibleLines[i].LoadGibberishAudio(characterName, scene, topic, transform.name, emotion.ToString());
					elapsedGibberishSeconds = possibleLines[i].LoadGibberishAudio(characterName, scene, topic, transform.name, emotion.ToString()).length;
				}
				
			}
		}	
	}
}
