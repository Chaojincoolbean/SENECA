using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaForestForkSceneScript : Scene<TransitionData> 
{
	public Player player;
    public float nextTimeToSearch = 0;
    public AudioClip clip;
    public AudioSource audioSource;
	public GameObject Exit;

	public string lastScene;
	public GameObject mainCamera;
	public Transform fromSenecaCampsite;
	public Transform fromSenecaFarm;
	public Transform fromSenecaRocks;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_ForkWitchLight") as AudioClip;

		mainCamera = GameObject.Find ("Main Camera");
		lastScene = mainCamera.GetComponent<GameManager> ().currentScene;

    }

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

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject result = GameObject.FindGameObjectWithTag("Player");

            if (result != null)
            {
				if (lastScene == "SenecaCampsiteSceneScript") {
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

    private void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }
    }

    internal override void OnExit()
	{
		//TransitionData.Instance.SENECA_FORK.position = player.transform.position;
		//TransitionData.Instance.SENECA_FORK.scale = player.transform.localScale;
		TransitionData.Instance.SENECA_FORK.visitedScene = true;
	}
}
