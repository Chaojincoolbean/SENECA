using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

#region UtanFarmSceneScript.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    This is the Scene script attached to the UtanFarm screen.                                                       */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          internal:                                                                                                   */
/*                 internal override void OnEnter(TransitionData data)                                                  */
/*                 internal override void OnExit()                                                                      */
/*                                                                                                                      */
/*           private:                                                                                                   */
/*                 private void Start()                                                                                 */
/*                 private void FindPlayer()                                                                            */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class UtanFarmSceneScript : Scene<TransitionData>
{
    public float nextTimeToSearch = 0;                  //  How long unitl I search for the player again
    public string lastScene;                            //  Reference to the previous scene
    public Player player;                               //  Reference to  the player

    public Transform fromUtanForkPath;                  //  Spawn position when coming from Utan Fork Path
    public Transform fromUtanHuntercamp;                //  Spawn position when coming from Utan Hunter camp
    public GameObject mainCamera;                       //  referecne to the main camera

	public Transform[] ToggleSortingLayerLocations;     //  Rendering order for sprites
	public SpriteRenderer[] farmLayers;                 //  Layer order for Utan farm scene

    #region Overview public void Start()
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
    private void Start()
	{
		mainCamera = GameObject.Find ("Main Camera");
		lastScene = mainCamera.GetComponent<GameManager> ().currentScene;
	}

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
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 3.93f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -3.93f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0.38f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = -0.38f;
    }

    #region Overview private void FindPlayer()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Finding the player if the player reference is null                                 				            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject result = GameObject.FindGameObjectWithTag("Player");
            if (result != null)
            {

				if (lastScene == "UtanForkPathSceneScript") {
					result.transform.position = fromUtanForkPath.position;
					result.transform.localScale = fromUtanForkPath.localScale;

				}
				if (lastScene == "UtanHunterCampSceneScript") {
					result.transform.position = fromUtanHuntercamp.position;
					result.transform.localScale = fromUtanHuntercamp.localScale;

				}

                player = result.GetComponent<Player>();
            }
            nextTimeToSearch = Time.time + 2.0f;
        }
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
    void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }

		if (player.transform.position.y <= ToggleSortingLayerLocations [0].position.y) {
			farmLayers [1].sortingOrder = 1;
		} else {
			farmLayers [1].sortingOrder = 11;
		}
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
    internal override void OnExit()
    {
        TransitionData.Instance.UTAN_FARM.visitedScene = true;
    }
}
