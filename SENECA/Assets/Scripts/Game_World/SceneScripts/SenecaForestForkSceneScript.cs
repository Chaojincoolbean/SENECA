using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

#region SenecaForestForkSceneScript.cs Overview
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
public class SenecaForestForkSceneScript : Scene<TransitionData> 
{
    public float nextTimeToSearch = 0;                      //  How long unitl I search for the player again
    public string lastScene;

    public Player player;                                   //  Reference to the player
    public AudioClip clip;
    public AudioSource audioSource;

    public Transform fromSenecaCampsite;
    public Transform fromSenecaFarm;
    public Transform fromSenecaRocks;

    public GameObject mainCamera;

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
        if (!TransitionData.Instance.SENECA_FORK.visitedScene)
        {
            audioSource = GetComponent<AudioSource>();
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                audioSource = GetComponent<AudioSource>();
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_ForkWitchLight") as AudioClip;
                audioSource.PlayOneShot(clip);
            }
        }

        GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0.35f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = -0.35f;
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
				if (lastScene == "SenecaCampsiteSceneScript" && GameManager.instance.tutorialIsDone) {
					result.transform.position = fromSenecaCampsite.position;
					result.transform.localScale = fromSenecaCampsite.localScale;

				}
				if (lastScene == "SenecaFarmSceneScript") {
					result.transform.position = fromSenecaFarm.position;
					result.transform.localScale = fromSenecaFarm.localScale;
					
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
    private void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
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
		TransitionData.Instance.SENECA_FORK.visitedScene = true;
	}
}
