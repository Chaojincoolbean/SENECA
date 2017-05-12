using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaRocksSceneScript : Scene<TransitionData>
{
    public Player player;

    internal override void OnEnter(TransitionData data)
    {
        player = GameManager.instance.player_Astrid;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow2D>().xPosBoundary = 0f;

    }

    internal override void OnExit()
    {
        TransitionData.Instance.SENECA_ROCKS.position = player.transform.position;
        TransitionData.Instance.SENECA_ROCKS.scale = player.transform.localScale;
        TransitionData.Instance.SENECA_ROCKS.visitedScene = true;
    }
}
