using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_prototype : MonoBehaviour {

	public List<AudioSource> notes;

	// Use this for initialization
	void Awake () 
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			notes.Add(transform.GetChild(i).GetComponent<AudioSource>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
