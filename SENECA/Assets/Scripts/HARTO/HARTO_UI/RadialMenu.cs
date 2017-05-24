using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SenecaEvents;

#region RadialMenu.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    RadialMenu.cs is responsible for rotating the icons and firing the events that rely on the HARTO UI               */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          public:                                                                                                     */
/*                  public void Start()                                                                                 */
/*                  public void SpawnIcons(HARTO_UI_Interface.Action[] actions)                                         */
/*                                                                                                                      */
/*          private:                                                                                                    */
/*                  private void RotateIconWheel(float scrollWheel)                                                     */
/*                  private void SelectIcon()                                                                           */
/*                  private void SelectOption(RadialIcon icon)                                                          */
/*                  private void ForceStart()                                                                           */
/*                  private void Update()                                                                               */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class RadialMenu : MonoBehaviour 
{
	public bool _destroyed;

    public bool iconSelected;
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

    #region Overview public void Init(Player player, RadialMenuSpawner thisMenu)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Initalizing variables. Runs once before making the menu                                                         */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          Player player: the player spawning the menu                                                                 */
    /*          RadialMenuSpawner thisMenu: reference to the menuspawner                                                    */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void Init(Player player, RadialMenuSpawner thisMenu)
	{
        //  Makes rotation speed faster oon Windows machines
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            rotationSpeed = 50.0f;
        }

        menuSpawner = thisMenu;
        iconSelected = false;
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

    #region Overview public void SpawnIcons(HARTO_UI_Interface obj, bool topicSelected)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Spawning the icons in a circle                                                                                  */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          HARTO_UI_Interface obj: the place the spawner receives info to populate the icon wheel                      */
    /*          bool topicSelected: grays out topic if it has already been selecetd                                         */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
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
            //  Positions the icons in a circle around the center of the gameobject
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

    #region Overview public void DisableSelection()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Disabling the selection of icons                                                                                */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void DisableSelection()
	{
		canSelect = false;
	}

    #region Overview public void EnableSelection()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Enabling the selection of icons                                                                                 */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void EnableSelection()
	{
		canSelect = true;
	}

    #region Overview private void SetEmotion(RadialEmotionIcon icon)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Setting the current emotion                                                                                     */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          RadialEmotionIcon icon: the selected icon                                                                   */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void SetEmotion(RadialEmotionIcon icon)
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

    #region Overview private void RotateIconWheel(float schrollWheel)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Rotation of icon wheel                                                                                      */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          float scrollWheel: Input axis from "Mouse ScrollWheel"                                                      */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void RotateIconWheel(float scrollWheel)
	{	
		for (int i = 0; i < iconList.Count; i++)
		{
			float theta = (2 * Mathf.PI / iconList.Count) * i;
			float xPos = Mathf.Sin(theta + scrollWheel) * 1.08f + 0.2f;
			float yPos = Mathf.Cos(theta + scrollWheel) * 1.08f;
			iconList[i].transform.localPosition = new Vector3(xPos, yPos) * 130.0f;
		}
	}

    #region Overview private void SelectIcon()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Selecting an icon                                                                                           */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void SelectIcon()
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
                        iconSelected = true;
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

                if (iconSelected && !audioSource.isPlaying)
                {
                    DetermineEvent(iconList[i]);
                }
            }
		}
	}

    #region Overview private void SetEmotion(RadialIcon icon)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*          Firing the appropriate event after selection                                                                */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          RadialEmotionIcon icon: the selected icon                                                                   */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void DetermineEvent(RadialIcon icon)
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
            icon.alreadySelected = true;
			Services.Events.Fire(new RecordingSelectedEvent(icon.title));
		}
	}

    #region Overview private void Update()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Running once per frame					                                                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void Update () 
	{
		if (!_destroyed)
        {
			if (HARTO_UI_Interface.HARTOSystem.usingBeornsHARTO)
            {
				_anim.SetBool ("Regular", false);
			}
            else
            {
				_anim.SetBool ("Regular", true);
			}
			_anim.SetBool ("usingBeornsHARTO", HARTO_UI_Interface.HARTOSystem.usingBeornsHARTO);

			if (GameManager.instance.isTestScene)
            {
				if (Input.GetKeyDown (KeyCode.Space))
                {
					notActive = !notActive;
				}
				if (notActive)
                {
					clip = Resources.Load ("Audio/SFX/HARTO_Inactive") as AudioClip;
					if (!audioSource.isPlaying)
                    {
						audioSource.PlayOneShot (clip);
					}
					_anim.SetBool ("Inactive", true);
				}
                else
                {
					clip = Resources.Load ("Audio/SFX/HARTO_Active") as AudioClip;
					if (!audioSource.isPlaying)
                    {
						audioSource.PlayOneShot (clip);
					}
					_anim.SetBool ("Inactive", false);
				}	
			}

			if (screenHARTO == null)
            {
				screenHARTO = GameObject.Find (HARTO_SCREEN).GetComponent<Image> ();
			}

			if (selectionArea == null)
            {
				selectionArea = GameObject.Find ("SelectionArea").GetComponent<Image> ();
			}

			if (GameManager.instance.waitingForInput || !GameManager.instance.inConversation && !menuSpawner.closing)
            {
				_anim.SetBool ("Inactive", false);
			}
            else
            {
				_anim.SetBool ("Confirm", false);
				_anim.SetBool ("Inactive", true);
			}

			if (Input.GetKeyDown (KeyCode.Mouse0) && !GameManager.instance.waitingForInput && GameManager.instance.inConversation)
            {
				clip = Resources.Load ("Audio/SFX/HARTO_Negative_Feedback") as AudioClip;
				if (!audioSource.isPlaying)
                {
					audioSource.PlayOneShot (clip);
				}
			}

			if (canSelect)
            {
				float rotate = rotateSelectionWheel + Input.GetAxis (SCROLLWHEEL) * rotationSpeed * Time.deltaTime;
				if (rotate != rotateSelectionWheel && GameObject.Find ("MOUSE_UI(Clone)"))
                {
					Destroy (GameObject.Find ("MOUSE_UI(Clone)"));
				}

				if (rotateSelectionWheel != rotate)
                {
					clip = Resources.Load ("Audio/SFX/HARTO_Scroll") as AudioClip;

					if (!audioSource.isPlaying)
                    {
						audioSource.PlayOneShot (clip, Mathf.Abs (Input.GetAxis (SCROLLWHEEL)));
					}
				}

				rotateSelectionWheel = rotate;
				RotateIconWheel (rotateSelectionWheel);

				SelectIcon ();
			}
		}
	}
}
