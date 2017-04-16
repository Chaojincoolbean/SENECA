using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

public class EventScript : MonoBehaviour 
{
	public bool canAcess;
	public bool waitingForEmotionalInput;

	public const string ASTRID = "Astrid";
	public const string VO = "VO";
	public const string ASTRID_TALKS_FIRST = "@";
	public const string NO_EMOTION_SELECTED = "None";
	public const string HARTO = "HARTO";
	public const string GIBBERISH = "Gibberish";
	public const string BROCA_PARTICLES = "BrocaParticles";
	public string scene;
	public string topicName;
	public string characterSearchKey;
	public int totalResponses;
	public int astridLines;
	public int npcLines;
	public int totalLines;
	public ResponseScript response;
	public GameObject thisResponse;
	public List<AudioSource> myCharacters;	
	public AudioController gibberishPlayer;
	public AudioSource[] thisEventsAudioSources;
	private HARTO astridHARTO;
	// Use this for initialization
	void Start () 
	{
		thisEventsAudioSources = GetComponentsInChildren<AudioSource>();
		for(int i = 0; i < thisEventsAudioSources.Length; i++)
		{
			myCharacters.Add(thisEventsAudioSources[i]);
		}

		astridHARTO = GameObject.FindGameObjectWithTag("HARTO").GetComponent<HARTO>();

		scene = transform.parent.name;
		topicName = transform.name.Replace("Event_", "");

		gibberishPlayer = GameObject.Find(BROCA_PARTICLES).GetComponent<AudioController>();
	
	}

	public void InitResponseScriptWith(string characterName, bool astridTalksFirst)
	{
		if(!topicName.Contains("Start_Game"))
		{
			GameEventsManager.Instance.Fire(new BeginDialogueEvent());
		}

		totalLines = 0;
		astridLines = 1;
		npcLines = 1;

		characterSearchKey = characterName;

		if (transform.FindChild(characterSearchKey))
		{
			for (int i = 0; i < myCharacters.Count; i++)
			{
				if (myCharacters[i].name  == characterSearchKey || myCharacters[i].name  == ASTRID)
				{
					totalResponses += myCharacters[i].transform.childCount;
				}
			}

			if (astridTalksFirst)
			{
				GameObject firstResponse = GameObject.Find("Astrid_VO_" + astridLines+ "_" + scene + "_" + topicName).gameObject;
				if (firstResponse.transform.childCount > 1)
				{
					response = firstResponse.GetComponent<EmotionalResponseScript>();
					waitingForEmotionalInput = true;
				}
				else
				{
					response = firstResponse.GetComponent<ResponseScript>();
				}
				astridLines++;
			}
			else
			{
				GameObject firstResponse = GameObject.Find(characterName + "_" + VO + "_" + npcLines + "_" + scene + "_" + topicName).gameObject;
				if (firstResponse.transform.childCount > 1)
				{
					Debug.Log("NO!");
					response = firstResponse.GetComponent<EmotionalResponseScript>();
					waitingForEmotionalInput = true;
				}
				else
				{
					response = firstResponse.GetComponent<ResponseScript>();
				}
				
				npcLines++;
			}
			StartCoroutine(PlayEventDialogue(characterName));
		}
	}

	public IEnumerator PlayEventDialogue(string characterName)
	{
		while(totalLines < totalResponses)
		{
			totalLines++;

			//	Redundant check (the first time)
			if (response.transform.childCount > 1)
			{
				//	If I am waiting for emotional input, I keep waiting unitl i get it.
				waitingForEmotionalInput = true;
			}

			
			//	Checks if response needs to wiat for emotional input
			while(astridHARTO.CurrentEmotion.ToString() == NO_EMOTION_SELECTED && response.transform.childCount > 1)
			{
				GameManager.instance.waitingForInput = waitingForEmotionalInput;
				yield return new WaitForFixedUpdate();
			}

			//gibberishPlayer.confirm = false;
			//	Redundant check
			if (response.transform.childCount > 1)
			{
				((EmotionalResponseScript)response).PlayEmotionLine(astridHARTO.CurrentEmotion, GIBBERISH, scene, topicName);
				yield return new WaitForSeconds(1.5f);
				((EmotionalResponseScript)response).PlayEmotionLine(astridHARTO.CurrentEmotion, HARTO, scene, topicName);
				waitingForEmotionalInput = false;
				GameManager.instance.waitingForInput = waitingForEmotionalInput;
			}
			else
			{	
				response.PlayLine(GIBBERISH, scene, topicName);
				yield return new WaitForSeconds(1.5f);
				response.PlayLine(HARTO, scene, topicName);
			}
			
			while(response.characterAudioSource.isPlaying)
			{
				if(!GameManager.instance.inUtan)
				{
					gibberishPlayer.GetComponent<AudioSource>().volume = 0.4f;
				}
				yield return new WaitForFixedUpdate();	
			}
			gibberishPlayer.GetComponent<AudioSource>().volume = 0.0f;
			//gibberishPlayer.confirm = true;	

			//	Checks who spoke last. If it was Astrid, play NPC dialouge.
			if (response.characterName == ASTRID)
			{
				try
				{
					thisResponse = GameObject.Find(characterName + "_" + VO + "_" + npcLines + "_" + scene + "_" + topicName).gameObject;
					if (thisResponse.transform.childCount > 1)
					{
						response = thisResponse.GetComponent<EmotionalResponseScript>();
					}
					else
					{
						response = thisResponse.GetComponent<ResponseScript>();
					}
					npcLines++;
					
				}
				catch (Exception e)
				{
					Debug.Log ("Could not find " + characterName + "_" + VO + "_" + npcLines + "_" + scene + "_" + topicName);
				}
				
			}
			else
			{
				try 
				{
					thisResponse = GameObject.Find("Astrid_VO_" + astridLines + "_" + scene + "_" + topicName).gameObject;

					if (thisResponse.transform.childCount > 1)
					{
						response = thisResponse.GetComponent<EmotionalResponseScript>();
						waitingForEmotionalInput = true;
					}
					else
					{
						response = thisResponse.GetComponent<ResponseScript>();
					}
					astridHARTO.CurrentEmotion = Emotions.None;
					astridLines++;
				}
				catch (Exception e)
				{
					Debug.Log ("Could not find " + "Astrid VO_" +  astridLines + "_" + scene + "_" + topicName);
				}
			}

			if( totalLines == totalResponses)
			{
				break;
			}
			
		}

		if(topicName != "Start_Game")
		{
			Debug.Log(topicName);
			GameEventsManager.Instance.Fire(new EndDialogueEvent(topicName));
		}
		else
		{
			if(!GameManager.instance.tabUIOnScreen)
			{
				GameEventsManager.Instance.Fire(new TABUIButtonAppearEvent());
			}
		}
		yield return null;
	}
}
