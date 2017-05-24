using UnityEngine;

#region RemoveWalkUI.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    RemoveWalkUI.cs is responsible for removing the WALK UI after pressing a vertical and horizontal input            */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                  private void Update()                                                                               */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class RemoveWalkUI : MonoBehaviour
{
    public bool horizontalInput;
    public bool verticalInput;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            verticalInput = true;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            horizontalInput = true;
        }

        if (verticalInput && horizontalInput)
        {
            Destroy(this.gameObject);
        }
    }
}
