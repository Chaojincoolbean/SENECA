using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsCamera;
using ChrsUtils.ChrsEventSystem.GameEvents;
using SenecaEvents;

#region SenecaCampsiteSceneScript.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    This is the Scene script attached to the SenecaCampsite screen.                                                   */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          internal:                                                                                                   */
/*                 internal override void OnEnter(TransitionData data)                                                  */
/*                 internal override void OnExit()                                                                      */
/*                                                                                                                      */
/*           private:                                                                                                   */
/*                 private void Start()                                                                                 */
/*                 private void OnDestroy()                                                                             */
/*                 private void OnTABUIButtonAppear(GameEvent e)                                                        */
/*                 private void OnWASDUIAppear(GameEvent e)                                                             */
/*                 private void OnToggleHARTO(GameEvent e)                                                              */
/*                 private void FindPlayer()                                                                            */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/*           public:                                                                                                    */
/*                  public static void MakeTabAppear()                                                                  */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class SenecaCampsiteSceneScript : Scene<TransitionData> 
{
    public static bool hasPriyaSpoken;
    public static bool tabUIOnScreen;
    public static bool wasdUIOnScreen;

    public static GameObject uiTAB;
    public static GameObject tab;
    public static GameObject uiWASD;
    public static GameObject wasd;

    public bool startedGame;                                   
	public bool begin;
	public bool inConversation;
	public bool waitingForInput;
	public bool completedOneTopic;

	public float nextTimeToSearch = 0;                              //  How long unitl I search for the player again
    public string lastScene;                                        //  The last scene I was in

    public Player player;                                           //  Reference to the player
    public Transform fromSenecaForestFork;                          //  Spawn position when coming from Seneca Forest Fork
    public Transform fromPrologue;                                  //  Spawn position when coming from the prologue
    public AudioSource audioSource;

    public Transform[] ToggleSortingLayerLocations;                 //  Sorting layyers for rendering purposes
    public SpriteRenderer[] campsiteLayers;                         //  Campsite's sorting layer orders
 
	public GameObject uiMouse;
	public GameObject npc_Priya;
    public GameObject mainCamera;                                   //  Reference to Main Camera 
   
	private TABUIButtonAppearEvent.Handler onTABUIButtonAppear;     // Delegate for TABUIButtonAppear Event
	private ToggleHARTOEvent.Handler onToggleHARTO;                 // Delegate for ToggleHARTO Event
    private WASDUIAppearEvent.Handler onWASDUIAppear;               // Delegate for WASDUIAppear Event

    #region Overview public void Start()
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
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

		mainCamera = GameObject.Find ("Main Camera");
		lastScene = mainCamera.GetComponent<GameManager> ().currentScene;
    }

    #region Overview internal override void OnEnter(TransitionData data)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Running when entering a scene					                                                            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          TradnsitionData data: A class with structs that represent data stored between each scene.                   */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    internal override void OnEnter(TransitionData data)
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xPosBoundary = 0.67f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().xNegBoundary = -0.72f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yPosBoundary = 0.43f;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow2D> ().yNegBoundary = -0.39f;

		startedGame = false;
		audioSource = GetComponent<AudioSource>();

		uiTAB = Resources.Load ("Prefabs/HARTO/UI/TAB_UI") as GameObject;
		uiMouse = Resources.Load ("Prefabs/HARTO/UI/MOUSE_UI") as GameObject;

		uiWASD = Resources.Load ("Prefabs/HARTO/UI/WalkUI") as GameObject;


		audioSource = GetComponent<AudioSource>();

		if (!TransitionData.Instance.SENECA_CAMPSITE.visitedScene) 
		{
            //  Sets up delegates for events
            onTABUIButtonAppear = new TABUIButtonAppearEvent.Handler (OnTABUIButtonAppear);
			onToggleHARTO = new ToggleHARTOEvent.Handler (OnToggleHARTO);
			onWASDUIAppear = new WASDUIAppearEvent.Handler (OnWASDUIAppear);

			uiTAB = Resources.Load ("Prefabs/HARTO/UI/TAB_UI") as GameObject;
			uiTAB.name = "TAB_UI";
			uiMouse = Resources.Load ("Prefabs/HARTO/UI/MOUSE_UI") as GameObject;
			uiWASD = Resources.Load ("Prefabs/HARTO/UI/WalkUI") as GameObject;

			begin = true;
			npc_Priya = Instantiate(Resources.Load("Prefabs/Characters/Priya", typeof(GameObject))) as GameObject;
			npc_Priya.transform.parent = transform;
			npc_Priya.gameObject.transform.position = new Vector3 (-13f, -3.5f, 0);
			startedGame = true;
			audioSource.PlayOneShot(GameManager.instance.recordingManager.LoadHARTOVO("HARTO_VO_Begin"));
            GameManager.instance.HARTOIsTalking = true;
            
            begin = false;

            //  Registers delegates for events
            Services.Events.Register<ToggleHARTOEvent> (onToggleHARTO);
			Services.Events.Register<TABUIButtonAppearEvent> (onTABUIButtonAppear);
			Services.Events.Register<WASDUIAppearEvent> (onWASDUIAppear);

		} 
		else
		{
            FindPlayer();
		}
			
	}

    #region Overview private void OnDestroy()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Running once before GameObject is destroyed					                                                */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnDestroy()
    {
        if (!TransitionData.Instance.SENECA_CAMPSITE.visitedScene)
        {
            Services.Events.Unregister<ToggleHARTOEvent>(onToggleHARTO);
            Services.Events.Unregister<TABUIButtonAppearEvent>(onTABUIButtonAppear);
			Services.Events.Unregister<WASDUIAppearEvent> (onWASDUIAppear);
        }
    }

    #region Overview public static void MakeTabAppear()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Making the TAB UI icon appear               				                                                */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public static void MakeTabAppear()
	{
		if (!tabUIOnScreen)
		{
			tabUIOnScreen = true;
			Vector3 tabPosition = GameObject.Find("TAB_Button_Location").transform.localPosition;
			tab = Instantiate(uiTAB, tabPosition, Quaternion.identity);
			tab.transform.SetParent(GameObject.Find("HARTOCanvas").transform, false);
		}
	}

    #region Overview private void OnTABUIButtonAppear(GameEvent e)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Depreciated. Makes TABUI Button appear               				                                        */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnTABUIButtonAppear(GameEvent e)
	{
		if (!tabUIOnScreen)
		{
            GameManager.instance.hasPriyaSpoken = true;
            tabUIOnScreen = true;
			Vector3 tabPosition = GameObject.Find("TAB_Button_Location").transform.localPosition;
			GameObject tab = Instantiate(uiTAB, tabPosition, Quaternion.identity);
			tab.transform.SetParent(GameObject.Find("HARTOCanvas").transform, false);
		}
	}

    #region Overview private void OnWASDUIAppear(GameEvent e)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Makes WASD UI appear                                   				                                        */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          GameEvent e: The Event that called this delegate                                                            */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnWASDUIAppear(GameEvent e){
		if (!wasdUIOnScreen) {
			wasdUIOnScreen = true;
			Vector3 wasdPosition = GameObject.Find("TAB_Button_Location").transform.localPosition;
			GameObject wasd = Instantiate (uiWASD, wasdPosition, Quaternion.identity);
			wasd.transform.SetParent (GameObject.Find ("HARTOCanvas").transform, false);
		}
	}

    #region Overview private void OnToggleHARTO(GameEvent e)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Removing the TAB UI Button when turning on HARTO for the first time                                         */
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
		if(tabUIOnScreen)
		{
			Destroy(GameObject.Find("TAB_UI(Clone)"));
		}
	}

    #region Overview private void FindPlayer()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Finding the player if the player reference is null                                 				            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void FindPlayer()
	{
		if (nextTimeToSearch <= Time.time)
		{
			GameObject result = GameObject.FindGameObjectWithTag ("Player");
			if (result != null)
			{

				if (lastScene == "SenecaForestForkSceneScript") {
					result.transform.position = fromSenecaForestFork.position;
					result.transform.localScale = fromSenecaForestFork.localScale;

				}
				if (lastScene == "PrologueSceneScript") {
					result.transform.position = fromPrologue.position;
					result.transform.localScale = fromPrologue.localScale;

				}
				player = result.GetComponent<Player>();
                
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

        if (player == null)
        {
            FindPlayer();
            return;
        }

		//SKIP ZACH'S LINES
		if (GameManager.instance.cheatSpace) {
			if (audioSource.isPlaying && Input.GetKeyUp (KeyCode.Space)) {
				audioSource.Stop ();
			}
		}

        if (GameManager.instance.HARTOIsTalking)
        {
            Services.Events.Fire(new InteractableEvent(false, true, true));
        }

        if (!begin && !audioSource.isPlaying && !TransitionData.Instance.SENECA_CAMPSITE.visitedScene)
		{
            GameManager.instance.HARTOIsTalking = false;
            Services.Events.Fire(new InteractableEvent(false, false, false));
            Services.Events.Fire(new MoveMomEvent());
			begin = true;
		}

		if (player.transform.position.y <= ToggleSortingLayerLocations [0].position.y) {
			campsiteLayers [4].sortingOrder = 9;
		} else {
			campsiteLayers [4].sortingOrder = 11;
		}
	}

    #region Overview internal override void OnExit()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Running when exiting a scene					                                                            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*           None                                                                                                       */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    internal override void OnExit()
	{
		TransitionData.Instance.SENECA_CAMPSITE.visitedScene = true;
	}
}
