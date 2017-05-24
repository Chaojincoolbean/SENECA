using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

#region SenecaRoadSceneScript.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    This is the Scene script attached to the SenecaRoad screen.                                                       */
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
public class SenecaRoadSceneScript : Scene<TransitionData>
{
    public float nextTimeToSearch = 0;                  //  How long unitl we search for the player again
    public string lastScene;                            //  Name of the last scene we were in

    public Player player;                               //  Reference to the player
    public AudioClip clip;
    public AudioSource audioSouorce;

    public Transform fromSenecaRadiotower;              //  Spawn position when coming from Radio tower
    public Transform fromSenecaHuntercamp;              //  Spawn position when coming form the Hunter camp
    public Transform fromSenecaRocks;                   //  spawn position when coming from the rocks

    public GameObject mainCamera;                       //  Reference to the main camera

    public Transform[] ToggleSortingLayerLocations;     //  Sorting layer for correct sprite layering
    public SpriteRenderer[] roadLayers;                 //  Road layer order

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
        audioSouorce = GetComponent<AudioSource>();
        clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_RoadWitchLight") as AudioClip;

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
        
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 3.88f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -3.88f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0.41f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = -0.41f;

        if (!TransitionData.Instance.SENECA_ROAD.visitedScene)
        {
            GameObject.Find("witchlightRoad").GetComponent<Animator>().SetBool("ChaseMe", true);
            audioSouorce = GetComponent<AudioSource>();
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_RoadWitchLight") as AudioClip;
            if (clip != null)
            {
                audioSouorce.PlayOneShot(clip);
            }
            else
            {
                audioSouorce = GetComponent<AudioSource>();
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_RoadWitchLight") as AudioClip;
                audioSouorce.PlayOneShot(clip);
            }
        }
        else
        {
        }
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
				if (lastScene == "SenecaRadioTowerSceneScript") {
					result.transform.position = fromSenecaRadiotower.position;
					result.transform.localScale = fromSenecaRadiotower.localScale;

				}
				if (lastScene == "SenecaHunterCampSceneScript") {
					result.transform.position = fromSenecaHuntercamp.position;
					result.transform.localScale = fromSenecaHuntercamp.localScale;
				}
				if (lastScene == "SenecaRocksSceneScript") {
					result.transform.position =  fromSenecaRocks.position;
					result.transform.localScale =  fromSenecaRocks.localScale;
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

		/*if (player.transform.position.y <= ToggleSortingLayerLocations [0].position.y
			&& player.transform.position.x <= ToggleSortingLayerLocations[0].position.x) {
			roadLayers [1].sortingOrder = 9;
		} else {
			roadLayers [1].sortingOrder = 11;
		}*/

		if (player.transform.position.y <= ToggleSortingLayerLocations [1].position.y) {
			roadLayers [0].sortingOrder = 9;
		} else {
			roadLayers [0].sortingOrder = 11;
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
        TransitionData.Instance.SENECA_ROAD.visitedScene = true;
    }
}
