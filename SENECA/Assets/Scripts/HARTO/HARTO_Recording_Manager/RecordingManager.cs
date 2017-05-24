using System.Collections;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;

#region RecordingManager.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*   Where to Find in Unity:                                                                                            */
/*    In the Hierarchy Tab in Unity: SenecaSystem -> HARTO-> RecordingManagerManager                                    */
/*                                                                                                                      */
/*    Recoridng Manager takes in the filename forkm the HARTO_UI_Interface Inspector to find the recoridng in           */
/*    the file hierarchy                                                                                                */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*           private:                                                                                                   */
/*                 private void Start()                                                                                 */
/*                 private void OnDestroy()                                                                             */
/*                 private void OnRecordingSelected(GameEvent e)                                                        */
/*                                                                                                                      */
/*           public:                                                                                                    */
/*                 public IEnumerator RecordingIsPlaying(float recordingLength)                                         */
/*                 public AudioClip LoadHARTORecording (string filename)                                                */
/*                 public AudioClip LoadHARTOVO(string filename)                                                        */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class RecordingManager : MonoBehaviour 
{
	[Range(0.0f, 1.0f)]
	public float volume = 1.0f;
	
	public AudioClip audioRecording;
	public AudioSource audioSource;

    private const string RECORDING_OBJECT = "Recording_";
    private RecordingSelectedEvent.Handler onRecordingSelected;

    #region Overview private void Start()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Initalizing variables. Runs once at the beginning of the program                                                */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void Start () 
	{
		audioSource = GetComponent<AudioSource>();
		onRecordingSelected = new RecordingSelectedEvent.Handler(OnRecordingSelected);
		Services.Events.Register<RecordingSelectedEvent>(onRecordingSelected);
	}

    #region Overview private void OnDestroy()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Unregistering for events when being destroyed to stop any null reference errors                                 */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnDestroy()
    {
        Services.Events.Unregister<RecordingSelectedEvent>(onRecordingSelected);
    }

    #region Overview private void OnRecordingSelected(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*       Playing the recording This function is called when the OnRecordingelected event is fired.                      */
    /*       OnRecordingelected event is fired in RadialMenu.cs in void DetermineEvent(RadialIcon icon)                     */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnRecordingSelected(GameEvent e)
	{
		string recording = ((RecordingSelectedEvent)e).recording.Replace("Recording_", "");
        if (!audioSource.isPlaying)
        { 
            audioSource.PlayOneShot(LoadHARTORecording(recording), volume);
        }
		StartCoroutine(RecordingIsPlaying(LoadHARTORecording(recording).length));
	}

    #region Overview public  IEnumerator RecordingIsPlaying(float recordingLength)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Playing each recording .                                                                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          float recordingLength: How long until the end of the firing of the REcordingIsOverEvent                     */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The type of objects to enumerate.                                                                           */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public IEnumerator RecordingIsPlaying(float recordingLength)
	{
		yield return new WaitForSeconds(recordingLength);
		Services.Events.Fire(new RecordingIsOverEvent());
	}

    #region Overview public AudioClip LoadHARTORecording (string filename)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Find the correct audio clip by navigating the file hierarchy                                                */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          string filename: The name of the file                                                                       */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The selected audio file as an AudioClip                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public AudioClip LoadHARTORecording (string filename)
	{
		if (Resources.Load<AudioClip>("Audio/Recordings/" + filename) == null)
		{
			Debug.Log("Resource Not Found Error: " + "Audio/Recordings/" + filename + " not found!");
		}

		audioRecording = Resources.Load<AudioClip>("Audio/Recordings/" + filename);
		return audioRecording;
	}

    #region Overview public AudioClip LoadHARTOVO (string filename)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Find the correct audio clip by navigating the file hierarchy for all HARTO VO Lines                         */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          string filename: The name of the file                                                                       */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The selected audio file as an AudioClip                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public AudioClip LoadHARTOVO(string filename)
	{
		if (Resources.Load<AudioClip>("Audio/VO/HARTO/" + filename) == null)
		{
			Debug.Log("Resource Not Found Error: " + "Audio/VO/HARTO/" + filename + " not found!");
		}

		audioRecording = Resources.Load<AudioClip>("Audio/VO/HARTO/" + filename);
		return audioRecording;
	}
}
