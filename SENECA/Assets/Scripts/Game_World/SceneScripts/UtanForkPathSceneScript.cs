using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class UtanForkPathSceneScript : Scene<TransitionData> 
{
	public Player player;

	public string lastScene;
	public GameObject mainCamera;
	public Transform fromUtanCampsite;
	public Transform fromUtanFarm;
	public Transform fromUtanRocks;

	private void Start()
	{

		mainCamera = GameObject.Find ("Main Camera");
		lastScene = mainCamera.GetComponent<GameManager> ().currentScene;

	}

	internal override void OnEnter(TransitionData data)
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0f;

	}

    public float nextTimeToSearch = 0;

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject result = GameObject.FindGameObjectWithTag("Player");
            if (result != null)
            {

				if (lastScene == "UtanCampsiteSceneScript") {
					result.transform.position = fromUtanCampsite.position;
					result.transform.localScale = fromUtanCampsite.localScale;

				}
				if (lastScene == "UtanFarmSceneScript") {
					result.transform.position = fromUtanFarm.position;
					result.transform.localScale = fromUtanFarm.localScale;

				}
				if (lastScene == "UtanRocksSceneScript") {
					result.transform.position =  fromUtanRocks.position;
					result.transform.localScale =  fromUtanRocks.localScale;

				}

                player = result.GetComponent<Player>();
            }
            nextTimeToSearch = Time.time + 2.0f;
        }
    }

    void Update()
    {

        if (player == null)
        {
            FindPlayer();
            return;
        }
    }

    internal override void OnExit()
	{
		TransitionData.Instance.UTAN_FORK.position = player.transform.position;
		TransitionData.Instance.UTAN_FORK.scale = player.transform.localScale;
		TransitionData.Instance.UTAN_FORK.visitedScene = true;
	}
}
