using UnityEngine;
using ChrsUtils.ChrsEventSystem.GameEvents;
using SenecaEvents;

#region HARTO.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    The script version of the UI of the HARTO                                                                         */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void OnDestroy()                                                                             */
/*                  private void OnEmotionSelected(GameEvent e)                                                         */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public enum  Emotions
{
	None,
	Happy,
	Question,
	Depressed,
	Angry
}
public class HARTO : MonoBehaviour 
{

	public static HARTO instance;
	[SerializeField]
	private Emotions emotion;                                       //  Enum of all the emotions in the game
	public Emotions CurrentEmotion
	{
		get
		{
			return emotion;
		}
		set
		{
			emotion = value;
		}
	}

	private EmotionSelectedEvent.Handler onEmotionSelected;

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
    void Start () 
	{

		if(instance == null)
		{
			instance = this;
		}

		onEmotionSelected = new EmotionSelectedEvent.Handler(OnEmotionSelected);
		Services.Events.Register<EmotionSelectedEvent>(onEmotionSelected);
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
        Services.Events.Unregister<EmotionSelectedEvent>(onEmotionSelected);
    }

    #region Overview private void OnEmotionSelected(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Setting the emotion.                                                                                            */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The event that was fired                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnEmotionSelected(GameEvent e)
	{
		 emotion = ((EmotionSelectedEvent)e).emotion;
	}
}
