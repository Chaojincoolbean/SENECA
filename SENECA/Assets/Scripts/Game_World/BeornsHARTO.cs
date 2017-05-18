using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;

public class BeornsHARTO : MonoBehaviour {

    public bool clipHasPlayed;
    AudioClip clip;
    AudioSource audioSource;
    public bool hasBeenClicked;
    public Collider2D myCollider;
    public Texture2D hoverCursor;
    public CursorMode cursorMode = CursorMode.Auto;

    // Use this for initialization
    void Start ()
    {
        hasBeenClicked = false;
        if (GameManager.instance.pickedUpBeornsHARTO)
        {
            Destroy(gameObject);
        }
        clipHasPlayed = false;
        audioSource = GetComponent<AudioSource>();	
	}

    void OnMouseEnter()
    {
        if (!hasBeenClicked)
        {
            hoverCursor = Resources.Load("Sprites/UI_Images/handcursor") as Texture2D;
            Cursor.SetCursor(hoverCursor, Vector2.zero, cursorMode);
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasBeenClicked = true;
        GameManager.instance.HARTOinUtan = true;
        GameManager.instance.pickedUpBeornsHARTO = true;
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        clip = Resources.Load("Audio/VO/Astrid/SCENE_2/VO_Event/PickUpHARTO") as AudioClip;
        Services.Events.Fire(new InteractableEvent(true, false, true));
        StartCoroutine(BringUpHARTO());
        audioSource.PlayOneShot(clip);
        clipHasPlayed = true;
        
    }

    void OnMouseDown()
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            GameManager.instance.HARTOinUtan = true;
            GameManager.instance.pickedUpBeornsHARTO = true;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            clip = Resources.Load("Audio/VO/Astrid/SCENE_2/VO_Event/PickUpHARTO") as AudioClip;
            Services.Events.Fire(new InteractableEvent(true, false, true));
            StartCoroutine(BringUpHARTO());
            audioSource.PlayOneShot(clip);
        }
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
