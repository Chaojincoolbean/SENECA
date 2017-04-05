using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;
using SenecaEvents;

public class HARTOTuningv3Script : MonoBehaviour {

	public const string SCROLLWHEEL = "Mouse ScrollWheel";		//	Name reference to the scroll wheel axis
	public const string ASTRID = "Astrid";
	public const string HARTO_CANVAS = "HARTOCanvas";

	public KeyCode toggleHARTO = KeyCode.Tab;
	public KeyCode toggleRecordMode = KeyCode.BackQuote;
	public bool canUseHARTO;
	public bool isHARTOActive;
	public bool topicSelected;
	public bool recordingModeActive;
	public bool recordingFolderSelected;
	public float alphaChannelHARTO;
	public float deltaAlpha = 2.0f;							//	How much we increment/decrement the alpha channel of HARTO GameObject

	public float rotationSpeed = 20.0f;
	public float selectionAreaWidth;
	public Image uiHARTO;
	public GameObject topicWheel;
	public GameObject emotionWheel;
	public GameObject recordingsWheel;
	public GameObject myRecordings;
	
	public bool inConversation;
	public Canvas canvas;
	public Emotions currentEmotion;
	public Icon currentTopic;
	public Icon[] emotionWheelIcons;
	public Icon[] topicWheelIcons;
	public Icon[] recordingsWheelIcons;
	public Icon[] myRecordingsWheelIcons;
	public EmotionIcon[] emotionIcons;
	public Image selectionArea;
	public Image displayImage;
	public Image displayIcon;
	float rotateHARTO;

	public string recordingFolder;

	[SerializeField]
//	private Player player;
	private BeginDialogueEvent.Handler onBeginDialogueEvent;
	private EndDialogueEvent.Handler onEndDialogueEvent;

	private delegate void RotateUI(float rotation);
	private delegate void SelectIcon();

	RotateUI CurrentUI;
	SelectIcon Select;

	private RecordingModeToggledEvent.Handler onRecordingModeToggled;
	public Color transparent = new Color(1.0f, 1.0f, 1.0f);
	public Color selectionAreaColor;
	// Use this for initialization
	void Start () 
	{
		inConversation = false;

		canUseHARTO = true;
		isHARTOActive = false;
		alphaChannelHARTO = 0;

		canvas = GameObject.Find(HARTO_CANVAS).GetComponent<Canvas>();
		topicSelected = false;
		selectionArea = GameObject.Find("SelectionArea").GetComponent<Image>();
		selectionAreaWidth = selectionArea.sprite.bounds.extents.x * 1.5f;
		selectionAreaColor = selectionArea.color;

		displayImage = GameObject.Find("DisplayImage").GetComponent<Image>();
		displayIcon = GameObject.Find("DisplayIcon").GetComponent<Image>();

		uiHARTO = GameObject.Find("HARTOUI").GetComponent<Image>();

		topicWheel = GameObject.Find("TopicWheelUI");
		topicWheelIcons = topicWheel.GetComponentsInChildren<Icon>();

		emotionWheel = GameObject.Find("EmotionWheelUI");
		emotionWheelIcons = emotionWheel.GetComponentsInChildren<Icon>();
		emotionIcons = emotionWheel.GetComponentsInChildren<EmotionIcon>();

		recordingsWheel = GameObject.Find("RecordingsWheelUI");
		recordingsWheelIcons = recordingsWheel.GetComponentsInChildren<Icon>();

		myRecordings = GameObject.Find("MyRecordingsWheel");
		myRecordingsWheelIcons = myRecordings.GetComponentsInChildren<Icon>();

		onBeginDialogueEvent = new BeginDialogueEvent.Handler(OnBeginDialogueEvent);
		GameEventsManager.Instance.Register<BeginDialogueEvent>(onBeginDialogueEvent);

		onEndDialogueEvent = new EndDialogueEvent.Handler(OnEndDialogueEvent);
		GameEventsManager.Instance.Register<EndDialogueEvent>(onEndDialogueEvent);

		onRecordingModeToggled = new RecordingModeToggledEvent.Handler(OnRecordingModeToggled);
		GameEventsManager.Instance.Register<RecordingModeToggledEvent>(onRecordingModeToggled);

		//player = GameObject.Find(ASTRID).GetComponent<Player>();

		CurrentUI = RotateRecordingsWheel;
		Select = SelectRecordingIcon;
	}


	private void OnBeginDialogueEvent(GameEvent e)
	{
		//inConversation = true;
	}

	private void OnEndDialogueEvent(GameEvent e)
	{
		//inConversation = false;
	}

