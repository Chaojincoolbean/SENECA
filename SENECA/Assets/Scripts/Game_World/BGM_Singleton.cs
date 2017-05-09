using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChrsUtils.ChrsEventSystem;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;
using SenecaEvents;

public class BGM_Singleton : MonoBehaviour 
{
	public static BGM_Singleton instance;
	public string sceneName;

	public float volume;
	public AudioSource audioSource;
	public AudioClip clip;
	private SceneChangeEvent.Handler onSceneChange;
	// Use this for initialization
	void Start () 
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			sceneName = GameManager.instance.sceneName;
			audioSource = GetComponent<AudioSource>();
			volume = 0.5f;


			if(sceneName.Contains("Utan"))
			{
				clip = Resources.Load("Audio/Music/Utan_Theme") as AudioClip;
			}
			else if(sceneName.Contains("Seneca"))
			{
				clip = Resources.Load("Audio/Music/Seneca_Theme") as AudioClip;
			}
			else if(sceneName.Contains("Title") || sceneName.Contains("Credits"))
			{
				clip = Resources.Load("Audio/Music/Title_Theme") as AudioClip;
			}

			onSceneChange = new SceneChangeEvent.Handler(OnSceneChange);
			Services.Events.Register<SceneChangeEvent>(onSceneChange);
			audioSource.PlayOneShot(clip, volume);
			audioSource.volume = volume;
		}
		else
		{
			Destroy(gameObject);
		}
	
		onSceneChange = new SceneChangeEvent.Handler(OnSceneChange);
		Services.Events.Register<SceneChangeEvent>(onSceneChange);
		audioSource.PlayOneShot(clip, volume);
		audioSource.volume = volume;
	}

	void OnSceneChange(GameEvent e)
	{
		string newScene = ((SceneChangeEvent)e).sceneName;
		
		if(!sceneName.Contains("Seneca") && newScene.Contains("Seneca"))
		{
			clip = Resources.Load("Audio/Music/Seneca_Theme") as AudioClip;
			audioSource.Stop();
			audioSource.PlayOneShot(clip, volume);
		}
		else if (!sceneName.Contains("Utan") && newScene.Contains("Utan"))
		{
			clip = Resources.Load("Audio/Music/Utan_theme") as AudioClip;
			audioSource.Stop();
			audioSource.PlayOneShot(clip, volume);
			
		}
		else if (sceneName.Contains("Credits") || sceneName.Contains("Title"))
		{
			clip = Resources.Load("Audio/Music/Title_Theme") as AudioClip;
			audioSource.Stop();
			audioSource.PlayOneShot(clip, volume);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		sceneName = GameManager.instance.sceneName;

		if (GameManager.instance.inConversation)
		{
			volume = 0.25f;
			
		}
		else
		{
			volume = 0.5f;
		}
		audioSource.volume = volume;

		if (audioSource.isPlaying == false) 
		{
			audioSource.PlayOneShot (clip, volume);
		}
	}
}
