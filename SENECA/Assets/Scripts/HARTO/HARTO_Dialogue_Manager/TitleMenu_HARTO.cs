using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ChrsUtils;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;
using SenecaEvents;

public class TitleMenu_HARTO : MonoBehaviour 
{
	public bool clipHasBeenPlayed;
	public bool canSelect;
	public float rotationSpeed = 5.0f;
	public DisplayArea displayAreaPrefab;
	public RadialIcon radialIconPrefab;

	public Image selectionArea;
	public Image screenHARTO;		
	public Sprite emptyAreaSprite;
	public AudioClip clip;
	public AudioSource audioSource;

	public List<RadialIcon> iconList;
	private float rotateSelectionWheel;
	private RectTransform _rectTransform;
	private const string SCROLLWHEEL = "Mouse ScrollWheel";
	private const string HARTO_SCREEN = "HARTO_Screen";
	public Animator _anim;

	public void Start()
	{
		_anim = GetComponent<Animator>();
		canSelect = true;
		emptyAreaSprite = selectionArea.sprite;
		clipHasBeenPlayed = false;
		audioSource = GetComponent<AudioSource>();
		screenHARTO = GameObject.Find(HARTO_SCREEN).GetComponent<Image>();
		_rectTransform = GetComponent<RectTransform>();
		SpawnIcons(HARTO_UI_Interface.HARTOSystem.titleMenu);
	}

	public void SpawnIcons (HARTO_UI_Interface.Action[] actions) 
	{
		RadialIcon newRadialIcon;
		for (int i = 0 ; i < actions.Length; i++)
		{
			newRadialIcon = Instantiate(radialIconPrefab) as RadialIcon; 
			iconList.Add(newRadialIcon);
			newRadialIcon.transform.SetParent(transform);
			float theta = (2 * Mathf.PI / actions.Length) * i;
			float xPos = Mathf.Sin(theta);
			float yPos = Mathf.Cos(theta);
			newRadialIcon.transform.localPosition = new Vector3(xPos, yPos, 0.0f) * 0.0000000000000010f;
			newRadialIcon.transform.localRotation = Quaternion.Euler(0, 0, 90.0f);
			newRadialIcon.icon.color = actions[i].color;
			newRadialIcon.alreadySelected = actions[i].alreadySelected;
			newRadialIcon.icon.sprite = actions[i].sprite;
			newRadialIcon.title = actions[i].title;
		}
	}

	void RotateIconWheel(float scrollWheel)
	{	
		for (int i = 0; i < iconList.Count; i++)
		{
			float theta = (2 * Mathf.PI / iconList.Count) * i;
			float xPos = Mathf.Sin(theta + scrollWheel) * 0.9f;
			float yPos = Mathf.Cos(theta + scrollWheel) * 1.1f;
			iconList[i].transform.localPosition = new Vector3(xPos, yPos) * 200.0f;
		}
	}

	void SelectIcon()
	{
		
		for (int i = 0; i < iconList.Count; i++)
		{	
			if(Vector3.Distance(iconList[i].icon.rectTransform.position, selectionArea.rectTransform.position) < 181.0f)
			{
				_anim.SetBool("IconInRange", true);
				if (displayAreaPrefab.displayIcon.sprite != iconList[i].icon.sprite)
				{
					clipHasBeenPlayed = false;
				}
				clip = Resources.Load("Audio/SFX/HARTO_SFX/SWEEPS_0015") as AudioClip;

				if(!audioSource.isPlaying && !clipHasBeenPlayed)
				{
					//audioSource.PlayOneShot(clip);
					clipHasBeenPlayed = true;
				}

				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					if(!iconList[i].alreadySelected)
					{
						clip = Resources.Load("Audio/SFX/HARTO_SFX/LV-HTIS Beeps Simple 03") as AudioClip;
						if(!audioSource.isPlaying)
						{
							audioSource.PlayOneShot(clip, 0.5f);
						}
						_anim.SetBool("Confirm", true);
						StartGame(iconList[i]);
						
					}
					else
					{
						clip = Resources.Load("Audio/SFX/HARTO_SFX/Tune AM Radio 04") as AudioClip;
						if(!audioSource.isPlaying)
						{
							audioSource.PlayOneShot(clip);
						}
					}
				}
						
			}
			else
			{
				_anim.SetBool("IconInRange", false);
			}
		}
	}

	void StartGame(RadialIcon icon)
	{
			if(icon.title == "StartGame")
			{
				GameEventsManager.Instance.Fire(new SceneChangeEvent("_Prologue"));
				SceneManager.LoadScene("_Prologue");
			}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (screenHARTO == null)
		{
			screenHARTO = GameObject.Find(HARTO_SCREEN).GetComponent<Image>();
		}

		if(selectionArea == null)
		{
			selectionArea = GameObject.Find("SelectionArea").GetComponent<Image>();
		}

		float rotate = rotateSelectionWheel +  Input.GetAxis (SCROLLWHEEL) * rotationSpeed * Time.deltaTime;
		if(rotateSelectionWheel != rotate)
		{
			clip = Resources.Load("Audio/SFX/HARTO_SFX/RotaryTelephone Dial 03") as AudioClip;

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
