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
		clip = Resources.Load ("Audio/VO/Priya/SCENE_1/VO_EVENT/Priya_Hurry") as AudioClip;
	}

	void OnMouseDown()
	{
		if (transform.name == "Priya" && !myAudioSource.isPlaying) 
		{
			//myAudioSource.PlayOneShot (clip);
		}
	}
	
}
