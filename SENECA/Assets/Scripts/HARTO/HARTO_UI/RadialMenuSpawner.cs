using UnityEngine;
using UnityEngine.UI;

#region RadialMenuSpawner.cs Overview
/*********************************************************************************************************************************************/
/*                                                                                                                                           */
/*    RadialMenuSpawner.cs is responsible spawning a new HARTO menu and removing the old one                                                 */
/*                                                                                                                                           */
/*    Function List as of 5/20/2017:                                                                                                         */
/*          public:                                                                                                                          */
/*                  public void SpawnMenu(HARTO_UI_Interface obj, Player player, bool dialogueModeActive, bool topicSelected, bool delay)    */
/*                  public void DestroyMenu()                                                                                                */
/*                                                                                                                                           */
/*          private:                                                                                                                         */
/*                  private void Start()                                                                                                     */
/*                                                                                                                                           */
/*********************************************************************************************************************************************/
#endregion
public class RadialMenuSpawner : MonoBehaviour 
{
	public bool closing;
	public AudioClip clip;
	public AudioSource audioSource;

	public RadialMenu menuPrefab;
	public RadialMenu newMenu;
	public RadialMenu oldMenu;

    public GameObject uiMouse;

    public EasingProperties easing;

    private static bool firstPass = true;
    private RectTransform spawnPosition;

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
    private void Start()
    {
        easing = ScriptableObject.CreateInstance("EasingProperties") as EasingProperties;

        spawnPosition = GameObject.Find("HARTO_UI_Location").GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.3f;
    }

    #region Overview public void SpawnMenu(HARTO_UI_Interface obj, Player player, bool dialogueModeActive, bool topicSelected, bool delay)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Creating a new HARO menu                                                                                        */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          HARTO_UI_Interface obj: where the HARTO menu gets the info on what to spawn                                 */
    /*          Player player: referecne to the player                                                                      */
    /*          bool dialogueModeActive: Are we in dialogue mode or recording mode                                          */
    /*          bool topicSelected: Has a topic been selected                                                               */
    /*          bool delay: not used                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void SpawnMenu(HARTO_UI_Interface obj, Player player, bool dialogueModeActive, bool topicSelected, bool delay)
	{
		spawnPosition = GameObject.Find("HARTO_UI_Location").GetComponent<RectTransform>();
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.3f;

		if (audioSource != null)
        {
			audioSource.Stop ();
		}
		clip = Resources.Load("Audio/SFX/HARTO_SFX/OpenHARTO") as AudioClip;

		if(!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(clip);
		}

		newMenu = Instantiate(menuPrefab) as RadialMenu;
		newMenu.transform.SetParent(transform, false);
		newMenu.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		newMenu.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y, spawnPosition.position.z);
		newMenu.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1);
		newMenu.Init(player, this);
		newMenu.SpawnIcons(obj, topicSelected);

		if(firstPass)
		{
			Vector3 tabPosition = GameObject.Find("Mouse_Location").transform.localPosition;
			GameObject mouse = Instantiate(uiMouse, tabPosition, Quaternion.identity);

			mouse.transform.SetParent(GameObject.Find("HARTOCanvas").transform, false);
			firstPass = false;
		}
	}

    #region Overview public void DestroyMenu()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Destroying the menu                                                                                             */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void DestroyMenu()
	{
		if (newMenu != null)
		{
			audioSource.Stop();

			clip = Resources.Load("Audio/SFX/HARTO_Close") as AudioClip;

			if(!audioSource.isPlaying && !HARTO_UI_Interface.HARTOSystem.isHARTOActive)
			{
				audioSource.PlayOneShot(clip);
			}

			closing = true;
			newMenu._anim.SetBool("Confirm",false);
			newMenu._anim.SetBool ("Inactive", true);
			closing = false;
			newMenu._destroyed = true;

			Destroy(newMenu.gameObject);
		}
	}
}
