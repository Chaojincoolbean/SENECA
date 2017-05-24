using System.Collections;
using UnityEngine;
using SenecaEvents;

#region Exit.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for chaning between all game scenes. There is a 2 second scene lock when changing scenes to prevent   */
/*    the player from going back and forht between scenes rapidly                                                       */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private IEnumerator CanTranferScene()                                                                */
/*                 private void OnTriggerEnter2D(Collider2D coll)                                                       */
/*                 private void TransferScene(Transform player, string nextScene)                                       */
/*                 private void SelectScene(Transform player, string nextScene)                                         */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class Exit : MonoBehaviour 
{
    //  Name of the Seneca Exit triggers
	public const string SENECA_CAMPSITE = "To_SenecaCampsite";
	public const string SENECA_FARM = "To_SenecaFarm";
	public const string SENECA_FOREST_FORK= "To_SenecaForestFork";
	public const string SENECA_HUNTER_CAMP = "To_SenecaHuntercamp";
	public const string SENECA_MEADOW = "To_SenecaMeadow";
	public const string SENECA_RADIO_TOWER = "To_SenecaRadiotower";
	public const string SENECA_ROAD = "To_SenecaRoad";
	public const string SENECA_ROCKS = "To_SenecaRocks";

    //  Name of the Utan Exit triggers
    public const string UTAN_CAMPSITE = "To_UtanCampsite";
	public const string UTAN_FARM = "To_UtanFarm";
	public const string UTAN_FOREST_FORK = "To_UtanForkPath";
	public const string UTAN_HUNTER_CAMP = "To_UtanHuntercamp";
	public const string UTAN_MEADOW = "To_UtanMeadow";
	public const string UTAN_RADIO_TOWER = "To_UtanRadioTower";
	public const string UTAN_ROAD = "To_UtanRoad";
	public const string UTAN_ROCKS = "To_UtanRocks";

	public bool canTransferScene;
	public string currentScene;
	public GameObject mainCamera;

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
    private void Start () 
	{
		canTransferScene = false;
		StartCoroutine (CanTranferScene ());
		mainCamera = GameObject.Find("Main Camera");
	}

    #region Overview IEnumerator CanTransferScene()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Starting the scene lock cooldown.                      				                                        */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The type of objects to enumerate.                                                                           */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private IEnumerator CanTranferScene()
	{
		yield return new WaitForSeconds (2.0f);
		canTransferScene = true;
	}

    #region Overview private void OnTriggerEnter2D(Collider2D collider)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Determining if player is closer enough to exit the scene                    					            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          Collider2D collider: the object you collided with                                                           */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player" && GameManager.instance.hasPriyaSpoken && canTransferScene)
        { 
			TransferScene (coll.transform, transform.name);
			canTransferScene = false;
		}
	}

    #region Overview private void TransferScene(Transform player, string nextScene)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Sending the player to the appropriate scene                                 					            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          Collider2D collider: the object you collided with                                                           */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void TransferScene(Transform player, string nextScene)
	{
		string newScene = nextScene.Replace ("To_", "");
        Services.Events.Fire(new SceneChangeEvent(newScene));
		SelectScene (player, nextScene);
	}

    #region Overview private void SelectScene(Transform player, string nextScene)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Actually swaps the scenes                                                    					            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          Collider2D collider: the object you collided with                                                           */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void SelectScene(Transform player, string nextScene)
	{
		currentScene = this.transform.parent.name;
		mainCamera.GetComponent<GameManager> ().currentScene = currentScene;

		if (nextScene == SENECA_CAMPSITE) 
		{
			Services.Scenes.Swap<SenecaCampsiteSceneScript>(TransitionData.Instance);
		}
		else if (nextScene == SENECA_FARM)
		{
			Services.Scenes.Swap<SenecaFarmSceneScript>(TransitionData.Instance);
		}
		else if (nextScene == SENECA_FOREST_FORK) 
		{
			Services.Scenes.Swap<SenecaForestForkSceneScript> (TransitionData.Instance);

		}
        else if (nextScene == SENECA_HUNTER_CAMP)
        {
            Services.Scenes.Swap<SenecaHunterCampSceneScript>(TransitionData.Instance);
        }
		else if (nextScene == SENECA_MEADOW) 
		{
			Services.Scenes.Swap<SenecaMeadowSceneSript>(TransitionData.Instance);
		}
		else if (nextScene == SENECA_RADIO_TOWER)
		{
			Services.Scenes.Swap<SenecaRadioTowerSceneScript>(TransitionData.Instance);
		}
        else if (nextScene == SENECA_ROAD)
        {
            Services.Scenes.Swap<SenecaRoadSceneScript>(TransitionData.Instance);
        }
		else if (nextScene == SENECA_ROCKS)
		{
			Services.Scenes.Swap<SenecaRocksSceneScript>(TransitionData.Instance);
		}
        else if (nextScene == UTAN_CAMPSITE)
        {
            Services.Scenes.Swap<UtanCampsiteSceneScript>(TransitionData.Instance);
        }
		else if (nextScene == UTAN_FARM)
		{
			Services.Scenes.Swap<UtanFarmSceneScript>(TransitionData.Instance);
		}
        else if (nextScene == UTAN_FOREST_FORK)
        {
            Services.Scenes.Swap<UtanForkPathSceneScript>(TransitionData.Instance);
        }
        else if (nextScene == UTAN_HUNTER_CAMP)
        {
            Services.Scenes.Swap<UtanHunterCampSceneScript>(TransitionData.Instance);
        }
		else if (nextScene == UTAN_MEADOW)
		{
			Services.Scenes.Swap<UtanMeadowSceneScript>(TransitionData.Instance);
		}
		else if (nextScene == UTAN_RADIO_TOWER)
		{
			Services.Scenes.Swap<UtanRadioTowerSceneScript>(TransitionData.Instance);
		}
        else if (nextScene == UTAN_ROCKS)
        {
            Services.Scenes.Swap<UtanRocksSceneScript>(TransitionData.Instance);
        }
        else if (nextScene == UTAN_ROAD)
        {
            Services.Scenes.Swap<UtanRoadSceneScript>(TransitionData.Instance);
        }
    }
}
