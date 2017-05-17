using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaMeadowSceneSript : Scene<TransitionData>
{
	public Player player;
    public AudioClip clip;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_MeadowWitchLight") as AudioClip;

    }

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

    public float nextTimeToSearch = 0;

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject result = GameObject.FindGameObjectWithTag("Player");
            if (result != null)
            {
                player = result.GetComponent<Player>();
                player.transform.position = new Vector3(GameObject.Find("SenecaMeadowSpawnPoint").transform.position.x, GameObject.Find("SenecaMeadowSpawnPoint").transform.position.y, GameObject.Find("SenecaMeadowSpawnPoint").transform.position.z);

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
		TransitionData.Instance.SENECA_MEADOW.position = player.transform.position;
		TransitionData.Instance.SENECA_MEADOW.scale = player.transform.localScale;
		TransitionData.Instance.SENECA_MEADOW.visitedScene = true;
	}
}
