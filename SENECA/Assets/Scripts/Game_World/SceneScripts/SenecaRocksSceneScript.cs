using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaRocksSceneScript : Scene<TransitionData>
{
    public Player player;

    public AudioClip clip;
    public AudioSource audioSource;

	public string lastScene;
	public GameObject mainCamera;
	public Transform fromSenecaRoad;
	public Transform fromSenecaForestFork;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_RocksWitchLight") as AudioClip;

		mainCamera = GameObject.Find ("Main Camera");
		lastScene = mainCamera.GetComponent<GameManager> ().currentScene;
    }

    internal override void OnEnter(TransitionData data)
    {
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0.41f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = -0.41f;

        if (!TransitionData.Instance.SENECA_ROCKS.visitedScene)
        {
            GameObject.Find("witchlightRocks").GetComponent<Animator>().SetBool("ChaseMe", true);
            audioSource = GetComponent<AudioSource>();
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                audioSource = GetComponent<AudioSource>();
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_RocksWitchLight") as AudioClip;
                audioSource.PlayOneShot(clip);
            }
        }
        else
        {
            GameObject.Find("witchlightRocks").GetComponent<Animator>().SetBool("ChaseMe", false);
        }

    }

    public float nextTimeToSearch = 0;

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject result = GameObject.FindGameObjectWithTag("Player");
            if (result != null)
            {
				if (lastScene == "SenecaRoadSceneScript") {
					result.transform.position = fromSenecaRoad.position;
					result.transform.localScale = fromSenecaRoad.localScale;

				}
				if (lastScene == "SenecaForestForkSceneScript") {
					result.transform.position = fromSenecaForestFork.position;
					result.transform.localScale = fromSenecaForestFork.localScale;
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
        TransitionData.Instance.SENECA_ROCKS.position = player.transform.position;
        TransitionData.Instance.SENECA_ROCKS.scale = player.transform.localScale;
        TransitionData.Instance.SENECA_ROCKS.visitedScene = true;
    }
}
