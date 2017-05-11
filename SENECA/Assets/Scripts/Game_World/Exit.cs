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
	public const string SENECA_Huntercamp = "To_SenecaHuntercamp";
	public const string SENECA_MEADOW = "To_SenecaMeadow";
	public const string SENECA_RADIOTOWER = "To_SenecaRadiotower";
	public const string SENECA_ROAD = "To_SenecaRoad";
	public const string SENECA_ROCK = "To_SenecaRocks";

	public const string UTAN_CAMPSITE = "To_UtanCampsite";
	public const string UTAN_FARM = "To_UtanFarm";
	public const string UTAN_FOREST_FORK = "To_UtanForestFork";
	public const string UTAN_Huntercamp = "To_UtanHuntercamp";
	public const string UTAN_MEADOW = "To_UtanMeadow";
	public const string UTAN_RADIOTOWER = "To_UtanRadioTower";
	public const string UTAN_ROAD = "To_UtanRoad";
	public const string UTAN_ROCK = "To_UtanRocks";


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
		if (coll.gameObject.tag == "Player" && canTransferScene) 
		{
			TransferScene (coll.transform, transform.name);
			canTransferScene = false;
		}
	}

	void TransferScene(Transform player, string nextScene)
	{
		Debug.Log ("Transfer " + nextScene);
		string newScene = nextScene.Replace ("To_", "");
		Services.Events.Fire(new SceneChangeEvent(newScene));
		SelectScene (player, nextScene);

	}

	void SelectScene(Transform player, string nextScene)
	{
		Debug.Log ("Select " + nextScene);
		if (nextScene == SENECA_CAMPSITE) 
		{
			TransitionData.Instance.SENECA_CAMPSITE.position = player.position;
			TransitionData.Instance.SENECA_CAMPSITE.scale = player.localScale;
			Services.Scenes.Swap<SenecaCampsiteSceneScript>(TransitionData.Instance);
		} 
		else if (nextScene == SENECA_FOREST_FORK) 
		{
			TransitionData.Instance.SENECA_FORK.position = player.position;
			TransitionData.Instance.SENECA_FORK.scale = player.localScale;
			Services.Scenes.Swap<SenecaForestForkSceneScript>(TransitionData.Instance);
		}
		else if (nextScene == SENECA_MEADOW) 
		{
			TransitionData.Instance.SENECA_MEADOW.position = player.position;
			TransitionData.Instance.SENECA_MEADOW.scale = player.localScale;
			Services.Scenes.Swap<SenecaMeadowSceneSript>(TransitionData.Instance);
		}
		else if (nextScene == UTAN_MEADOW) 
		{
			TransitionData.Instance.UTAN_MEADOW.position = player.position;
			TransitionData.Instance.UTAN_MEADOW.scale = player.localScale;
			Services.Scenes.Swap<UtanMeadowSceneScript>(TransitionData.Instance);
		}
		else if (nextScene == UTAN_ROAD) 
		{
			TransitionData.Instance.UTAN_ROAD.position = player.position;
			TransitionData.Instance.UTAN_ROAD.scale = player.localScale;
			Services.Scenes.Swap<UtanRoadSceneScript>(TransitionData.Instance);
		}
		else if (nextScene == UTAN_FOREST_FORK) 
		{
			TransitionData.Instance.UTAN_FORK.position = player.position;
			TransitionData.Instance.UTAN_FORK.scale = player.localScale;
			Services.Scenes.Swap<UtanForkPathSceneScript>(TransitionData.Instance);
		}
		else if (nextScene == UTAN_RADIOTOWER) 
		{
			TransitionData.Instance.UTAN_RADIOTOWER.position = player.position;
			TransitionData.Instance.UTAN_RADIOTOWER.scale = player.localScale;
			Services.Scenes.Swap<UtanRadioTowerSceneScript>(TransitionData.Instance);
		}
	}


}
