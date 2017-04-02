using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour 
{
	public BufferShuffler shuffler;

	public bool confirm;

	// Use this for initialization
	void Start ()
	{
		shuffler = GetComponent<BufferShuffler>();
		shuffler.SetSecondsPerShuffle (0.15f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!confirm && !shuffler.soundIsPlaying ())
		{
			StartCoroutine (shuffler.PlaySound ());
		}
	}

}
