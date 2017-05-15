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

    internal override void OnEnter(TransitionData data)
	{
		player = GameManager.instance.player_Astrid;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 22.37f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = 0f;

        if (!TransitionData.Instance.SENECA_ROCKS.visitedScene)
        {
            audioSouorce = GetComponent<AudioSource>();
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_ForkWitchLight") as AudioClip;
            audioSouorce.PlayOneShot(clip);
        }

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
		TransitionData.Instance.UTAN_RADIO_TOWER.position = player.transform.position;
		TransitionData.Instance.UTAN_RADIO_TOWER.scale = player.transform.localScale;
		TransitionData.Instance.UTAN_RADIO_TOWER.visitedScene = true;
	}
}
