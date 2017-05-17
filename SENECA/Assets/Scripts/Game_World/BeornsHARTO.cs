using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;

public class BeornsHARTO : MonoBehaviour {

    public bool clipHasPlayed;
    AudioClip clip;
    AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {

        if (GameManager.instance.pickedUpBeornsHARTO)
        {
            Destroy(gameObject);
        }
        clipHasPlayed = false;
        audioSource = GetComponent<AudioSource>();	
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.HARTOinUtan = true;
        GameManager.instance.pickedUpBeornsHARTO = true;
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        clip = Resources.Load("Audio/VO/Astrid/SCENE_2/VO_Event/PickUpHARTO") as AudioClip;
        Services.Events.Fire(new InteractableEvent(true, false, true));
        StartCoroutine(BringUpHARTO());
        audioSource.PlayOneShot(clip);
        clipHasPlayed = true;
        
    }

    IEnumerator BringUpHARTO()
    {
        
        yield return new WaitForSeconds(7.5f);
        Services.Events.Fire(new InteractableEvent(false, true, true));

    }

    // Update is called once per frame
    void Update ()
    {
		if(clipHasPlayed && !audioSource.isPlaying)
        {
            GameManager.instance.HARTOinUtan = false;
            Services.Events.Fire(new InteractableEvent(false, false, false));
            Destroy(gameObject);
        }
	}
}
