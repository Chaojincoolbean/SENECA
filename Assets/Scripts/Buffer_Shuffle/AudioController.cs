using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {

	public BufferShuffler shuffler;
	public Slider tuner;
	public Slider audioLengthSlider;

	bool confirm;

	float currentTime;
	float sliderTime;

	// Use this for initialization
	void Start () {
		tuner.minValue = shuffler.SecondsPerCrossfade+0.1f;
		tuner.maxValue = shuffler.maxClipLength-0.1f;

		audioLengthSlider.minValue = 0f;
		audioLengthSlider.maxValue = tuner.maxValue;
		
	}
	
	// Update is called once per frame
	void Update () {
		

		if(Input.GetKeyUp(KeyCode.Space)){
			confirm = true;
		}

		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.Quit ();
		}


		sliderTime = Mathf.Repeat (Time.time, shuffler.maxClipLength);

		audioLengthSlider.value = sliderTime;
		if (audioLengthSlider.value >= shuffler.maxClipLength) {
			audioLengthSlider.value = 0f;
		}


		if (!confirm && !shuffler.soundIsPlaying ()){
			StartCoroutine (shuffler.PlaySound ());

			/*currentTime += Time.time;
			sliderTime = Time.time - currentTime;*/
			//sliderTime += Time.deltaTime;
		}


	}

	public void SetTuner(){
		shuffler.SetSecondsPerShuffle (tuner.value);
	}

}
