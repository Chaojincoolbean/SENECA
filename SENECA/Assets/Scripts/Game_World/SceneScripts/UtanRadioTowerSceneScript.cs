using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class UtanRadioTowerSceneScript : Scene<TransitionData> 
{
	public Player player;
    public AudioClip clip;
    public AudioSource audioSouorce;

	public string lastScene;
	public GameObject mainCamera;
	public Transform fromUtanMeadow;
	public Transform fromUtanRoad;


	private void Start()
	{

		mainCamera = GameObject.Find ("Main Camera");
		lastScene = mainCamera.GetComponent<GameManager> ().currentScene;

	}


    internal override void OnEnter(TransitionData data)
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 22.37f;
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

				if (lastScene == "UtanMeadowSceneScript") {
					result.transform.position = fromUtanMeadow.position;
					result.transform.localScale = fromUtanMeadow.localScale;

				}
				if (lastScene == "UtanRoadSceneScript") {
					result.transform.position = fromUtanRoad.position;
					result.transform.localScale = fromUtanRoad.localScale;

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
		FindPlayer(); 
		TransitionData.Instance.UTAN_RADIO_TOWER.position = player.transform.position;
		TransitionData.Instance.UTAN_RADIO_TOWER.scale = player.transform.localScale;
		TransitionData.Instance.UTAN_RADIO_TOWER.visitedScene = true;
	}
}
