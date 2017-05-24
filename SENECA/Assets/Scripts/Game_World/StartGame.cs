using UnityEngine;
using UnityEngine.SceneManagement;

#region DEPRECIATED StartGame.cs Overview DEPRECIATED
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for starting the game                                                                                 */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class StartGame : MonoBehaviour 
{
	public KeyCode startGame = KeyCode.Tab;
	public const string OPENING_SCENE = "Seneca_Campsite";
	public const string PREAMBLE_SCENE = "_Prologue";

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
		if (Input.GetKeyDown(startGame))
		{
			SceneManager.LoadScene(OPENING_SCENE);
		}	
	}
}
