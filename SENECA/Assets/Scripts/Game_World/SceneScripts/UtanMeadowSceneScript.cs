using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class UtanMeadowSceneScript : Scene<TransitionData> 
{
	public Player player;
	public float nextTimeToSearch = 0;	

	internal override void OnEnter(TransitionData data)
	{

		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0f;

	}

	void FindPlayer()
	{
		if (nextTimeToSearch <= Time.time)
		{
			GameObject result = GameObject.FindGameObjectWithTag ("Player");
			if (result != null)
			{
				player = result.GetComponent<Player>();
			}
			nextTimeToSearch = Time.time + 2.0f;
		}
	}


	void Update()
	{

		if(player == null)
		{
			FindPlayer();
			return;
		}
		player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -1);
	}

	internal override void OnExit()
	{
		TransitionData.Instance.UTAN_MEADOW.position = player.transform.position;
		TransitionData.Instance.UTAN_MEADOW.scale = player.transform.localScale;
		TransitionData.Instance.UTAN_MEADOW.visitedScene = true;
	}
}
