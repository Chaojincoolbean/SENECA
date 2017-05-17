using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class UtanHunterCampSceneScript : Scene<TransitionData>
{
    public Player player;

	public string lastScene;
	public GameObject mainCamera;
	public Transform fromUtanFarm;
	public Transform fromUtanRoad;

	private void Start()
	{

		mainCamera = GameObject.Find ("Main Camera");
		lastScene = mainCamera.GetComponent<GameManager> ().currentScene;

	}

    internal override void OnEnter(TransitionData data)
    {
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0.35f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = -0.35f;

    }

    public float nextTimeToSearch = 0;

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject result = GameObject.FindGameObjectWithTag("Player");
            if (result != null)
            {

				if (lastScene == "UtanFarmSceneScript") {
					result.transform.position = fromUtanFarm.position;
					result.transform.localScale = fromUtanFarm.localScale;

				}
				if (lastScene == "UtanRoadSceneScript") {
					result.transform.position = fromUtanRoad.position;
					result.transform.localScale = fromUtanRoad.localScale;

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
        TransitionData.Instance.UTAN_HUNTER_CAMP.position = player.transform.position;
        TransitionData.Instance.UTAN_HUNTER_CAMP.scale = player.transform.localScale;
        TransitionData.Instance.UTAN_HUNTER_CAMP.visitedScene = true;
    }
}
