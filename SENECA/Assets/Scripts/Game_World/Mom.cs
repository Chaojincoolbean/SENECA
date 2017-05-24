using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;

#region Mom.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Moving Priya at the beginning of the game and starting the Start_Game and Tutorial dialogues                      */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void OnDestroy()                                                                             */
/*                 private void OnMoveMom(GameEvent e)                                                                  */
/*                 private void OnToggleHARTO(GameEvent e)                                                              */
/*                 private void OnTriggerEnter2D(Collider2D col)                                                        */
/*                 private void Update ()                                                                               */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class Mom : MonoBehaviour 
{
	public bool beginGame;
	public bool moveMom;
	public bool tutorialBegan;
    public bool onposition;

    public float x;
	public float y;
	
	private MoveMomEvent.Handler onMoveMomEvent;
	private ToggleHARTOEvent.Handler onToggleHARTO;
	private ClosingHARTOForTheFirstTimeEvent.Handler onClosingHARTOForTheFirstTime;

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
		beginGame = false;
		gameObject.name = "Priya";
		moveMom = false;
		tutorialBegan = false;
		x = -10f;
		onposition = false;

		onMoveMomEvent = new MoveMomEvent.Handler(OnMoveMom);
		onToggleHARTO = new ToggleHARTOEvent.Handler(OnToggleHARTO);
		onClosingHARTOForTheFirstTime = new ClosingHARTOForTheFirstTimeEvent.Handler(OnClosingHARTOForTheFirstTime);

		Services.Events.Register<MoveMomEvent>(onMoveMomEvent);
		Services.Events.Register<ToggleHARTOEvent>(onToggleHARTO);
		Services.Events.Register<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
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
        Services.Events.Unregister<MoveMomEvent>(onMoveMomEvent);
        Services.Events.Unregister<ToggleHARTOEvent>(onToggleHARTO);
        Services.Events.Unregister<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
    }

    private void OnClosingHARTOForTheFirstTime(GameEvent e)
	{
		Collider2D[] colliders = GetComponents<Collider2D>();
		for(int i = 0;i < colliders.Length; i++)
		{
			if(colliders[i].isTrigger)
			{
				colliders[i].enabled = false;
			}
		}
	}

    #region Overview private void OnToggleHARTO(GameEvent e)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Starts moving Priya                                                                                         */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnMoveMom(GameEvent e)
	{
		moveMom = true;
	}

    #region Overview private void OnToggleHARTO(GameEvent e)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Beginning the Tutorial dialogue                                                                             */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnToggleHARTO(GameEvent e)
	{
		if (!tutorialBegan && !GameManager.instance.isTestScene)
		{
            GameManager.instance.hasPriyaSpoken = true;
			tutorialBegan = true;
			Services.Events.Fire(new BeginTutorialEvent());	
		}
	}

    #region Overview private void OnTriggerEnter2D(Collider2D collider)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Stop moving Priya and starting the Start_Game dialogue                        					            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          Collider2D collider: the object you collided with                                                           */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Marker" && !beginGame)
        {
            beginGame = true;
            Services.Events.Fire(new BeginGameEvent());
            SenecaCampsiteSceneScript.hasPriyaSpoken = true;
            onposition = true;
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
    private void Update () {

		//	Mom waits until HARTO finishes talking
		if (onposition == false && moveMom) 
		{
			x = x + 0.05f;

			if (y > 1f)
            {
				y -= 0.01f;
			}
            else if(y > -1f)
            {
				y += 0.01f;
			}

			this.gameObject.transform.position = new Vector3 (x, -4f, 0);
			this.gameObject.transform.position += new Vector3 (0f, y, 0f);
		}

		if (x < -20f)
        {
			Destroy (this);
		}
	}
}
