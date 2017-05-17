using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class UtanRoadSceneScript : Scene<TransitionData> 
{
	Player player;

	public string lastScene;
	public GameObject mainCamera;
	public Transform fromUtanRadiotower;
	public Transform fromUtanHuntercamp;
	public Transform fromUtanRocks;

	private void Start()
	{

		mainCamera = GameObject.Find ("Main Camera");
		lastScene = mainCamera.GetComponent<GameManager> ().currentScene;

	}

	internal override void OnEnter(TransitionData data)
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 6.16f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = 0f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = 0f;

	}

    public float nextTimeToSearch = 0;

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject result = GameObject.FindGameObjectWithTag("Player");
            if (result != null)
            {

				if (lastScene == "UtanRadioTowerSceneScript") {
					result.transform.position = fromUtanRadiotower.position;
					result.transform.localScale = fromUtanRadiotower.localScale;

				}
				if (lastScene == "UtanHunterCampSceneScript") {
					result.transform.position = fromUtanHuntercamp.position;
					result.transform.localScale = fromUtanHuntercamp.localScale;

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
		TransitionData.Instance.UTAN_ROAD.position = player.transform.position;
		TransitionData.Instance.UTAN_ROAD.scale = player.transform.localScale;
		TransitionData.Instance.UTAN_ROAD.visitedScene = true;
	}
}
