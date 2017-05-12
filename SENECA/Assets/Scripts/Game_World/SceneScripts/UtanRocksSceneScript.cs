using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class UtanRocksSceneScript : Scene<TransitionData>
{
    public Player player;

    internal override void OnEnter(TransitionData data)
    {
        player = GameManager.instance.player_Astrid;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow2D>().xPosBoundary = 0f;

    }

    internal override void OnExit()
    {
        TransitionData.Instance.UTAN_ROCKS.position = player.transform.position;
        TransitionData.Instance.UTAN_ROCKS.scale = player.transform.localScale;
        TransitionData.Instance.UTAN_ROCKS.visitedScene = true;
    }
}
