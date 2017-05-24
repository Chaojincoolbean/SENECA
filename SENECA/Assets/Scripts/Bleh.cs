using UnityEngine;

#region  Bleh.cs Overview 
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Holds a function to be called by the CreditSceneScript Animator                                                   */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                                                                                                                      */
/*          public:                                                                                                     */
/*                 public void PlayBleh()                                                                               */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class Bleh : MonoBehaviour
{
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
        clip = Resources.Load("Audio/VO/HARTO/BLEH!") as AudioClip;	
	}

    #region Overview public void PlayBleh()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Playing BLEH in the CreditSceneScript animator                                                                  */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void PlayBleh()
    {
        audioSource.PlayOneShot(clip);
    }
}
