using UnityEngine;

#region VoiceOverLine.cs Overview
/*********************************************************************************************************************************************************/
/*                                                                                                                                                       */
/*    NAvigate the file paths to find the audio clips                                                                                                    */
/*                                                                                                                                                       */
/*    Function List as of 5/20/2017:                                                                                                                     */
/*          private:                                                                                                                                     */
/*                 private void Start ()                                                                                                                 */
/*                 private void FindBrocaParticles()                                                                                                     */
/*                 private void Update()                                                                                                                 */
/*                                                                                                                                                       */
/*           public:                                                                                                                                     */
/*                 public AudioClip LoadGibberishAudio (string characterName, string scene, string topic, string filename, string emotionalResponse)     */
/*                 public AudioClip LoadAudioClip(string characterName, string scene, string topic, string filename, string emotionalResponse)           */
/*                 public AudioClip LoadGibberishAudio (string characterName, string scene, string topic, string filename)                               */
/*                 public AudioClip LoadAudioClip(string characterName, string scene, string topic, string filename)                                     */
/*                                                                                                                                                       */
/*********************************************************************************************************************************************************/
#endregion
public class VoiceOverLine : MonoBehaviour 
{
	public float nextTimeToSearch = 0;				            //	How long unitl the camera searches for the target again
	public string voiceOverLine = "";
	public const string GIBBERISH = "Gibberish";
	public const string BROCA_PARTICLES = "BrocaParticles";
	public AudioClip voiceOverGibberish;
	public AudioClip voiceOverHARTO;
	public BufferShuffler gibberishGenerator;

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
    private void Start () 
	{
		gibberishGenerator = GameObject.Find(BROCA_PARTICLES).GetComponent<BufferShuffler>();
	}

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
				gibberishGenerator = result.GetComponent<BufferShuffler>();
			}
			nextTimeToSearch = Time.time + 2.0f;
		}
	}

    #region Overview public AudioClip LoadGibberishAudio (string characterName, string scene, string topic, string filename, string emotionalResponse)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Find the correct audio clip by navigating the file hierarchy                                                */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          string characterName: Name of the character talking                                                         */
    /*          string scene: the current scene                                                                             */
    /*          string topic: the conversation topic                                                                        */
    /*          string filename: The name of the file                                                                       */
    /*          string emotionalResponse: the selecetd emotion                                                              */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The selected audio file as an AudioClip                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public AudioClip LoadGibberishAudio (string characterName, string scene, string topic, string filename, string emotionalResponse)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse) == null)
		{
			// Play empty audio here
			Debug.Log("Resource Not Found Error4: " + "Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse + " not found!");
		}

		voiceOverGibberish = Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse);
		gibberishGenerator.ClipToShuffle = voiceOverGibberish;
		return gibberishGenerator.ClipToShuffle;
	}

    #region Overview public AudioClip LoadAudioClip (string characterName, string scene, string topic, string filename, string emotionalResponse)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Find the correct audio clip by navigating the file hierarchy                                                */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          string characterName: Name of the character talking                                                         */
    /*          string scene: the current scene                                                                             */
    /*          string topic: the conversation topic                                                                        */
    /*          string filename: The name of the file                                                                       */
    /*          string emotionalResponse: the selecetd emotion                                                              */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The selected audio file as an AudioClip                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public AudioClip LoadAudioClip(string characterName, string scene, string topic, string filename, string emotionalResponse)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse) == null)
		{
			Debug.Log("Resource Not Found Error3: " + "Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse + " not found!");
		}
		
		voiceOverHARTO = Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + "_" + emotionalResponse);

		return voiceOverHARTO;
	}

    #region Overview public AudioClip LoadGibberishAudio (string characterName, string scene, string topic, string filename)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Find the correct audio clip by navigating the file hierarchy                                                */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          string characterName: Name of the character talking                                                         */
    /*          string scene: the current scene                                                                             */
    /*          string topic: the conversation topic                                                                        */
    /*          string filename: The name of the file                                                                       */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The selected audio file as an AudioClip                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public AudioClip LoadGibberishAudio (string characterName, string scene, string topic, string filename)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename) == null)
		{
			Debug.Log("Resource Not Found Error2: " + "Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + " not found!");
		}

		voiceOverGibberish = Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename);
		gibberishGenerator.ClipToShuffle = voiceOverGibberish;
		return gibberishGenerator.ClipToShuffle;
	}

    #region Overview public AudioClip LoadAudioClip (string characterName, string scene, string topic, string filename)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Find the correct audio clip by navigating the file hierarchy                                                */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          string characterName: Name of the character talking                                                         */
    /*          string scene: the current scene                                                                             */
    /*          string topic: the conversation topic                                                                        */
    /*          string filename: The name of the file                                                                       */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The selected audio file as an AudioClip                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public AudioClip LoadAudioClip(string characterName, string scene, string topic, string filename)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename) == null)
		{
			Debug.Log("Resource Not Found Error1: " + "Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename + " not found!");
		}

		voiceOverHARTO = Resources.Load<AudioClip>("Audio/VO/" + characterName + "/" + scene + "/" + topic + "/" + filename);
		return voiceOverHARTO;
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
        if (gibberishGenerator == null)
        {
            FindBrocaParticles();
            return;
        }
    }
}
