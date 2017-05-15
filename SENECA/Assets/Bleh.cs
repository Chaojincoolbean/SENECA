using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleh : MonoBehaviour {

    public AudioClip clip;
    public AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load("Audio/VO/HARTO/BLEH!") as AudioClip;
		
	}

    void PlayBleh()
    {
        audioSource.PlayOneShot(clip);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