	private void OnRecordingModeToggled(GameEvent e)
	{
		// recordingModeActive = !recordingModeActive;
		// //	Some UI change here
		// if (recordingModeActive)
		// {
		// 	CurrentUI = RotateRecordingsWheel;
		// 	Select = SelectRecordingIcon;
		// }
		// else
		// {
		// 	CurrentUI = RotateDialogueWheel;
		// 	Select = SelectDialougeIcon;
		// }
	}

	void FadeHARTO(float alpha)
	{

		uiHARTO.color = new Color (uiHARTO.color.r, uiHARTO.color.g, uiHARTO.color.b, alpha);

		for (int i = 0; i < emotionWheelIcons.Length; i++)
		{
			if (!emotionWheelIcons[i].selected)
			{
				emotionWheelIcons[i].myIcon.color =  new Color(emotionWheelIcons[i].myIcon.color.r,
																emotionWheelIcons[i].myIcon.color.g,
																emotionWheelIcons[i].myIcon.color.b,
																alpha * Icon.alphaLimit);
			}

			if (!topicSelected || recordingModeActive)
			{
				emotionWheelIcons[i].myIcon.color =  new Color(emotionWheelIcons[i].myIcon.color.r,
																emotionWheelIcons[i].myIcon.color.g,
																emotionWheelIcons[i].myIcon.color.b,
																0 * Icon.alphaLimit);
			}
		}

		for (int i = 0; i < topicWheelIcons.Length; i++)
		{
	
			topicWheelIcons[i].myIcon.color = new Color(topicWheelIcons[i].myIcon.color.r,
													 		topicWheelIcons[i].myIcon.color.g,
															topicWheelIcons[i].myIcon.color.b,
															alpha * Icon.alphaLimit);
			if (topicSelected || recordingModeActive)
			{
				topicWheelIcons[i].myIcon.color = new Color(topicWheelIcons[i].myIcon.color.r,
													 		topicWheelIcons[i].myIcon.color.g,
															topicWheelIcons[i].myIcon.color.b,
															alpha * 0);
			}


		}

		for (int i = 0; i < myRecordingsWheelIcons.Length; i++)
		{
			if (!myRecordingsWheelIcons[i].selected)
			{
				myRecordingsWheelIcons[i].myIcon.color =  new Color(myRecordingsWheelIcons[i].myIcon.color.r,
																myRecordingsWheelIcons[i].myIcon.color.g,
																myRecordingsWheelIcons[i].myIcon.color.b,
																alpha * Icon.alphaLimit);
			}

			if (!recordingFolderSelected || !recordingModeActive)
			{
				myRecordingsWheelIcons[i].myIcon.color =  new Color(myRecordingsWheelIcons[i].myIcon.color.r,
																myRecordingsWheelIcons[i].myIcon.color.g,
																myRecordingsWheelIcons[i].myIcon.color.b,
																0 * Icon.alphaLimit);
			}
		}

		for (int i = 0; i < recordingsWheelIcons.Length; i++)
		{
	
			recordingsWheelIcons[i].myIcon.color = new Color(recordingsWheelIcons[i].myIcon.color.r,
													 		recordingsWheelIcons[i].myIcon.color.g,
															recordingsWheelIcons[i].myIcon.color.b,
															alpha * Icon.alphaLimit);
			if (recordingFolderSelected || !recordingModeActive)
			{
				recordingsWheelIcons[i].myIcon.color = new Color(recordingsWheelIcons[i].myIcon.color.r,
													 		recordingsWheelIcons[i].myIcon.color.g,
															recordingsWheelIcons[i].myIcon.color.b,
															alpha * 0);
			}


		}

		selectionArea.color = new Color(selectionArea.color.r,selectionArea.color.g, selectionArea.color.b, alpha);

		displayImage.color = new Color(displayImage.color.r, displayImage.color.g, displayImage.color.b, alpha);

		displayIcon.color = new Color(displayIcon.color.r, displayIcon.color.g, displayIcon.color.b, alpha);

		
	}
	
	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	RotateLevel: Rotates the enitre level												*/
    /*		param: float z - the value taken from the scroll wheel input					*/
	/*																						*/
    /*--------------------------------------------------------------------------------------*/
	void RotateDialogueWheel(float z)
	{
		//	Where the rotation magic happens
		if (!topicSelected)
		{
			topicWheel.transform.rotation = Quaternion.Euler(topicWheel.transform.rotation.x, topicWheel.transform.rotation.y, z * rotationSpeed);
		}
		else
		{
			emotionWheel.transform.rotation = Quaternion.Euler(emotionWheel.transform.rotation.x, emotionWheel.transform.rotation.y, z * rotationSpeed);
		}
	}

