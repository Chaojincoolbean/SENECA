using UnityEngine;
using UnityEngine.UI;

#region RadialIcon.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for icon data.                                                                                        */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class RadialIcon : MonoBehaviour 
{
	public bool alreadySelected;
	public string title;
	public Image color;
	public Image icon;
	public RadialMenu myMenu;

    #region Overview protected void Start()
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
    protected void Start()
    {
		color.color = new Color (1f, 1f, 1f, 0f);
	}

    #region Overview protecetd void Update()
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
    protected void Update()
    {
		if (alreadySelected)
        {
			color.color = new Color (0.5f, 0.5f, 0.5f, 1.0f);
		}
        else
        {
			color.color = new Color (1f,1f,1f, 1.0f);
		}
    }
}
