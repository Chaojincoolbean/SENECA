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
	public static RadialMenuSpawner instance;

	public GameObject uiMouse;
	public RadialMenu menuPrefab;

	public RadialMenu newMenu;

	public EasingProperties easing;
	private static bool firstPass = true;
	private RectTransform spawnPosition;
	

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		easing = new EasingProperties();
		//Debug.Log(Resources.Load("Prefabs/HARTO/UI/HARTOMenu"));
		//menuPrefab = Resources.Load("Prefabs/HARTO/UI/HARTOMenu") as RadialMenu;

		spawnPosition = GameObject.Find("HARTO_UI_Location").GetComponent<RectTransform>();
		audioSource = GetComponent<AudioSource>();
	}

	public void SpawnMenu(HARTO_UI_Interface obj, Player player, bool dialogueModeActive, bool topicSelected)
	{
		audioSource.Stop();
		clip = Resources.Load("Audio/SFX/HARTO_SFX/OpenHARTO") as AudioClip;

		if(!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(clip);
		}
		if(newMenu == null)
		{
			newMenu = Instantiate(menuPrefab) as RadialMenu;
			newMenu.transform.SetParent(transform, false);
			newMenu.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			newMenu.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y, spawnPosition.position.z);
			newMenu.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0);
			StartCoroutine(Animate(true));
			newMenu.Init(player);
			newMenu.SpawnIcons(obj, topicSelected);
		}
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
			
			closing = true;
			newMenu._anim.SetBool("Inactive", true);
			yield return new WaitForSeconds(1.2f);
			Destroy(newMenu.gameObject);
			closing = false;
		}
		
    }

	public void DestroyMenu()
	{
		//	Courtine to fade out image here
		if (newMenu != null)
		{
			audioSource.Stop();
			clip = Resources.Load("Audio/SFX/HARTO_SFX/CloseHARTO") as AudioClip;

			if(!audioSource.isPlaying && !HARTO_UI_Interface.HARTOSystem.isHARTOOn)
			{
				audioSource.PlayOneShot(clip);
			}
			StartCoroutine(Animate(false));
			
		}
	}
}