	void RotateRecordingsWheel(float z)
	{
		//	Where the rotation magic happens
		if (!recordingFolderSelected)
		{
			recordingsWheel.transform.rotation = Quaternion.Euler(recordingsWheel.transform.rotation.x, recordingsWheel.transform.rotation.y, z * rotationSpeed);
		}
		else
		{
			myRecordings.transform.rotation = Quaternion.Euler(myRecordings.transform.rotation.x, myRecordings.transform.rotation.y, z * rotationSpeed);
		}
	}

	void SelectDialougeIcon()
	{
		Icon[] myIconArray;
		if (topicSelected)
		{
			
			myIconArray = emotionIcons;
		}
		else
		{
			myIconArray = topicWheelIcons;
		}

		for (int i = 0; i < myIconArray.Length; i++)
		{		
			if (myIconArray[i].transform.position.x < selectionArea.transform.position.x + selectionAreaWidth &&
				myIconArray[i].transform.position.x > selectionArea.transform.position.x - selectionAreaWidth)
			{
				displayIcon.sprite = myIconArray[i].myIcon.sprite;
				//	Replace these if statements with Events and delegates!!!
				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					if (topicSelected)
					{
						currentEmotion = ((EmotionIcon)myIconArray[i]).emotion;
						myIconArray[i].selected = true;
						//GameEventsManager.Instance.Fire(new EmotionSelectedEvent(this));
					}
					else
					{
						currentTopic = myIconArray[i];
						myIconArray[i].selected = true;
						topicSelected = true;
						//GameEventsManager.Instance.Fire(new TopicSelectedEvent(this, player));
					}
				}
			}
			else
			{
				displayIcon.color = transparent;
				myIconArray[i].myIcon.color = Icon.inactiveColor;
				myIconArray[i].selected = false;
			}
		}
	}

	void SelectRecordingIcon()
	{
		Icon[] myIconArray;
		if (recordingFolderSelected)
		{
			
			myIconArray = myRecordingsWheelIcons;
		}
		else
		{
			myIconArray = recordingsWheelIcons;
		}

		for (int i = 0; i < myIconArray.Length; i++)
		{		
			if (myIconArray[i].transform.position.x < selectionArea.transform.position.x + selectionAreaWidth &&
				myIconArray[i].transform.position.x > selectionArea.transform.position.x - selectionAreaWidth)
			{
				displayIcon.sprite = myIconArray[i].myIcon.sprite;
				//	Replace these if statements with Events and delegates!!!
				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					if (recordingFolderSelected)
					{
						currentTopic = myIconArray[i];
						myIconArray[i].selected = true;
						//GameEventsManager.Instance.Fire(new RecordingFolderSelectedEvent(currentTopic.name));
						recordingFolder = currentTopic.name;
					}
					else
					{
						currentTopic = myIconArray[i];
						myIconArray[i].selected = true;
						recordingFolderSelected = true;
						//GameEventsManager.Instance.Fire(new RecordingSelectedEvent(recordingFolder, currentTopic.name));
					}
				}
			}
			else
			{
				displayIcon.color = transparent;
				myIconArray[i].myIcon.color = Icon.inactiveColor;
				myIconArray[i].selected = false;
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(toggleRecordMode))
		{
			//GameEventsManager.Instance.Fire(new RecordingModeToggledEvent());
		}

		if (canUseHARTO)
		{
			if (Input.GetKeyDown(toggleHARTO) && !inConversation)
			{
				isHARTOActive = !isHARTOActive;
				topicSelected = false;
				recordingFolderSelected = false;
				//GameEventsManager.Instance.Fire(new ToggleHARTOEvent());
			}

			if (isHARTOActive) 
			{
				Select();

				rotateHARTO = rotateHARTO +  Input.GetAxis (SCROLLWHEEL) * Time.deltaTime;
				CurrentUI(rotateHARTO);

				alphaChannelHARTO += deltaAlpha * Time.deltaTime;
				if (alphaChannelHARTO > 1.0f) 
				{
					alphaChannelHARTO = 1.0f;
				}
			}	 
			else 
			{
				alphaChannelHARTO -= deltaAlpha * Time.deltaTime;
				if (alphaChannelHARTO < 0.0f) 
				{
					alphaChannelHARTO = 0.0f;
				}
			}
			
			FadeHARTO (alphaChannelHARTO);
		}
		else
		{
			FadeHARTO (0);
		}
	}
}
