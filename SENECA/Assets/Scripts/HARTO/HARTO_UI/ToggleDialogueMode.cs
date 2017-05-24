using UnityEngine;
using UnityEngine.UI;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;

#region ToggleDialogueMode.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for toggling between Dialogue and Recoding modes                                                      */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void OnDestroy()                                                                             */
/*                 private void OnBeginDialogueEvent(GameEvent e)                                                       */
/*                 private void OnEndDialogueEvent(GameEvent e)                                                         */
/*                                                                                                                      */
/*          public:                                                                                                     */
/*                 public void OnMouseEnter()                                                                           */
/*                 public void OnMouseExit()                                                                            */
/*                 public void TackOnClick()                                                                            */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class ToggleDialogueMode : MonoBehaviour 
{
	public HARTO_UI_Interface ui;
	public Button thisButton;
	public RectTransform dialoguePosition;
	public Vector3 dialogueRotation;
	public RectTransform recordingPosition;
	public Vector3 recordingRotation;
    public Texture2D hoverCursor;
    public CursorMode cursorMode = CursorMode.Auto;

    private BeginDialogueEvent.Handler onBeginDialogueEvent;
	private EndDialogueEvent.Handler onEndDialogueEvent;

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
		ui = GameObject.Find("HARTO_UI_Interface").GetComponent<HARTO_UI_Interface>();
		thisButton = GetComponent<Button>();
		thisButton.onClick.AddListener(TaskOnClick);	
		thisButton.interactable = false;
		dialoguePosition = GetComponent<RectTransform>();
		dialogueRotation = transform.localRotation.eulerAngles;

		recordingPosition = GameObject.Find("RecordingSwitchPos").GetComponent<RectTransform>();
		recordingRotation = GameObject.Find("RecordingSwitchPos").transform.localRotation.eulerAngles;

		onBeginDialogueEvent = new BeginDialogueEvent.Handler(OnBeginDialogueEvent);
		onEndDialogueEvent = new EndDialogueEvent.Handler(OnEndDialogueEvent);

		Services.Events.Register<BeginDialogueEvent>(onBeginDialogueEvent);
		Services.Events.Register<EndDialogueEvent>(onEndDialogueEvent);
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
		Services.Events.Unregister<BeginDialogueEvent>(onBeginDialogueEvent);
		Services.Events.Unregister<EndDialogueEvent>(onEndDialogueEvent);
	}

    #region Overview private void OnBeginDialogueEvent(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Making the toggle switch inactive when in conversation                                                          */
    /*      This function is called when the EventScript.cs fires a new BeginDialogueEvent.                                 */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The event that was fired                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnBeginDialogueEvent(GameEvent e)
	{
		thisButton.interactable = false;
	}

    #region Overview private void OnEndDialogueEvent(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Making the toggle switch active when not in conversation                                                        */
    /*      This function is called when the EventScript.cs fires a new EndDialogueEvent.                                   */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The event that was fired                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnEndDialogueEvent(GameEvent e)
	{
		thisButton.interactable = true;
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
		if(thisButton == null)
		{
			ui = GameObject.Find("HARTO_UI_Interface").GetComponent<HARTO_UI_Interface>();
			thisButton = GetComponent<Button>();
			thisButton.onClick.AddListener(TaskOnClick);

			onBeginDialogueEvent = new BeginDialogueEvent.Handler(OnBeginDialogueEvent);

			Services.Events.Register<BeginDialogueEvent>(onBeginDialogueEvent);
			
		}

		if(GameManager.instance.inConversation)
		{
			thisButton.interactable = false;
		}
		else if (GameManager.instance.completedOneTopic)
		{
			thisButton.interactable = true;
		}

		if(dialoguePosition == null || recordingPosition == null)
		{
			dialoguePosition = GetComponent<RectTransform>();
			dialogueRotation = transform.localRotation.eulerAngles;
			recordingPosition = GameObject.Find("RecordingSwitchPos").GetComponent<RectTransform>();
			recordingRotation = GameObject.Find("RecordingSwitchPos").transform.localRotation.eulerAngles;
		}
		
		if(ui.dialogueModeActive)
		{
			GetComponent<RectTransform>().position = new Vector3(dialoguePosition.position.x, dialoguePosition.position.y, dialoguePosition.position.z);
			GetComponent<RectTransform>().rotation = Quaternion.Euler(dialoguePosition.rotation.x, dialoguePosition.rotation.y, dialoguePosition.rotation.z);
		}
		else
		{
			GetComponent<RectTransform>().position = new Vector3(recordingPosition.position.x, recordingPosition.position.y, recordingPosition.position.z);
			GetComponent<RectTransform>().rotation = Quaternion.Euler(recordingPosition.rotation.x, recordingPosition.rotation.y, recordingPosition.rotation.z - 20.0f);
		}
	}

    #region Overview private void OnMouseEnter()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Changing the cursor image when ontop of the toggle switch                                                       */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void OnMouseEnter()
    {
        if (thisButton.interactable)
        {
            hoverCursor = Resources.Load("Sprites/UI_Images/handcursor") as Texture2D;
            Cursor.SetCursor(hoverCursor, Vector2.zero, cursorMode);
        }
    }

    #region Overview private void OnMouseExit()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Changing the cursor image when moving off of the toggle switch                                                  */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    #region Overview public void TaskOnClick()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Toggle dialopgue mode when clicking on switch.                                                                  */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void TaskOnClick()
	{
		ui.ToggleDialogueMode();
	}
}
