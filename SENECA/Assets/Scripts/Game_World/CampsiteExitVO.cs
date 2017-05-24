using UnityEngine;
using SenecaEvents;

#region BeornHARTO.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for Beorn's existance in the scenes and starting Astrid finding Beorn's HARTO scene                   */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void OnTriggerEnter2D(Collider2D collider)                                                   */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class CampsiteExitVO : MonoBehaviour
{
    public static bool hasPlayedOnce = false;
    public AudioClip clip;
    public AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Adventure") as AudioClip;
    }

    #region Overview private void OnTriggerEnter2D(Collider2D collider)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Starting the Astrid exiting the campsite scene line                         					            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          Collider2D collider: the object you collided with                                                           */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && GameManager.instance.hasPriyaSpoken && !hasPlayedOnce)
        {
            GameManager.instance.trackProgressInHARTO = true;
            Services.Events.Fire(new InteractableEvent(true, true, true));
            
            audioSource.PlayOneShot(clip);
            hasPlayedOnce = true;
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
		if(!audioSource.isPlaying && hasPlayedOnce && !GameManager.instance.tutorialIsDone && !GameManager.instance.HARTOinUtan)
        {
            GameManager.instance.trackProgressInHARTO = false;
            //  Stops talking animation, puts the HARTO arm down , and unfreezes animation.
            //  Put this in an event to make it work nicer
            Services.Events.Fire(new InteractableEvent(false, false, false));
        }
	}
}
