
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaRoadSceneScript : Scene<TransitionData>
{
    public Player player;
    public AudioClip clip;
    public AudioSource audioSouorce;

    private void Start()
    {
        audioSouorce = GetComponent<AudioSource>();
        clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_RoadWitchLight") as AudioClip;
    }

    internal override void OnEnter(TransitionData data)
    {
        
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 3.88f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -3.88f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0.41f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = -0.41f;

        if (!TransitionData.Instance.SENECA_ROAD.visitedScene)
        {
            GameObject.Find("witchlightRoad").GetComponent<Animator>().SetBool("ChaseMe", true);
            audioSouorce = GetComponent<AudioSource>();
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_RoadWitchLight") as AudioClip;
            if (clip != null)
            {
                audioSouorce.PlayOneShot(clip);
            }
            else
            {
                audioSouorce = GetComponent<AudioSource>();
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_RoadWitchLight") as AudioClip;
                audioSouorce.PlayOneShot(clip);
            }
        }
        else
        {
            GameObject.Find("witchlightRoad").GetComponent<Animator>().SetBool("ChaseMe", false);
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
        TransitionData.Instance.SENECA_ROAD.position = player.transform.position;
        TransitionData.Instance.SENECA_ROAD.scale = player.transform.localScale;
        TransitionData.Instance.SENECA_ROAD.visitedScene = true;
    }
}
