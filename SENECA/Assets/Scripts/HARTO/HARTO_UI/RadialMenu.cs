using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChrsUtils;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;
using SenecaEvents;

public class RadialMenu : MonoBehaviour 
{
	public bool activeSFXPlayedOnce;
	public bool notActive;
	public bool clipHasBeenPlayed;
	public bool canSelect;
	public float rotationSpeed = 5.0f;
	public DisplayArea displayAreaPrefab;
	public RadialIcon radialIconPrefab;
	public RadialEmotionIcon radialEmotionIconPrefab;
	public RadialIcon selected;
    public RadialMenuSpawner menuSpawner;


    public Image selectionArea;
	public Image screenHARTO;		
	public Sprite defaultDisplay;
	public Sprite emptyAreaSprite;
	public AudioClip clip;
	public AudioSource audioSource;

	public List<RadialIcon> iconList;
	private float rotateSelectionWheel;
	public Player _player;
	private const string SCROLLWHEEL = "Mouse ScrollWheel";
	private const string PLAYER_TAG = "Player";
	private const string TOPIC_TAG = "Topic_";
	private const string EMOTION_TAG = "Emotion_";
	private const string AFFIRM_TAG = "Affirm_";
	private const string FOLDER_TAG = "Folder_";
	private const string RECORDING_TAG = "Recording_";
	private const string HARTO_SCREEN = "HARTO_Screen";
	public Color inactiveColor = new Color (0.39f, 0.39f, 0.39f, 1.0f);
	public Animator _anim;

	public void Init(Player player, RadialMenuSpawner thisMenu)
	{
        menuSpawner = thisMenu;

        _anim = GetComponent<Animator>();
		canSelect = true;
		_player = player;
		displayAreaPrefab = GetComponentInChildren<DisplayArea>();
		clipHasBeenPlayed = false;
		audioSource = GetComponent<AudioSource>();
		screenHARTO = GameObject.Find(HARTO_SCREEN).GetComponent<Image>();
		if (!GameManager.instance.inConversation)
		{
			_anim.SetBool("Inactive", true);
		}
        audioSource.volume = 0.3f;
        activeSFXPlayedOnce = false;
		notActive = true;
	}

	public void SpawnIcons (HARTO_UI_Interface obj, bool topicSelected) 
	{
		RadialIcon newRadialIcon;
		for (int i = 0 ; i < obj.options.Length; i++)
		{
			if (topicSelected)
			{
				if (obj.options[i].title.Contains("Emotion_"))
				{
					newRadialIcon = Instantiate(radialEmotionIconPrefab) as RadialEmotionIcon;
					newRadialIcon.title = obj.options[i].title;
					SetEmotion((RadialEmotionIcon)newRadialIcon);
				}
				else
				{
					newRadialIcon = Instantiate(radialIconPrefab) as RadialIcon; 
				}
			}
			else
			{
				newRadialIcon = Instantiate(radialIconPrefab) as RadialIcon; 
			}
			iconList.Add(newRadialIcon);
			newRadialIcon.transform.SetParent(transform, false);
			float theta = (2 * Mathf.PI / obj.options.Length) * i;
			float xPos = Mathf.Sin(theta) * 1.08f + 0.2f;
			float yPos = Mathf.Cos(theta) * 1.08f;
			newRadialIcon.transform.localPosition = new Vector3(xPos, yPos, 0.0f) * 130.0f;
			newRadialIcon.icon.color = obj.options[i].color;
			newRadialIcon.alreadySelected = obj.options[i].alreadySelected;
			newRadialIcon.icon.sprite = obj.options[i].sprite;
			newRadialIcon.title = obj.options[i].title;
			newRadialIcon.myMenu = this;
		}
	}

	public void DisableSelection()
	{
		canSelect = false;
	}

	public void EnableSelection()
	{
		canSelect = true;
	}

	void SetEmotion(RadialEmotionIcon icon)
	{

		string iconEmotion = icon.title.Replace("Emotion_", "");
		try 
		{
			if (System.Enum.IsDefined(typeof(Emotions), icon.emotion))
			{
				icon.emotion = (Emotions)System.Enum.Parse(typeof(Emotions), iconEmotion, true);
			}
		}
		catch (Exception e)
		{
			Debug.Log ("Emotion " + transform.name + " not found. Has " + transform.name + " been added to the Enum in HARTO.cs? Error: " + e.Message);
		}
	}


	void RotateIconWheel(float scrollWheel)
	{	
		for (int i = 0; i < iconList.Count; i++)
		{
			float theta = (2 * Mathf.PI / iconList.Count) * i;
			float xPos = Mathf.Sin(theta + scrollWheel) * 1.08f + 0.2f;
			float yPos = Mathf.Cos(theta + scrollWheel) * 1.08f;
			iconList[i].transform.localPosition = new Vector3(xPos, yPos) * 130.0f;
		}
	}

