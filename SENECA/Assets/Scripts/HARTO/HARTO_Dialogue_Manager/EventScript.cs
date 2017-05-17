using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;

#region EventScript Overview
/*
    This script determines who talks using a Coroutine. 



    This scipt is a part of the Dialogue System and is only called by DialogueManger.cs
  
 */
#endregion
public class EventScript : MonoBehaviour 
{
	public bool waitingForEmotionalInput;                   //  Bool to check if we are waiting on emotional input

	public const string ASTRID = "Astrid";                  //  Reference to player
	public const string VO = "VO";                          //  
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
	public float nextTimeToSearch = 0;				//	How long unitl the camera searches for the target again

	public ResponseScript response;
	public GameObject thisResponse;
	public List<AudioSource> myCharacters;	
	public AudioController gibberishPlayer;
	public AudioSource[] thisEventsAudioSources;
	private HARTO astridHARTO;

	public GameObject Priya;
    public GameObject Ruth;

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

	void FindBrocaParticles()
	{
		if (nextTimeToSearch <= Time.time)
		{
			GameObject result = GameObject.FindGameObjectWithTag (BROCA_PARTICLES);
			if (result != null)
			{
				gibberishPlayer = result.GetComponent<AudioController>();
			}
				nextTimeToSearch = Time.time + 2.0f;
		}
	}

	void Update()
	{
        if(GameManager.instance.inUtan)
        {
           if(name.Contains("SCENE_1"))
            {
                gameObject.SetActive(false);
            }
        }

        if (gibberishPlayer == null)
		{
			FindBrocaParticles();
			return;
		}
	}

	public void InitResponseScriptWith(string characterName, bool astridTalksFirst)
	{
		if(!topicName.Contains("Start_Game"))
		{
			Services.Events.Fire(new BeginDialogueEvent());
		}

		totalLines = 0;
		astridLines = 0;
		npcLines = 0;
        
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
				astridLines++;
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
			}
			else
			{
				npcLines++;
				GameObject firstResponse = GameObject.Find(characterName + "_" + VO + "_" + npcLines + "_" + scene + "_" + topicName).gameObject;
				if (firstResponse.transform.childCount > 1)
				{
					response = firstResponse.GetComponent<EmotionalResponseScript>();
					waitingForEmotionalInput = true;
				}
				else
				{
					response = firstResponse.GetComponent<ResponseScript>();
				}
				
				
			}
			StartCoroutine(PlayEventDialogue(characterName));
		}
	}

	public IEnumerator PlayEventDialogue(string characterName)
	{
		while(totalLines < totalResponses)
		{
			totalLines++;

			if (response.transform.childCount > 1)
			{
				waitingForEmotionalInput = true;
			}


			while(astridHARTO.CurrentEmotion.ToString() == NO_EMOTION_SELECTED && response.transform.childCount > 1)
			{
                if (scene == "SCENE_1" && !GameManager.instance.playerAnimationLock)
                {
                    GameManager.instance.player_Astrid._animator.SetBool("HARTOActive", true);
                    GameManager.instance.player_Astrid._animator.SetBool("IsTalking", true);
                }
                GameManager.instance.waitingForInput = waitingForEmotionalInput;
				yield return new WaitForFixedUpdate();
			}

			if (response.transform.childCount > 1)
			{
				((EmotionalResponseScript)response).PlayEmotionLine(astridHARTO.CurrentEmotion, HARTO, scene, topicName);
				waitingForEmotionalInput = false;
				GameManager.instance.waitingForInput = waitingForEmotionalInput;
			}
			else
			{
				response.PlayLine(HARTO, scene, topicName);
			}

            
            while (response.characterAudioSource.isPlaying)
			{
                //	TODO: Talking animations

                //	if response == Astrid. set astrid talking anim to true
                //	else set base class fo npc talking anim to true
                
				if (response.characterName == "Astrid")
				{
                    if (scene == "SCENE_1" && !GameManager.instance.playerAnimationLock && topicName != "Start_Game")
                    {
                        GameManager.instance.player_Astrid._animator.SetBool("HARTOActive", true);
                        GameManager.instance.player_Astrid._animator.SetBool("IsTalking", true);
                        Priya = GameObject.FindGameObjectWithTag("Priya");
                        Priya.GetComponent<Animator>().SetBool("IsTalking", false);
                    }
                    else
                    {
                        GameManager.instance.player_Astrid._animator.SetBool("IsTalking", true);
                    }
                    
				} 
				else if (response.characterName == "Priya") 
				{
                    if (!GameManager.instance.playerAnimationLock)
                    {
                        GameManager.instance.player_Astrid._animator.SetBool("HARTOActive", false);
                        GameManager.instance.player_Astrid._animator.SetBool("IsTalking", false);
                    }
                    // other character istalking is true;
                    Priya = GameObject.FindGameObjectWithTag ("Priya");
					Priya.GetComponent<Animator>().SetBool("IsTalking", true);
                    Services.Events.Fire(new InteractableEvent(false, false, false));

                }
                else if (response.characterName == "Ruth")
                {
                    // other character istalking is true;
                   // Ruth = GameObject.FindGameObjectWithTag("Ruth");
                    //Ruth.GetComponent<Animator>().SetBool("IsTalking", true);

                }
                if (!GameManager.instance.inUtan)
				{
					gibberishPlayer.GetComponent<AudioSource>().volume = 0f;
				}
				yield return new WaitForFixedUpdate();	
			}

			//GameManager.instance.player_Astrid._animator.SetBool ("IsTalking", false);
			//other charcter's istalking is false
			gibberishPlayer.GetComponent<AudioSource>().volume = 0.0f;

			if (response.characterName == ASTRID)
			{
				try
				{
					npcLines++;
					thisResponse = GameObject.Find(characterName + "_" + VO + "_" + npcLines + "_" + scene + "_" + topicName).gameObject;
					if (thisResponse.transform.childCount > 1)
					{
						response = thisResponse.GetComponent<EmotionalResponseScript>();
					}
					else
					{
						response = thisResponse.GetComponent<ResponseScript>();
					}
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
					astridLines++;
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
					
				}
				catch (Exception e)
				{
				}
			}

			if( totalLines == totalResponses)
			{
				break;
			}
			
		}

		if(topicName != "Start_Game" && !scene.Contains("2"))
		{
			Services.Events.Fire(new EndDialogueEvent(topicName));
			if (topicName == "Exit")
			{
                GameManager.instance.tutorialIsDone = true;
				HARTO_UI_Interface.HARTOSystem.WaitForExitScript();
				Services.Events.Fire(new ToggleHARTOEvent());
			}
		}
        else if(scene.Contains("2") && topicName == "Ruth")
        {
            GameManager.instance.endGame = true;
        }
		else
		{
			if(!GameManager.instance.tabUIOnScreen)
			{
				//SenecaCampsiteSceneScript.MakeTabAppear ();
				Services.Events.Fire(new TABUIButtonAppearEvent());
			}
		}
		yield return null;
	}
}
