using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;

public class SenecaRadioTowerSceneScript : Scene<TransitionData>
{
    public Player player;
    public AudioClip clip;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_RadioWitchLight") as AudioClip;

    }

    internal override void OnEnter(TransitionData data)
    {
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -0.69f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 22.37f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = 0f;
        if(!TransitionData.Instance.SENECA_RADIO_TOWER.visitedScene)
        {
            audioSource = GetComponent<AudioSource>();
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_RadioWitchLight") as AudioClip;
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                audioSource = GetComponent<AudioSource>();
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_Event/Astrid_RadioWitchLight") as AudioClip;
                audioSource.PlayOneShot(clip);
            }
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
        TransitionData.Instance.SENECA_RADIO_TOWER.position = player.transform.position;
        TransitionData.Instance.SENECA_RADIO_TOWER.scale = player.transform.localScale;
        TransitionData.Instance.SENECA_RADIO_TOWER.visitedScene = true;
    }
}
