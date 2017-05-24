using UnityEngine;
using ChrsUtils.ChrsEventSystem.GameEvents;
using SenecaEvents;

#region BGM_Singleton.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for playing all background music. BGM = Background Music                                              */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void OnDestroy()                                                                             */
/*                 private void OnSceneChane(GameEvent e)                                                               */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class BGM_Singleton : MonoBehaviour 
{
	public static BGM_Singleton instance;                   //  Instance of this class for access by other scripts

    public float volume;                                    //  Volume of the background music
    public string sceneName;                                //  Current scene's name
	
	public AudioSource audioSource;
	public AudioClip clip;

	private SceneChangeEvent.Handler onSceneChange;         //  Delegate for the SceneChange event

    #region Overview private void Start()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Initalizing variables. Runs once at the beginning of the program                                                */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void Start () 
	{
		if(instance == null)
		{
            //  We need this instance to access BGM_Singleton variables form other scripts. 
            //  The reason why we are Destroying on Load is to allow players to press the Restart Game key
            //  and have all values reset. The Restart Game key is Backspace as of 5/22/2017
			instance = this;
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
			else if(sceneName.Contains("Title"))
			{
				clip = Resources.Load("Audio/Music/Title_Theme_Loop") as AudioClip;
			}
			else if(sceneName.Contains("Credits"))
			{
				clip = Resources.Load("Audio/Music/Credits_Theme") as AudioClip;
			}

			onSceneChange = new SceneChangeEvent.Handler(OnSceneChange);
			Services.Events.Register<SceneChangeEvent>(onSceneChange);
			audioSource.PlayOneShot(clip, volume);
			audioSource.volume = volume;
		}
	
		onSceneChange = new SceneChangeEvent.Handler(OnSceneChange);
		Services.Events.Register<SceneChangeEvent>(onSceneChange);
		audioSource.PlayOneShot(clip, volume);
		audioSource.volume = volume;
	}

    #region Overview private void OnDestroy()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Unregistering for events when being destroyed to stop any null reference errors                                 */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnDestroy()
    {
        Services.Events.Unregister<SceneChangeEvent>(onSceneChange);
    }

    #region Overview private void OnSceneChange(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Changing the music when going from a Title Screen to Game scenes, Seneca to Utan, and Utan to Credits           */
    /*      This function is called when the event manager fires a new SceneChangeEvent.                                    */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The event that was fired                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnSceneChange(GameEvent e)
	{
		string newScene = ((SceneChangeEvent)e).sceneName;
		
		if (!sceneName.Contains ("Seneca") && newScene.Contains ("Seneca")) {

			audioSource.loop = true;
			clip = Resources.Load ("Audio/Music/Seneca_Theme") as AudioClip;
			audioSource.Stop ();
			audioSource.PlayOneShot (clip, volume);
		} else if (!sceneName.Contains ("Utan") && newScene.Contains ("Utan")) {
			audioSource.loop = true;
			clip = Resources.Load ("Audio/Music/Utan_theme") as AudioClip;
			audioSource.Stop ();
			audioSource.PlayOneShot (clip, volume);
			
		} else if (sceneName.Contains ("Title")) {
			clip = Resources.Load ("Audio/Music/Title_Theme") as AudioClip;
			audioSource.Stop ();
			audioSource.loop = false;
			audioSource.PlayOneShot (clip, volume);
		} else if (sceneName.Contains ("Credits")) {
			audioSource.loop = true;
			clip = Resources.Load ("Audio/Music/Title_Theme") as AudioClip;
			audioSource.Stop ();
			audioSource.PlayOneShot (clip, volume);
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
		sceneName = GameManager.instance.sceneName;

		if (GameManager.instance.inConversation)
		{
			volume = 0.2f;
		}
		else if(!GameManager.instance.endGame)
		{
			volume = 0.3f;
		}
		audioSource.volume = volume;

		if (!audioSource.isPlaying && audioSource.loop) 
		{
			audioSource.PlayOneShot (clip, volume);
		}
	}
}
