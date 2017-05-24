using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

#region SenecaFarmSceneScript.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    This is the Scene script attached to the SenecaFarm screen.                                                       */
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
public class SenecaFarmSceneScript : Scene<TransitionData>
{
    public float nextTimeToSearch = 0;                          //  How long until the next time we search for the player

    public Player player;
    public AudioClip clip;
    public AudioSource audioSource;

	public string lastScene;                                    //  The last scene we were in
	public GameObject mainCamera;                               //  Reference to the main camera
	public Transform fromSenecaForestFork;                      //  Spawn positioin when coming from SenecaForestFork
	public Transform fromSenecaHuntercamp;                      //  Spawn position when coming from SenecaHuntercamp

	public Transform[] ToggleSortingLayerLocations;             //  Sorting layers for rendering the sprite
	public SpriteRenderer[] farmLayers;                         //  Farm sorting layers

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
        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_ForkWitchLight") as AudioClip;

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
    private void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject result = GameObject.FindGameObjectWithTag("Player");
            if (result != null)
            {
				if (lastScene == "SenecaForestForkSceneScript") {
					result.transform.position = fromSenecaForestFork.position;
					result.transform.localScale = fromSenecaForestFork.localScale;

				}
				if (lastScene == "SenecaHunterCampSceneScript") {
					result.transform.position = fromSenecaHuntercamp.position;
					result.transform.localScale = fromSenecaHuntercamp.localScale;

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
    private void Update()
    {

        if (player == null)
        {
            FindPlayer();
            return;
        }

		if (player.transform.position.y <= ToggleSortingLayerLocations [0].position.y) {
			farmLayers [2].sortingOrder = 1;
		} else {
			farmLayers [2].sortingOrder = 11;
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
        TransitionData.Instance.SENECA_FARM.position = player.transform.position;
        TransitionData.Instance.SENECA_FARM.scale = player.transform.localScale;
        TransitionData.Instance.SENECA_FARM.visitedScene = true;
    }
}
