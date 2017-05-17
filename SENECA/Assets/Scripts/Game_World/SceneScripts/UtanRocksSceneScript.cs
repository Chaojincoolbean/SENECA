using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class UtanRocksSceneScript : Scene<TransitionData>
{
    public Player player;

	public string lastScene;
	public GameObject mainCamera;
	public Transform fromUtanRoad;
	public Transform fromUtanForestFork;

	private void Start()
	{

		mainCamera = GameObject.Find ("Main Camera");
		lastScene = mainCamera.GetComponent<GameManager> ().currentScene;

	}

    internal override void OnEnter(TransitionData data)
    {
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0.41f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = -0.41f;

    }

    public float nextTimeToSearch = 0;

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject result = GameObject.FindGameObjectWithTag("Player");
            if (result != null)
            {

				if (lastScene == "UtanRoadSceneScript") {
					result.transform.position = fromUtanRoad.position;
					result.transform.localScale = fromUtanRoad.localScale;

				}
				if (lastScene == "UtanForestForkSceneScript") {
					result.transform.position = fromUtanForestFork.position;
					result.transform.localScale = fromUtanForestFork.localScale;
				}

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
        TransitionData.Instance.UTAN_ROCKS.position = player.transform.position;
        TransitionData.Instance.UTAN_ROCKS.scale = player.transform.localScale;
        TransitionData.Instance.UTAN_ROCKS.visitedScene = true;
    }
}
