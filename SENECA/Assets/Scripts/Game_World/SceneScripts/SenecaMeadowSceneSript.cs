using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaMeadowSceneSript : Scene<TransitionData>
{
	Player player;

	void OnEnter(TransitionData data)
	{
		player = GameManager.instance.player_Astrid;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0f;

	}

	void OnExit(TransitionData data)
	{
		TransitionData.Instance.SENECA_MEADOW.position = player.transform.position;
		TransitionData.Instance.SENECA_MEADOW.scale = player.transform.localScale;
		TransitionData.Instance.SENECA_MEADOW.visitedScene = true;
	}
}
