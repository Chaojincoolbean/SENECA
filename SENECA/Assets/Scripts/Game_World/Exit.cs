using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

public class Exit : MonoBehaviour 
{
	public const string SENECA_CAMPSITE = "To_SenecaCampsite";
	public const string SENECA_FARM = "To_SenecaFarm";
	public const string SENECA_FOREST_FORK= "To_SenecaForestFork";
	public const string SENECA_HUNTER_CAMP = "To_SenecaHuntercamp";
	public const string SENECA_MEADOW = "To_SenecaMeadow";
	public const string SENECA_RADIO_TOWER = "To_SenecaRadiotower";
	public const string SENECA_ROAD = "To_SenecaRoad";
	public const string SENECA_ROCKS = "To_SenecaRocks";

	public const string UTAN_CAMPSITE = "To_UtanCampsite";
	public const string UTAN_FARM = "To_UtanFarm";
	public const string UTAN_FOREST_FORK = "To_UtanForkPath";
	public const string UTAN_HUNTER_CAMP = "To_UtanHuntercamp";
	public const string UTAN_MEADOW = "To_UtanMeadow";
	public const string UTAN_RADIO_TOWER = "To_UtanRadioTower";
	public const string UTAN_ROAD = "To_UtanRoad";
	public const string UTAN_ROCKS = "To_UtanRocks";


	public bool canTransferScene;

	// Use this for initialization
	void Start () 
	{
		canTransferScene = false;
		StartCoroutine (CanTranferScene ());
	}

	IEnumerator CanTranferScene()
	{
		yield return new WaitForSeconds (1.0f);
		canTransferScene = true;
	}

	// Update is called once per frame
	void Update () {


	}

	// Do not go to nuext scene until EndConvo topic has been played
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player" && GameManager.instance.hasPriyaSpoken) 
		{
			TransferScene (coll.transform, transform.name);
			canTransferScene = false;
		}
	}

	void TransferScene(Transform player, string nextScene)
	{
		string newScene = nextScene.Replace ("To_", "");
        StartCoroutine(CanTranferScene());
		Services.Events.Fire(new SceneChangeEvent(newScene));
		SelectScene (player, nextScene);

	}

	void SelectScene(Transform player, string nextScene)
	{
		if (nextScene == SENECA_CAMPSITE) 
		{
			TransitionData.Instance.SENECA_CAMPSITE.position = player.position;
			TransitionData.Instance.SENECA_CAMPSITE.scale = player.localScale;
			Services.Scenes.Swap<SenecaCampsiteSceneScript>(TransitionData.Instance);
		}
		else if (nextScene == SENECA_FARM)
		{
			TransitionData.Instance.SENECA_FARM.position = player.position;
			TransitionData.Instance.SENECA_FORK.scale = player.localScale;
			Services.Scenes.Swap<SenecaFarmSceneScript>(TransitionData.Instance);
		}
		else if (nextScene == SENECA_FOREST_FORK) 
		{
			TransitionData.Instance.SENECA_FORK.position = player.position;
			TransitionData.Instance.SENECA_FORK.scale = player.localScale;
			Services.Scenes.Swap<SenecaForestForkSceneScript>(TransitionData.Instance);
		}

        else if (nextScene == SENECA_HUNTER_CAMP)
        {
            TransitionData.Instance.SENECA_HUNTER_CAMP.position = player.position;
            TransitionData.Instance.SENECA_HUNTER_CAMP.scale = player.localScale;
            Services.Scenes.Swap<SenecaHunterCampSceneScript>(TransitionData.Instance);

        }
		else if (nextScene == SENECA_MEADOW) 
		{
			TransitionData.Instance.SENECA_MEADOW.position = player.position;
			TransitionData.Instance.SENECA_MEADOW.scale = player.localScale;
			Services.Scenes.Swap<SenecaMeadowSceneSript>(TransitionData.Instance);
		}
		else if (nextScene == SENECA_RADIO_TOWER)
		{
			TransitionData.Instance.SENECA_RADIO_TOWER.position = player.position;
			TransitionData.Instance.SENECA_RADIO_TOWER.scale = player.localScale;
			Services.Scenes.Swap<SenecaRadioTowerSceneScript>(TransitionData.Instance);
		}
        else if (nextScene == SENECA_ROAD)
        {
            TransitionData.Instance.SENECA_ROAD.position = player.position;
            TransitionData.Instance.SENECA_ROAD.scale = player.localScale;
            Services.Scenes.Swap<SenecaRoadSceneScript>(TransitionData.Instance);
        }
		else if (nextScene == SENECA_ROCKS)
		{
			TransitionData.Instance.SENECA_ROCKS.position = player.position;
			TransitionData.Instance.SENECA_ROCKS.scale = player.localScale;
			Services.Scenes.Swap<SenecaRocksSceneScript>(TransitionData.Instance);
		}
        else if (nextScene == UTAN_CAMPSITE)
        {
            TransitionData.Instance.UTAN_CAMPSITE.position = player.position;
            TransitionData.Instance.UTAN_CAMPSITE.scale = player.localScale;
            Services.Scenes.Swap<UtanCampsiteSceneScript>(TransitionData.Instance);
        }
		else if (nextScene == UTAN_FARM)
		{
			TransitionData.Instance.UTAN_FARM.position = player.position;
			TransitionData.Instance.UTAN_FARM.scale = player.localScale;
			Services.Scenes.Swap<UtanFarmSceneScript>(TransitionData.Instance);
		}
        else if (nextScene == UTAN_FOREST_FORK)
        {
            TransitionData.Instance.UTAN_FORK.position = player.position;
            TransitionData.Instance.UTAN_FORK.scale = player.localScale;
            Services.Scenes.Swap<UtanForkPathSceneScript>(TransitionData.Instance);
        }
        else if (nextScene == UTAN_HUNTER_CAMP)
        {
            TransitionData.Instance.UTAN_HUNTER_CAMP.position = player.position;
            TransitionData.Instance.UTAN_HUNTER_CAMP.scale = player.localScale;
            Services.Scenes.Swap<UtanHunterCampSceneScript>(TransitionData.Instance);
        }
		else if (nextScene == UTAN_MEADOW)
		{
			TransitionData.Instance.UTAN_MEADOW.position = player.position;
			TransitionData.Instance.UTAN_MEADOW.scale = player.localScale;
			Services.Scenes.Swap<UtanMeadowSceneScript>(TransitionData.Instance);
		}
		else if (nextScene == UTAN_RADIO_TOWER)
		{
			TransitionData.Instance.UTAN_RADIO_TOWER.position = player.position;
			TransitionData.Instance.UTAN_RADIO_TOWER.scale = player.localScale;
			Services.Scenes.Swap<UtanRadioTowerSceneScript>(TransitionData.Instance);
		}
        else if (nextScene == UTAN_ROCKS)
        {
            TransitionData.Instance.UTAN_ROCKS.position = player.position;
            TransitionData.Instance.UTAN_ROCKS.scale = player.localScale;
            Services.Scenes.Swap<UtanRocksSceneScript>(TransitionData.Instance);
        }
        else if (nextScene == UTAN_ROAD)
        {
            TransitionData.Instance.UTAN_ROAD.position = player.position;
            TransitionData.Instance.UTAN_ROAD.scale = player.localScale;
            Services.Scenes.Swap<UtanRoadSceneScript>(TransitionData.Instance);
        }


    }


}
