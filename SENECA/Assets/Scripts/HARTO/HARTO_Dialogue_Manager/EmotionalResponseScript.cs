using UnityEngine;

#region GameManager.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Plays lines that require emotional input. Inherits from ResponseScript.cs                                         */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*           private:                                                                                                   */
/*                 private void Start()                                                                                 */
/*                                                                                                                      */
/*           public:                                                                                                    */
/*                  public Emotions GetEmotionalInput()                                                                 */
/*                  public void PlayEmotionLine(Emotions emotion, string dialogueType, string scene, string topic)      */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class EmotionalResponseScript : ResponseScript
{
	private const string HARTO_REF = "HARTO";
	private HARTO astridHARTO;

	public VoiceOverLine[] possibleLines;

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
		base.Start();	
		astridHARTO = GameObject.FindGameObjectWithTag(HARTO_REF).GetComponent<HARTO>();
		possibleLines = GetComponentsInChildren<VoiceOverLine>();
	}

    #region Overview public Emotions GetEmotionalInput()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Getting the current emotion selected                                                                            */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Enum Emotion: the current selected emotion                                                                  */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public Emotions GetEmotionalInput()
	{
		return astridHARTO.CurrentEmotion;
	}

    #region Overview public void PlayEmotionLine(Emotions emotion, string dialogueType, string scene, string topic)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Playing the voice over line connected to the appropriate emotion                                                */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          Emotions emotion: The current selected emotion                                                              */
    /*          DEPRECIATED string dialogueType: Wether you shouldplay HARTO line or gibberish line                         */
    /*          string scene: The current scene                                                                             */
    /*          string topic: The selected topic                                                                            */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void PlayEmotionLine(Emotions emotion, string dialogueType, string scene, string topic)
	{		
		for (int i  = 0; i < possibleLines.Length; i++)
		{
			if (possibleLines[i].name.Contains(emotion.ToString()))
			{	
				if (dialogueType == HARTO)
				{
					characterAudioSource.PlayOneShot(possibleLines[i].LoadAudioClip(characterName, scene, topic, transform.name, emotion.ToString()), volume);
					elapsedHARTOSeconds = possibleLines[i].LoadAudioClip(characterName, scene, topic, transform.name, emotion.ToString()).length;
				}
				else if (dialogueType == GIBBERISH)
				{
					possibleLines[i].LoadGibberishAudio(characterName, scene, topic, transform.name, emotion.ToString());
					elapsedGibberishSeconds = possibleLines[i].LoadGibberishAudio(characterName, scene, topic, transform.name, emotion.ToString()).length;
				}
				
			}
		}	
	}
}
