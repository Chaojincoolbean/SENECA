using System.Collections;
using UnityEngine;
using SenecaEvents;

#region Prologue.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Loads the next scene when the prologue is done                                                                    */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private IEnumerator LoadNextScene()                                                                  */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/*          public:                                                                                                     */
/*                public void LoadNext()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class Prologue : MonoBehaviour 
{
	public AudioClip clip;                      //  Reference to audioclip to be played
	private AudioSource audioSource;            //  Reference to the audioSource

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
		clip = Resources.Load("Audio/VO/Beorn/BEORN_VO_GAMEINTRO") as AudioClip;
		audioSource = GetComponent<AudioSource>();

		audioSource.PlayOneShot(clip);
		GameManager.instance.inConversation = true;	
	}

    #region Overview IEnumerator LoadNextScene()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          swaping the prologue scene with the CampsiteScene					                                        */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The type of objects to enumerate.                                                                           */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private IEnumerator LoadNextScene()
	{
		yield return new WaitForSeconds(4.0f);
		Services.Events.Fire(new SceneChangeEvent("Seneca_Campsite"));
		TransitionData.Instance.TITLE.visitedScene = true;
		TransitionData.Instance.TITLE.position = Vector3.zero;
		TransitionData.Instance.TITLE.scale = Vector3.zero;
		Services.Scenes.Swap<SenecaCampsiteSceneScript>(TransitionData.Instance);
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
		/*
		if(!audioSource.isPlaying)
		{
			GameManager.instance.inConversation = false;
			StartCoroutine(LoadNextScene());	
		}
		*/

	    /*	
         
        Skips the Prologue
         
     	if (Input.GetKey (KeyCode.Space)) 
    	{
			GameManager.instance.inConversation = false;
			StartCoroutine(LoadNextScene());
		}
        */
	}

    #region Overview public void LoadNext()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Starting to the LoadNextScene IEnumerator				                                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void LoadNext()
    {
		GameManager.instance.inConversation = false;
		StartCoroutine(LoadNextScene());
	}
}
