using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

#region SenecaMeadowSceneScript.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    This is the Scene script attached to the SenecaMeadow screen.                                                     */
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
public class SenecaMeadowSceneSript : Scene<TransitionData>
{
    public float nextTimeToSearch = 0;          //  How long unitl we search for the player
    public Player player;                       //  Reference to the player
    public AudioClip clip;
    public AudioSource audioSource;

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
        clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_MeadowWitchLight") as AudioClip;
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
        GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0.35f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = -0.35f;

        if(!TransitionData.Instance.SENECA_MEADOW.visitedScene)
        {
            audioSource = GetComponent<AudioSource>();
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_MeadowWitchLight") as AudioClip;
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                audioSource = GetComponent<AudioSource>();
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_MeadowWitchLight") as AudioClip;
                audioSource.PlayOneShot(clip);
            }
        }
        FindPlayer();
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
        GameObject result = GameObject.FindGameObjectWithTag("Player");
        if (result != null)
        {
            player = result.GetComponent<Player>();
            player.transform.position = new Vector3(GameObject.Find("SenecaMeadowSpawnPoint").transform.position.x, GameObject.Find("SenecaMeadowSpawnPoint").transform.position.y, GameObject.Find("SenecaMeadowSpawnPoint").transform.position.z);

        }
        nextTimeToSearch = Time.time + 2.0f;
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
		TransitionData.Instance.SENECA_MEADOW.visitedScene = true;
	}
}
