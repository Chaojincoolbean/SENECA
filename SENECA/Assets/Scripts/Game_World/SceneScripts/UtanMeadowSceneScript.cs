using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class UtanMeadowSceneScript : Scene<TransitionData> 
{
	Player player;

	internal override void OnEnter(TransitionData data)
	{
		player = GameManager.instance.player_Astrid;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0f;

	}

	internal override void OnExit()
	{
		TransitionData.Instance.UTAN_MEADOW.position = player.transform.position;
		TransitionData.Instance.UTAN_MEADOW.scale = player.transform.localScale;
		TransitionData.Instance.UTAN_MEADOW.visitedScene = true;
	}
}
