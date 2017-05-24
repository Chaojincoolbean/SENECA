using UnityEngine;

#region AssignAnimalSong.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    This script is ueed to assign the Animal song in the approproate scene                                            */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*           private:                                                                                                   */
/*                 private void Start()                                                                                 */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class AssignAnimalSong : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource audioSource;

    #region Overview public void Start()
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
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        if (transform.name == "Road")
        {
            clip = Resources.Load("Audio/SFX/SFX/INDIVIDUAL PUZZLE NOTES/SecondPuzzle_AnimalSong") as AudioClip;
        }
        else
        {
            clip = Resources.Load("Audio/SFX/SFX/INDIVIDUAL PUZZLE NOTES/FirstPuzzle_AnimalSong") as AudioClip;
        }

        audioSource.PlayOneShot(clip);
	}
}
