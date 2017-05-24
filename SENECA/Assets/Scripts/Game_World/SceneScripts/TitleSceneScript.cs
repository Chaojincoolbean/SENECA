using GameScenes;

#region TitleSceneScript.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    This is the Scene script attached to the title screen.                                                            */
/*                                                                                                                      */
/*    TitleSceneScript.cs is responsible  for:                                                                          */
/*    Setting bools for the player visiting a scene to false                                                            */
/*                                                                                                                      */
/*    TitleScene logic is handled in Scripts -> HARTO -> HARTO_UI -> TitleMenu_HARTO.cs                                 */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          internal:                                                                                                   */
/*                 internal override void OnEnter(TransitionData data)                                                  */
/*                 internal override void OnExit()                                                                      */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class TitleSceneScript : Scene<TransitionData> 
{
    #region Overview internal override void OnEnter(TransitionData data)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Running when entering a scene					                                                            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          TradnsitionData data: A class with structs that represent data stored between each scene.                   */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    internal override void OnEnter(TransitionData data)
	{
        TransitionData.Instance.SENECA_CAMPSITE.visitedScene = false;
        TransitionData.Instance.SENECA_FORK.visitedScene = false;
        TransitionData.Instance.SENECA_FARM.visitedScene = false;
        TransitionData.Instance.SENECA_HUNTER_CAMP.visitedScene = false;
        TransitionData.Instance.SENECA_MEADOW.visitedScene = false;
        TransitionData.Instance.SENECA_RADIO_TOWER.visitedScene = false;
        TransitionData.Instance.SENECA_ROAD.visitedScene = false;
        TransitionData.Instance.SENECA_ROCKS.visitedScene = false;

        TransitionData.Instance.UTAN_CAMPSITE.visitedScene = false;
        TransitionData.Instance.UTAN_FARM.visitedScene = false;
        TransitionData.Instance.UTAN_HUNTER_CAMP.visitedScene = false;
        TransitionData.Instance.UTAN_FORK.visitedScene = false;
        TransitionData.Instance.UTAN_MEADOW.visitedScene = false;
        TransitionData.Instance.UTAN_RADIO_TOWER.visitedScene = false;
        TransitionData.Instance.UTAN_ROAD.visitedScene = false;
        TransitionData.Instance.UTAN_ROCKS.visitedScene = false;

    }

    #region Overview internal override void OnExit()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Running when exiting a scene					                                                            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*           None                                                                                                       */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    internal override void OnExit() { }
}
