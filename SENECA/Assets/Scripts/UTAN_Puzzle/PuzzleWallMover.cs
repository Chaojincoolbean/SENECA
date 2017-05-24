using UnityEngine;

#region  PuzzleWallMover.cs Overview 
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Storing a refernece to the wall move animator                                                                     */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class PuzzleWallMover : MonoBehaviour 
{
	public Animator anim;

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
		anim = GetComponent<Animator>();	
	}
}
