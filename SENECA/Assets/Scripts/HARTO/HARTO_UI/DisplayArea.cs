using UnityEngine;
using UnityEngine.UI;

#region DisplayArea.cs Overview
/*********************************************************************************************************************************************************/
/*                                                                                                                                                       */
/*    Sets the display image at the beginning of the game                                                                                                */
/*                                                                                                                                                       */
/*    Function List as of 5/20/2017:                                                                                                                     */
/*          private:                                                                                                                                     */
/*                 private void Awake ()                                                                                                                 */
/*                                                                                                                                                       */
/*********************************************************************************************************************************************************/
#endregion
public class DisplayArea : MonoBehaviour 
{
	public Image displayIcon;
	public Sprite defaultSprite;

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
		displayIcon = GetComponent<Image>();	
		
	}
}
