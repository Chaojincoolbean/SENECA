using UnityEngine;

#region witchlightmanager.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for moving the witchlight in the Seneca Forest Fork scene                                             */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class witchlightmanager : MonoBehaviour
{
    public float x;         //  Witchlight's x position in the Seneca Forest Fork Scene
    public float y;         //  Witchlight's y position in the Seneca Forest Fork Scene

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
    void Start ()
    {
		x = this.gameObject.transform.position.x;
		y = this.gameObject.transform.position.y;
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
    void Update ()
    {
		x = x + 0.05f;

		if (x >= 0f)
        {
			x = x + 0.02f;
			y = y - 0.03f;
		}

		if (x >= 2f)
        {
			x = x + 0.01f;
			y = y - 0.03f;
		}

		this.gameObject.transform.position = new Vector3 (x, y, 0);
	}
}
