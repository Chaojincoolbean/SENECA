using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaFarmSceneScript : Scene<TransitionData>
{
    public Player player;

    internal override void OnEnter(TransitionData data)
    {
        player = GameManager.instance.player_Astrid;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow2D>().xPosBoundary = 0f;

    }

    internal override void OnExit()
    {
        TransitionData.Instance.SENECA_FARM.position = player.transform.position;
        TransitionData.Instance.SENECA_FARM.scale = player.transform.localScale;
        TransitionData.Instance.SENECA_FARM.visitedScene = true;
    }
}
