using UnityEngine;
using SenecaEvents;

#region DEPRECIATED ExitUtan1.cs Overview DEPRECIATED
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Going to the next scene after Utan1                                                                             */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void OnTriggerEnter2D(Collider2D coll)                                                       */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class ExitUtan1 : MonoBehaviour
{
    #region Overview private void OnTriggerEnter2D(Collider2D collider)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Going to the next scene after Utan1 					                                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          Collider2D collider: the object you collided with                                                           */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Player") 
		{
			Services.Events.Fire(new SceneChangeEvent("Utan_Meadow"));

			TransitionData.Instance.UTAN_FORK.position = coll.transform.position;
			TransitionData.Instance.UTAN_FORK.scale = coll.transform.localScale;
			Services.Scenes.Swap<UtanRoadSceneScript>(TransitionData.Instance);
		}
	}
}
