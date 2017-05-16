using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;

public class RecordingManager : MonoBehaviour 
{

	[Range(0.0f, 1.0f)]
	public float volume = 1.0f;
	private const string RECORDING_OBJECT = "Recording_";
	public AudioClip audioRecording;
	public AudioSource audioSource;
	private RecordingSelectedEvent.Handler onRecordingSelected;
	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
		onRecordingSelected = new RecordingSelectedEvent.Handler(OnRecordingSelected);
		Services.Events.Register<RecordingSelectedEvent>(onRecordingSelected);
	}

    private void OnDestroy()
    {
        Services.Events.Unregister<RecordingSelectedEvent>(onRecordingSelected);
    }

    void OnRecordingSelected(GameEvent e)
	{
		string recording = ((RecordingSelectedEvent)e).recording.Replace("Recording_", "");
        if (!audioSource.isPlaying)
        { 
            audioSource.PlayOneShot(LoadHARTORecording(recording), volume);
        }
		StartCoroutine(RecordingIsPlaying(LoadHARTORecording(recording).length));
	}
        

    IEnumerator RecordingIsPlaying(float recordingLength)
	{
		yield return new WaitForSeconds(recordingLength);
		Services.Events.Fire(new RecordingIsOverEvent());
	}

	public AudioClip LoadHARTORecording (string filename)
	{
		if (Resources.Load<AudioClip>("Audio/Recordings/" + filename) == null)
		{
			// Play empty audio here
			Debug.Log("Resource Not Found Error: " + "Audio/Recordings/" + filename + " not found!");
		}

		audioRecording = Resources.Load<AudioClip>("Audio/Recordings/" + filename);
		return audioRecording;
	}

	public AudioClip LoadHARTOVO(string filename)
	{
		if (Resources.Load<AudioClip>("Audio/VO/HARTO/" + filename) == null)
		{
			// Play empty audio here
			Debug.Log("Resource Not Found Error: " + "Audio/VO/HARTO/" + filename + " not found!");
		}

		audioRecording = Resources.Load<AudioClip>("Audio/VO/HARTO/" + filename);
		return audioRecording;
	}
}
