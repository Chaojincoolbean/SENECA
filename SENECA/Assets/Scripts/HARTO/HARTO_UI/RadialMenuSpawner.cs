using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;
using ChrsUtils;
using UnityEngine.UI;


public class RadialMenuSpawner : MonoBehaviour 
{
    
	public bool closing;
	public AudioClip clip;
	public AudioSource audioSource;

	public GameObject uiMouse;
	public RadialMenu menuPrefab;

	public RadialMenu newMenu;
	public RadialMenu oldMenu;

	public EasingProperties easing;
	private static bool firstPass = true;
	private RectTransform spawnPosition;

    private void Start()
    {
        easing = ScriptableObject.CreateInstance("EasingProperties") as EasingProperties;

        spawnPosition = GameObject.Find("HARTO_UI_Location").GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.3f;
    }

	public void ReloadSpawnMenu(HARTO_UI_Interface obj, Player player, bool dialogueModeActive, bool topicSelected, bool delay)
	{

		spawnPosition = GameObject.Find("HARTO_UI_Location").GetComponent<RectTransform>();
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.3f;

		if (audioSource != null) {
			audioSource.Stop ();
		}
		clip = Resources.Load("Audio/SFX/HARTO_SFX/OpenHARTO") as AudioClip;

		if(!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(clip);
		}

		oldMenu = newMenu;
		newMenu._destroyed = true;
		newMenu = Instantiate(menuPrefab) as RadialMenu;
		newMenu.transform.SetParent(transform, false);
		newMenu.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		newMenu.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y, spawnPosition.position.z);
		//newMenu.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0);
		newMenu.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1);
		//StartCoroutine(Animate(true));
		newMenu.Init(player, this);
		newMenu.SpawnIcons(obj, topicSelected);

		if(firstPass)
		{

			//	toss this in a coroutine!!!!!!
			//GameEventsManager.Instance.Fire(new BeginTutorialEvent());
			Vector3 tabPosition = GameObject.Find("Mouse_Location").transform.localPosition;
			GameObject mouse = Instantiate(uiMouse, tabPosition, Quaternion.identity);

			mouse.transform.SetParent(GameObject.Find("HARTOCanvas").transform, false);
			firstPass = false;
		}
		// Courtine to fade in image here!
	}

	public void ReloadDestroyMenu(){
		//	Courtine to fade out image here
		if (oldMenu != null)
		{
			audioSource.Stop();

			clip = Resources.Load("Audio/SFX/HARTO_Close") as AudioClip;
			//Debug.Log (clip);

			if(!audioSource.isPlaying && !HARTO_UI_Interface.HARTOSystem.isHARTOActive && oldMenu._destroyed)
			{
				audioSource.PlayOneShot(clip);
			}

			closing = true;
			//newMenu._anim.SetBool("Inactive", true);
			oldMenu._anim.SetBool("Confirm",false);
			oldMenu._anim.SetBool ("Inactive", true);
			closing = false;
			//StartCoroutine(Animate(false));
			oldMenu._destroyed = true;
			Destroy(oldMenu.gameObject, 1f);
			newMenu._destroyed = false;
		}
	}

	public void SpawnMenu(HARTO_UI_Interface obj, Player player, bool dialogueModeActive, bool topicSelected, bool delay)
	{

		spawnPosition = GameObject.Find("HARTO_UI_Location").GetComponent<RectTransform>();
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.3f;

		if (audioSource != null) {
			audioSource.Stop ();
		}
		clip = Resources.Load("Audio/SFX/HARTO_SFX/OpenHARTO") as AudioClip;

		if(!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(clip);
		}

		//oldMenu = newMenu;

		newMenu = Instantiate(menuPrefab) as RadialMenu;
		newMenu.transform.SetParent(transform, false);
		newMenu.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		newMenu.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y, spawnPosition.position.z);
		//newMenu.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0);
		newMenu.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1);
		//StartCoroutine(Animate(true));
		newMenu.Init(player, this);
		newMenu.SpawnIcons(obj, topicSelected);

		if(firstPass)
		{

			//	toss this in a coroutine!!!!!!
			//GameEventsManager.Instance.Fire(new BeginTutorialEvent());
			Vector3 tabPosition = GameObject.Find("Mouse_Location").transform.localPosition;
			GameObject mouse = Instantiate(uiMouse, tabPosition, Quaternion.identity);

			mouse.transform.SetParent(GameObject.Find("HARTOCanvas").transform, false);
			firstPass = false;
		}
		// Courtine to fade in image here!
	}

	private IEnumerator Animate(bool fadeIn)
    {

        yield return StartCoroutine(Coroutines.DoOverEasedTime(1.0f, easing.MovementEasing, t =>
        {
			float alpha;
			if(fadeIn)
			{
				alpha = Mathf.Lerp(0, 1, t);
			}
			else
			{
				
				alpha = Mathf.Lerp(1, 0, t);
			}
            newMenu.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
			newMenu.selectionArea.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			newMenu.screenHARTO.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			for(int i = 0; i < newMenu.iconList.Count; i++)
			{
				if (!newMenu.iconList[i].alreadySelected)
				{
					newMenu.iconList[i].color.color = new Color(1.0f, 1.0f, 1.0f, alpha);
				}
				else
				{
					newMenu.iconList[i].color.color = new Color(0.5f, 0.5f, 0.5f, alpha);
				}
			}
			
        }));
		if(!fadeIn)
		{
			
			
		}
		
    }

	public void DestroyMenu()
	{
		//	Courtine to fade out image here
		if (newMenu != null)
		{
			audioSource.Stop();

			clip = Resources.Load("Audio/SFX/HARTO_Close") as AudioClip;
			//Debug.Log (clip);

			if(!audioSource.isPlaying && !HARTO_UI_Interface.HARTOSystem.isHARTOActive)
			{
				audioSource.PlayOneShot(clip);
			}

			closing = true;
			//newMenu._anim.SetBool("Inactive", true);
			newMenu._anim.SetBool("Confirm",false);
			newMenu._anim.SetBool ("Inactive", true);
			closing = false;
			//StartCoroutine(Animate(false));
			newMenu._destroyed = true;
			Destroy(newMenu.gameObject);
		}
	}

}
