using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour 
{
	public Collider2D myCollider;
    public Texture2D hoverCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public AudioSource myAudioSource;
	public AudioClip clip;
	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent<AudioSource> ();
        myCollider = GetComponent<Collider2D>();
		
	}

    void OnMouseEnter()
    {
        hoverCursor = Resources.Load("Sprites/UI_Images/handcursor") as Texture2D;
        Cursor.SetCursor(hoverCursor, Vector2.zero, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    void OnMouseDown()
	{
		if (transform.name == "Priya"&& GameManager.instance.tutorialIsDone) 
		{
            //  done
            clip = Resources.Load("Audio/VO/Priya/SCENE_1/VO_EVENT/Priya_Hurry") as AudioClip;
		}   
        else if(tag == "Rocks")
        {
            // done
            Services.Events.Fire(new InteractableEvent(true, true, true));
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Rocks") as AudioClip;
        }
        else if (tag == "Radio")
        {
            //  done
            Services.Events.Fire(new InteractableEvent(true, true, true));
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Radio") as AudioClip;
        }
        else if (tag == "Sign")
        {
            // done
            Services.Events.Fire(new InteractableEvent(true, true, true));
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Sign") as AudioClip;
        }
        else if (tag == "Racks")
        {
            //  done
            Services.Events.Fire(new InteractableEvent(true, true, true));
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Rack") as AudioClip;
        }
        else if (tag == "Fence")
        {
            //  done
            Services.Events.Fire(new InteractableEvent(true, true, true));
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Fence") as AudioClip;
        }
        else if (tag == "Carving")
        {
            // done
            Services.Events.Fire(new InteractableEvent(true, true, true));
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Carving") as AudioClip;
        }
        else if (tag == "Backpack" && GameManager.instance.tutorialIsDone)
        {
            //  done
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Backpack") as AudioClip;
        }
 
            myAudioSource.PlayOneShot(clip);
    }

    private void Update()
    {
        if(!myAudioSource.isPlaying && GameManager.instance.tutorialIsDone && !GameManager.instance.HARTOinUtan)
        {
            Services.Events.Fire(new InteractableEvent(false, false, false));

        }
    }

}
