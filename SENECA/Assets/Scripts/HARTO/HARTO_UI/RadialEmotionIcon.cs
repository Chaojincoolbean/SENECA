using UnityEngine.UI;

#region RadialEmotionIcon.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for emotional icon data. Inherits from RadialIcon.cs                                                  */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class RadialEmotionIcon : RadialIcon
{
	public Emotions emotion;            //  Based of the enum emotion in HARTO.cs

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
		GetComponent<Image> ().color = transform.parent.GetChild (0).gameObject.GetComponent<Image> ().color;
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
		GetComponent<Image> ().color = transform.parent.GetChild (0).gameObject.GetComponent<Image> ().color;
	}
}
