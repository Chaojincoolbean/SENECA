using UnityEngine;
using UnityEngine.UI;

#region ColorControl.cs Overview
/*********************************************************************************************************************************************************/
/*                                                                                                                                                       */
/*    I don't know what this one does  in Seneca                                                                                                         */
/*                                                                                                                                                       */
/*    Function List as of 5/20/2017:                                                                                                                     */
/*          private:                                                                                                                                     */
/*                 private void Update()                                                                                                                 */
/*                                                                                                                                                       */
/*********************************************************************************************************************************************************/
#endregion
public class ColorControl : MonoBehaviour
{
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
		GetComponent<Image> ().color = transform.parent.GetChild (0).gameObject.GetComponent<Image> ().color;
	}
}
