using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;

public class CampsiteExitVO : MonoBehaviour
{
    public static bool hasPlayedOnce = false;
    public AudioClip clip;
    public AudioSource audioSource;

    
	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Adventure") as AudioClip;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && GameManager.instance.hasPriyaSpoken && !hasPlayedOnce)
        {
            Services.Events.Fire(new DisablePlayerMovementEvent(true));
            Services.Events.Fire(new AstridTalksToHARTOEvent(true));
            
            audioSource.PlayOneShot(clip);
            hasPlayedOnce = true;
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if(!audioSource.isPlaying)
        {
            Services.Events.Fire(new DisablePlayerMovementEvent(false));
            Services.Events.Fire(new AstridTalksToHARTOEvent(false));
        }
	}
}
