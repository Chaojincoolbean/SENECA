using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaFarmSceneScript : Scene<TransitionData>
{
    public Player player;
    public AudioClip clip;
    public AudioSource audioSource;

	public string lastScene;
	public GameObject mainCamera;
	public Transform fromSenecaForestFork;
	public Transform fromSenecaHuntercamp;

	public Transform[] ToggleSortingLayerLocations;
	public SpriteRenderer[] farmLayers;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_ForkWitchLight") as AudioClip;

		mainCamera = GameObject.Find ("Main Camera");
		lastScene = mainCamera.GetComponent<GameManager> ().currentScene;
    }

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
				if (lastScene == "SenecaForestForkSceneScript") {
					result.transform.position = fromSenecaForestFork.position;
					result.transform.localScale = fromSenecaForestFork.localScale;

				}
				if (lastScene == "SenecaHunterCampSceneScript") {
					result.transform.position = fromSenecaHuntercamp.position;
					result.transform.localScale = fromSenecaHuntercamp.localScale;

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

		if (player.transform.position.y <= ToggleSortingLayerLocations [0].position.y) {
			farmLayers [2].sortingOrder = 1;
		} else {
			farmLayers [2].sortingOrder = 11;
		}
    }

    internal override void OnExit()
    {
        TransitionData.Instance.SENECA_FARM.position = player.transform.position;
        TransitionData.Instance.SENECA_FARM.scale = player.transform.localScale;
        TransitionData.Instance.SENECA_FARM.visitedScene = true;
    }
}
