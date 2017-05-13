using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class UtanForkPathSceneScript : Scene<TransitionData> 
{
	public Player player;

	internal override void OnEnter(TransitionData data)
	{
		player = GameManager.instance.player_Astrid;
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
