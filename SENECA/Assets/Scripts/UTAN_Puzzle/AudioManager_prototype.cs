using System.Collections.Generic;
using UnityEngine;

#region AudioManager_prototype.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    AudioManager_prototype Info										                                                */
/*              Adds an audioSource to each note for the puzzles                                                        */
/*                                                                                                                      */
/*    Function List as of 05/29/2017:                                                                                   */
/*          internal:                                                                                                   */
/*                                                                                                                      */
/*          private:                                                                                                    */
/*              private void Awake ()                                                                                   */
/*                                                                                                                      */
/*          protected:                                                                                                  */
/*                                                                                                                      */
/*          public:                                                                                                     */
/*                                                                                                                      */
/*          public static:                                                                                              */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class AudioManager_prototype : MonoBehaviour
{ 
	public List<AudioSource> notes;

    #region Overview private void Awake()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Initalizing variables. Runs once at the beginning of the program before Start                                   */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void Awake () 
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			notes.Add(transform.GetChild(i).GetComponent<AudioSource>());
		}
	}
}
