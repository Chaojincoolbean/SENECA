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
        // fix this
        if(collision.tag == "Player" && GameManager.instance.hasPriyaSpoken && !hasPlayedOnce)
        {
            GameManager.instance.playerAnimationLock = true;
            Services.Events.Fire(new InteractableEvent(true, true, true));
            
            audioSource.PlayOneShot(clip);
            hasPlayedOnce = true;
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if(!audioSource.isPlaying && hasPlayedOnce && !GameManager.instance.tutorialIsDone && !GameManager.instance.HARTOinUtan)
        {
            Services.Events.Fire(new InteractableEvent(false, false, false));
        }
	}
}
