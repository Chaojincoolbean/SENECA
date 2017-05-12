using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaHunterCampSceneScript : Scene<TransitionData>
{
    public Player player;

    internal override void OnEnter(TransitionData data)
    {
        player = GameManager.instance.player_Astrid;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow2D>().xPosBoundary = 0f;
    }

    internal override void OnExit()
    {
        TransitionData.Instance.SENECA_HUNTER_CAMP.position = player.transform.position;
        TransitionData.Instance.SENECA_HUNTER_CAMP.scale = player.transform.localScale;
        TransitionData.Instance.SENECA_HUNTER_CAMP.visitedScene = true;
    }
}
