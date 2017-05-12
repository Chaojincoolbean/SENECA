using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour 
{
	public Collider myCollider;

	public AudioSource myAudioSource;
	public AudioClip clip;
	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent<AudioSource> ();
		
	}

	void OnMouseDown()
	{
		if (transform.name == "Priya" && !myAudioSource.isPlaying) 
		{
            clip = Resources.Load("Audio/VO/Priya/SCENE_1/VO_EVENT/Priya_Hurry") as AudioClip;
            myAudioSource.PlayOneShot (clip);
		}   
        else if(tag == "Rocks")
        {
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Rocks") as AudioClip;
        }
        else if (tag == "Radio")
        {
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Radio") as AudioClip;
        }
        else if (tag == "Rocks")
        {
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Rocks") as AudioClip;
        }
        else if (tag == "Sign")
        {
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Sign") as AudioClip;
        }
        else if (tag == "Racks")
        {
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Racks") as AudioClip;
        }
        else if (tag == "Fence")
        {
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Fence") as AudioClip;
        }
        else if (tag == "Carving")
        {
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Carving") as AudioClip;
        }
        else if (tag == "Backpack")
        {
            clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Backpack") as AudioClip;
        }
    }
	
}
