using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour 
{
	public Collider2D myCollider;

	public AudioSource myAudioSource;
	public AudioClip clip;
	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent<AudioSource> ();
        myCollider = GetComponent<Collider2D>();
		
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
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Rocks") as AudioClip;
        }
        else if (tag == "Radio")
        {
            //  done
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Radio") as AudioClip;
        }
        else if (tag == "Sign")
        {
            // done
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Sign") as AudioClip;
        }
        else if (tag == "Racks")
        {
            //  done
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Rack") as AudioClip;
        }
        else if (tag == "Fence")
        {
            //  done
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Fence") as AudioClip;
        }
        else if (tag == "Carving")
        {
            // done
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Carving") as AudioClip;
        }
        else if (tag == "Backpack" && GameManager.instance.tutorialIsDone)
        {
            //  done
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Backpack") as AudioClip;
        }

 
            myAudioSource.PlayOneShot(clip);

    }
	
}
