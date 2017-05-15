using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignAnimalSong : MonoBehaviour {

    public AudioClip clip;
    public AudioSource audioSource;
	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        if (transform.name == "Road")
        {
            clip = Resources.Load("Audio/SFX/SFX/INDIVIDUAL PUZZLE NOTES/SecondPuzzle_AnimalSong") as AudioClip;
        }
        else
        {
            clip = Resources.Load("Audio/SFX/SFX/INDIVIDUAL PUZZLE NOTES/FirstPuzzle_AnimalSong") as AudioClip;
        }

        audioSource.PlayOneShot(clip);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
