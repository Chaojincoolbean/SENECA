using UnityEngine;

#region ResponseScript.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Base class for all responses. Plays the audio for each line of dialogue                                           */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void FindBrocaParticles()                                                                    */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/*          protected:                                                                                                  */
/*                 protected void Start ()                                                                              */
/*                                                                                                                      */
/*           virtual:                                                                                                   */
/*                 virtual public void PlayLine(string dialogueType, string scene, string topic)                        */
/*                 virtual public void StopLine()                                                                       */
/*                 virtual public void PlayLine(Emotions myEmotion)                                                     */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class ResponseScript : MonoBehaviour
{
	[Range(0.0f, 1.0f)]
	public float volume = 1.0f;                                 //  Volume of the specific line
	
	public float nextTimeToSearch = 0;				            //	How long unitl the camera searches for the target again

	public float elapsedHARTOSeconds;                           //  How long the audio file is
	public float elapsedGibberishSeconds;                       //  How long the audio file is in gibberish
	public VoiceOverLine myLine;                                //  The line I'm going to play

	public AudioSource characterAudioSource;
	public AudioSource gibberishAudioSource;
	public string characterName;

	protected const string HARTO = "HARTO";
	protected const string GIBBERISH = "Gibberish";
	private const string BROCA_PARTICLES = "BrocaParticles";

    #region Overview protected void Start()
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
    protected void Start () 
	{
		characterAudioSource = transform.parent.GetComponent<AudioSource>();
		gibberishAudioSource = GameObject.Find(BROCA_PARTICLES).GetComponentInParent<AudioSource>();
		characterName = transform.parent.name;
		myLine = GetComponentInChildren<VoiceOverLine>();
	}

    #region Overview virtual public void PlayLine(string dialogueType, string scene, string topic)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Playing the diaglogue line                                                                                      */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          DEPRECIATED string dialogueType: Is the line a HARTO line or a gibberish line?                              */
    /*          string scene: The current scene. Name like the file                                                         */
    /*          string topic: The current topic                                                                             */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    virtual public void PlayLine(string dialogueType, string scene, string topic)
	{
		if (dialogueType == HARTO)
		{
			characterAudioSource.PlayOneShot(myLine.LoadAudioClip(characterName, scene, topic, transform.name), volume);
			elapsedHARTOSeconds = myLine.LoadAudioClip(characterName, scene, topic,transform.name).length;
		}
	}

    #region Overview virtual public void StopLine()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Stopping the diaglogue lines to skip them                                                                       */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    virtual public void StopLine()
    {
		if (characterAudioSource.isPlaying)
        {
			characterAudioSource.Stop ();
		}
    }

    #region Overview virtual public void PlayLine(Emotions myEmotion)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Override function for emotion lines                                                                             */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*                                                                                                                      */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    virtual public void PlayLine(Emotions myEmotion) {  }

    #region Overview private void FindBrocaParticles()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Finding the the BrocaParticles if the BrocaParticles reference is null                                      */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void FindBrocaParticles()
	{
		if (nextTimeToSearch <= Time.time)
		{
			GameObject result = GameObject.FindGameObjectWithTag (BROCA_PARTICLES);
			if (result != null)
			{
				gibberishAudioSource = result.GetComponent<AudioSource>();
			}
				nextTimeToSearch = Time.time + 2.0f;
		}
	}

    #region Overview private void Update()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Running once per frame					                                                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void Update()
	{
		if(gibberishAudioSource == null)
		{
			FindBrocaParticles();
			return;
		}
	}
}
