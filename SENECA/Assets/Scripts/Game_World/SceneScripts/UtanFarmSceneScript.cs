using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class UtanFarmSceneScript : Scene<TransitionData>
{
    public Player player;

    internal override void OnEnter(TransitionData data)
    {
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 3.93f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -3.93f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0.38f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = -0.38f;

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
        TransitionData.Instance.UTAN_FARM.position = player.transform.position;
        TransitionData.Instance.UTAN_FARM.scale = player.transform.localScale;
        TransitionData.Instance.UTAN_FARM.visitedScene = true;
    }
}