	void SelectIcon()
	{
		for (int i = 0; i < iconList.Count; i++)
		{	
			if(Vector3.Distance(iconList[i].icon.rectTransform.localPosition, selectionArea.rectTransform.localPosition) < 115.3f)
			{	
				if (displayAreaPrefab.displayIcon.sprite != iconList[i].icon.sprite)
				{
					clipHasBeenPlayed = false;
				}

				clip = Resources.Load("Audio/SFX/HARTO_Icon_Passes_Into_Circle") as AudioClip;

				if(!audioSource.isPlaying && !clipHasBeenPlayed)
				{
					audioSource.Stop();
					audioSource.PlayOneShot(clip);
					clipHasBeenPlayed = true;
				}

				displayAreaPrefab.displayIcon.sprite = iconList[i].icon.sprite;

				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					if(!iconList[i].alreadySelected)
					{
						clip = Resources.Load("Audio/SFX/HARTO_Select") as AudioClip;
						if(!audioSource.isPlaying)
						{
							audioSource.PlayOneShot(clip);
						}
						_anim.SetBool("Confirm", true);
						DetermineEvent(iconList[i]);
						
					}
					else
					{
						clip = Resources.Load("Audio/SFX/HARTO_Negative_Feedback") as AudioClip;
						if(!audioSource.isPlaying)
						{
							audioSource.PlayOneShot(clip);
						}
					}
				}			
			}
		}
	}

	void DetermineEvent(RadialIcon icon)
	{
		if(icon.title.Contains(TOPIC_TAG))
		{
			Services.Events.Fire(new TopicSelectedEvent(icon.title, _player.npcAstridIsTalkingTo));
		}
		else if (icon.title.Contains(EMOTION_TAG))
		{
			Services.Events.Fire(new EmotionSelectedEvent(((RadialEmotionIcon)icon).emotion));
		}
		else if (icon.title.Contains(FOLDER_TAG))
		{
			Services.Events.Fire(new RecordingFolderSelectedEvent(icon.title));
		}
		else if (icon.title.Contains(RECORDING_TAG))
		{
			Services.Events.Fire(new RecordingSelectedEvent(icon.title));
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(GameManager.instance.isTestScene)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				notActive = !notActive;
			}
			if(notActive)
			{
				clip = Resources.Load("Audio/SFX/HARTO_Inactive") as AudioClip;
				if(!audioSource.isPlaying)
				{
					audioSource.PlayOneShot(clip);
				}
				_anim.SetBool("Inactive", true);
			}
			else
			{
				clip = Resources.Load("Audio/SFX/HARTO_Active") as AudioClip;
				if(!audioSource.isPlaying)
				{
					audioSource.PlayOneShot(clip);
				}
				_anim.SetBool("Inactive", false);
			}	
		}

		if (screenHARTO == null)
		{
			screenHARTO = GameObject.Find(HARTO_SCREEN).GetComponent<Image>();
		}

		if(selectionArea == null)
		{
			selectionArea = GameObject.Find("SelectionArea").GetComponent<Image>();
		}

		if (GameManager.instance.waitingForInput || !GameManager.instance.inConversation && !menuSpawner.closing)
		{
			_anim.SetBool("Inactive", false);
		}
		else
		{
			_anim.SetBool("Confirm", false);
			_anim.SetBool("Inactive", true);
		}

		if (Input.GetKeyDown(KeyCode.Mouse0) && !GameManager.instance.waitingForInput && GameManager.instance.inConversation)
		{
			clip = Resources.Load("Audio/SFX/HARTO_Negative_Feedback") as AudioClip;
			if(!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(clip);
			}
		}

		if(canSelect)
		{
			
			float rotate = rotateSelectionWheel +  Input.GetAxis (SCROLLWHEEL) * rotationSpeed * Time.deltaTime;
			if(rotate != rotateSelectionWheel && GameObject.Find("MOUSE_UI(Clone)"))
			{
				Destroy(GameObject.Find("MOUSE_UI(Clone)"));
			}

			if(rotateSelectionWheel != rotate)
			{
				clip = Resources.Load("Audio/SFX/HARTO_Scroll") as AudioClip;

				if(!audioSource.isPlaying)
				{
					audioSource.PlayOneShot(clip, Mathf.Abs(Input.GetAxis(SCROLLWHEEL)));
				}
			}

			rotateSelectionWheel = 	rotate;
			RotateIconWheel(rotateSelectionWheel);

			SelectIcon();
		}
	}
}
