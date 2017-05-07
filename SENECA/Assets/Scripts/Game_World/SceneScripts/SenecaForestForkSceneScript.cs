using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaForestForkSceneScript : Scene<TransitionData> 
{
	Player player;

	internal override void OnEnter(TransitionData data)
	{
		player = GameManager.instance.player_Astrid;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0f;
	}

	internal override void OnExit()
	{
		TransitionData.Instance.SENECA_FORK.position = player.transform.position;
		TransitionData.Instance.SENECA_FORK.scale = player.transform.localScale;
		TransitionData.Instance.SENECA_FORK.visitedScene = true;
	}
}
