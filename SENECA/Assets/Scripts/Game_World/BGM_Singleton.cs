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

			if(SceneManager.GetActiveScene().name.Contains("Utan"))
			{
				clip = Resources.Load("Audio/Music/Seneca - Diary - utan or title sketch 1 v2 mix v1") as AudioClip;
			}
			else if(SceneManager.GetActiveScene().name.Contains("Seneca"))
			{
				clip = Resources.Load("Audio/Music/Seneca - Diary - seneca theme v1") as AudioClip;
			}
			else if(SceneManager.GetActiveScene().name.Contains("Title"))
			{
				clip = Resources.Load("Audio/Music/Seneca - Diary - seneca theme v1") as AudioClip;
			}

			onSceneChange = new SceneChangeEvent.Handler(OnSceneChange);
			GameEventsManager.Instance.Register<SceneChangeEvent>(onSceneChange);
			audioSource.PlayOneShot(clip, volume);
			audioSource.volume = volume;
		}
		else
		{
			Destroy(gameObject);
		}

		

	}

	void OnSceneChange(GameEvent e)
	{
		string newScene = ((SceneChangeEvent)e).sceneName;
		
		Debug.Log(sceneName + " || " + newScene);

		if(sceneName.Contains("Utan") && newScene.Contains("Seneca"))
		{
			clip = Resources.Load("Audio/Music/Seneca - Diary - seneca theme v1") as AudioClip;
			audioSource.Stop();
			audioSource.PlayOneShot(clip, volume);
		}
		else if (sceneName.Contains("Seneca") && newScene.Contains("Utan"))
		{
			clip = Resources.Load("Audio/Music/Seneca - Diary - utan or title sketch 1 v2 mix v1") as AudioClip;
			audioSource.Stop();
			audioSource.PlayOneShot(clip, volume);
			
		}
		else if (sceneName.Contains("Credits"))
		{
			clip = Resources.Load("Audio/Music/Seneca - Diary - seneca theme v1") as AudioClip;
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
