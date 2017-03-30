using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

public class RadialMenu : MonoBehaviour 
{
	public bool canSelect;
	public float rotationSpeed = 5.0f;
	public DisplayArea displayAreaPrefab;
	public RadialIcon radialIconPrefab;
	public RadialEmotionIcon radialEmotionIconPrefab;
	public RadialIcon selected;

	public Image selectionArea;
	public Sprite defaultDisplay;
	public Sprite emptyAreaSprite;

	public List<RadialIcon> iconList;
	private float rotateSelectionWheel;
	public Player _player;
	private const string SCROLLWHEEL = "Mouse ScrollWheel";
	private const string TOPIC_TAG = "Topic_";
	private const string EMOTION_TAG = "Emotion_";
	private const string AFFIRM_TAG = "Affirm_";
	private const string FOLDER_TAG = "Folder_";
	private const string RECORDING_TAG = "Recording_";

	public void Init()
	{
		canSelect = true;
		_player = GameObject.Find("Astrid").GetComponent<Player>();
		emptyAreaSprite = selectionArea.sprite;
	}

	public void SpawnIcons (Interactable obj, bool topicSelected) 
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
			float xPos = Mathf.Sin(theta);
			float yPos = Mathf.Cos(theta);
			newRadialIcon.transform.localPosition = new Vector3(xPos, yPos, 0.0f) * 180.0f;
			newRadialIcon.icon.color = obj.options[i].color;
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
			float xPos = Mathf.Sin(theta + scrollWheel);
			float yPos = Mathf.Cos(theta + scrollWheel);
			iconList[i].transform.localPosition = new Vector3(xPos, yPos) * 180.0f;
		}
	}

	void SelectIcon()
	{
		for (int i = 0; i < iconList.Count; i++)
		{	
			if(Vector3.Distance(iconList[i].icon.rectTransform.position, selectionArea.rectTransform.position) < 10)
			{
				displayAreaPrefab.displayIcon.sprite = iconList[i].icon.sprite;
				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					Debug.Log(iconList[i].title);
					DetermineEvent(iconList[i]);
				}	
			}
		}
	}

	void DetermineEvent(RadialIcon icon)
	{
		if(icon.title.Contains(TOPIC_TAG))
		{
			GameEventsManager.Instance.Fire(new TopicSelectedEvent(icon.title, _player));
		}
		else if (icon.title.Contains(EMOTION_TAG))
		{
			
			GameEventsManager.Instance.Fire(new EmotionSelectedEvent(((RadialEmotionIcon)icon).emotion));
		}
		else if (icon.title.Contains(FOLDER_TAG))
		{
			GameEventsManager.Instance.Fire(new RecordingFolderSelectedEvent(icon.title));
		}
		else if (icon.title.Contains(RECORDING_TAG))
		{
			GameEventsManager.Instance.Fire(new RecordingSelectedEvent(icon.title));
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		rotateSelectionWheel = rotateSelectionWheel +  Input.GetAxis (SCROLLWHEEL) * rotationSpeed * Time.deltaTime;	
		
		RotateIconWheel(rotateSelectionWheel);
		if(selectionArea == null)
		{
			selectionArea = GameObject.Find("SelectionArea").GetComponent<Image>();
		}

		if(canSelect)
		{
			SelectIcon();
		}
	}
}
