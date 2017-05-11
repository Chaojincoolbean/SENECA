using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class UtanRadioTowerSceneScript : Scene<TransitionData> 
{
	Player player;

	internal override void OnEnter(TransitionData data)
	{
		player = GameManager.instance.player_Astrid;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0f;

	}

	internal override void OnExit()
	{
		TransitionData.Instance.UTAN_RADIOTOWER.position = player.transform.position;
		TransitionData.Instance.UTAN_RADIOTOWER.scale = player.transform.localScale;
		TransitionData.Instance.UTAN_RADIOTOWER.visitedScene = true;
	}
}
