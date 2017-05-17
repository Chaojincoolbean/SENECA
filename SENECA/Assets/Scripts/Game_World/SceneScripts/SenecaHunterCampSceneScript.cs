using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaHunterCampSceneScript : Scene<TransitionData>
{
    public Player player;
    public AudioClip clip;
    public AudioSource audioSource;

	public string lastScene;
	public GameObject mainCamera;
	public Transform fromSenecaRoad;
	public Transform fromSenecaFarm;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

		mainCamera = GameObject.Find ("Main Camera");
		lastScene = mainCamera.GetComponent<GameManager> ().currentScene;

    }

    internal override void OnEnter(TransitionData data)
    {
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0.35f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = -0.35f;
    }

    public float nextTimeToSearch = 0;

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject result = GameObject.FindGameObjectWithTag("Player");
            if (result != null)
            {

				if (lastScene == "SenecaRoadSceneScript") {
					result.transform.position = fromSenecaRoad.position;
					result.transform.localScale = fromSenecaRoad.localScale;

				}
				if (lastScene == "SenecaFarmSceneScript") {
					result.transform.position = fromSenecaFarm.position;
					result.transform.localScale = fromSenecaFarm.localScale;

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
        TransitionData.Instance.SENECA_HUNTER_CAMP.position = player.transform.position;
        TransitionData.Instance.SENECA_HUNTER_CAMP.scale = player.transform.localScale;
        TransitionData.Instance.SENECA_HUNTER_CAMP.visitedScene = true;
    }
}
