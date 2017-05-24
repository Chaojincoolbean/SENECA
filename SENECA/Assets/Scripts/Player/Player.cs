using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.GameEvents;

#region Player.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for player movement and animations                                                                    */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void OnDestroy()                                                                             */
/*                 private void OnInteractable(GameEvent e)                                                             */
/*                 private void OnAstridTalksToHARTO(GameEvent e)                                                       */
/*                 private void OnToggleDisableMovement(GameEvent e)                                                    */
/*                 private void OnToggleHARTO(GameEvent e)                                                              */
/*                 private void OnClosingHARTOForTheFirstTime(GameEvent e)                                              */
/*                 private void OnBeginTutorial(GameEvent e)                                                            */
/*                 private void Move(float dx, float dy)                                                                */
/*                 private void OnTriggerEnter2D(Collider2D other)                                                      */
/*                 private void FixedUpdate ()                                                                          */
/*                                                                                                                      */
/*           public:                                                                                                    */
/*                 public void PlayFootStepAudio()                                                                      */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class Player : MonoBehaviour 
{
	public const string NPC = "NPC";
	public const string HORIZONTAL_AXIS = "Horizontal";
	public const string VERICLE_AXIS = "Vertical";
	public const string WALL = "Wall";
	public const string WITCHLIGHT = "Witchlight";

	public bool disableMovement;
	public bool facingLeft;
	public string npcAstridIsTalkingTo;
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;
	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;

    public float x;
    public float y;
	public float moveSpeed;
	public Animator _animator;

	private const float MAX_SCALE = 0.7f;
	private const float MIN_SCALE = 0.2f;
	private AudioSource _audioSource;
	private AudioClip _clip;

	private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _renderer;
    private InteractableEvent.Handler onInteractable;
    private AstridTalksToHARTOEvent.Handler onAstridTalksToHARTO;
	private DisablePlayerMovementEvent.Handler onToggleDisableMovement;
    private BeginTutorialEvent.Handler onBeginTutorial;
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
		_rigidBody2D = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
		_audioSource = GetComponent<AudioSource>();

        onInteractable = new InteractableEvent.Handler(OnInteractable);
        onAstridTalksToHARTO = new AstridTalksToHARTOEvent.Handler(OnAstridTalksToHARTO);
		onToggleDisableMovement = new DisablePlayerMovementEvent.Handler(OnToggleDisableMovement);
        onBeginTutorial = new BeginTutorialEvent.Handler(OnBeginTutorial);
		onToggleHARTO = new ToggleHARTOEvent.Handler(OnToggleHARTO);
		onClosingHARTOForTheFirstTime = new ClosingHARTOForTheFirstTimeEvent.Handler(OnClosingHARTOForTheFirstTime);

        Services.Events.Register<InteractableEvent>(onInteractable);
        Services.Events.Register<AstridTalksToHARTOEvent>(onAstridTalksToHARTO);
		Services.Events.Register<DisablePlayerMovementEvent>(onToggleDisableMovement);
        Services.Events.Register<BeginTutorialEvent>(onBeginTutorial);
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
        Services.Events.Unregister<InteractableEvent>(onInteractable);
        Services.Events.Unregister<AstridTalksToHARTOEvent>(onAstridTalksToHARTO);
        Services.Events.Unregister<DisablePlayerMovementEvent>(onToggleDisableMovement);
        Services.Events.Unregister<BeginTutorialEvent>(onBeginTutorial);
        Services.Events.Unregister<ToggleHARTOEvent>(onToggleHARTO);
        Services.Events.Unregister<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
    }

    #region Overview private void OnInteractable(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*          Plays the combination of animations for when an interactable is clicked                                     */
    /*          This function is called when the event manager fires a new InteractableEvent.                               */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The event that was fired                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnInteractable(GameEvent e)
    {
        _animator.SetBool("HARTOActive", ((InteractableEvent)e).armUp);
        _animator.SetBool("IsTalking", ((InteractableEvent)e).talkingToHARTO);
        disableMovement = ((InteractableEvent)e).disableMovement;
    }

    #region Overview private void OnAstridTalksToHARTO(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*          Plays the combination of animations for when an Astrid should be talking to HARTO                           */
    /*          This function is called when the event manager fires a new AstridTalksToHARTOEvent.                         */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The event that was fired                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnAstridTalksToHARTO(GameEvent e)
    {
        _animator.SetBool("HARTOActive", ((AstridTalksToHARTOEvent)e).talkingToHARTO);
        _animator.SetBool("IsTalking", ((AstridTalksToHARTOEvent)e).talkingToHARTO);
        disableMovement = ((AstridTalksToHARTOEvent)e).talkingToHARTO;
    }

    #region Overview private void OnToggleDisableMovement(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*          Stoping Astrid from moving                                                                                  */
    /*          This function is called when the event manager fires a new ToggleDisableMovementEvent.                      */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The event that was fired                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnToggleDisableMovement(GameEvent e)
	{
		disableMovement = ((DisablePlayerMovementEvent)e).disableMovement;
	}

    #region Overview private void OnToggleHARTO(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*          PLaying animation from bringing up HARTO                                                                    */
    /*          This function is called when the event manager fires a new ToggleHARTOEvent          .                      */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The event that was fired                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnToggleHARTO(GameEvent e)
	{
		_animator.SetBool("HARTOActive", HARTO_UI_Interface.HARTOSystem.isHARTOActive);
	}

    #region Overview private void OnClosingHARTOForTheFirstTime(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*          Setting the NPC Astrid is talking to no one after closing HARTO for the first time                          */
    /*          This function is called when the event manager fires a new ClosingHARTOForTheFirstTime                      */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The event that was fired                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnClosingHARTOForTheFirstTime(GameEvent e)
	{
		npcAstridIsTalkingTo = "";		
	}

    #region Overview private void OnBeginTutorial(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*          Setting the NPC Astrid is talking to at the beginning of the game                                           */
    /*          This function is called when the event manager fires a new BeginTutorialEvent                               */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The event that was fired                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnBeginTutorial(GameEvent e)
    {
        npcAstridIsTalkingTo = "Priya";
    }

    #region Overview private void Move(float dx, float dy)
    /****************************************************************************************/
    /*																						*/
    /*	Move: Handles player movement														*/
    /*		param: float dx - Horizontal Input												*/
    /*			   float dy - Vertical Input												*/
    /*																						*/
    /****************************************************************************************/
    #endregion
    private void Move(float dx, float dy)
	{
		//	Adds force to rigidbody based on the input
		_rigidBody2D.velocity = new Vector2(dx * moveSpeed, dy * moveSpeed);
		_animator.SetFloat("SpeedX", Mathf.Abs(dx));
		_animator.SetFloat("SpeedY", dy);
	}

    #region Overview private void PlayFootStepAudio()
    /****************************************************************************************/
    /*																						*/
    /*	Responsible for letting the animator play footstep audio			            	*/
    /*		param: None                     												*/
    /*		Returns: Nothing                												*/
    /*																						*/
    /****************************************************************************************/
    #endregion
    public void PlayFootStepAudio()
	{
        if(GameManager.instance.sceneName.Contains("Meadow"))
        {
            _clip = Resources.Load("Audio/SFX/FOOTSTEPS/grass_footstepss") as AudioClip;
        }
        else if (GameManager.instance.sceneName.Contains("Hunter"))
        {
            _clip = Resources.Load("Audio/SFX/FOOTSTEPS/rock_footsteps") as AudioClip;
        }
        else if (GameManager.instance.sceneName.Contains("Road"))
        {
            _clip = Resources.Load("Audio/SFX/FOOTSTEPS/road_footsteps") as AudioClip;
        }
        else if (GameManager.instance.sceneName.Contains("Rock"))
        {
            _clip = Resources.Load("Audio/SFX/FOOTSTEPS/wet_footsteps") as AudioClip;
        }
        else
        {
            _clip = Resources.Load("Audio/SFX/FOOTSTEPS/dirt_footsteps") as AudioClip;
        }

        _audioSource.Stop();
		if(!_audioSource.isPlaying)
		{
			_audioSource.pitch = Random.Range(0.85f, 1.2f);
			_audioSource.PlayOneShot(_clip);
		}
	}

    #region Overview private void FixedUpdate()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Running once per fixed interval					                                                            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void FixedUpdate () 
	{

        if(Input.GetKey(leftKey) || Input.GetKey(KeyCode.LeftArrow))
        {
            _renderer.flipX = false;
        }
        else if(Input.GetKey(rightKey) || Input.GetKey(KeyCode.RightArrow))
        {
            _renderer.flipX = true;
        }

		x = Input.GetAxis(HORIZONTAL_AXIS);
		y = Input.GetAxis(VERICLE_AXIS);
        if (!GameManager.instance.HARTOinUtan && !GameManager.instance.trackProgressInHARTO)
        {
            _animator.SetBool("HARTOActive", HARTO_UI_Interface.HARTOSystem.isHARTOActive);
        }
		if(!disableMovement)
		{
			Move(x, y);
            if (x != 0 || y != 0)
            {
                _animator.SetBool("HARTOActive", false);
                _animator.SetBool("IsTalking", false);
            }
        }
        else
        {
            Move(0, 0);
        }
    }

    #region Overview private void OnTriggerEnter2D(Collider2D collider)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Setting the NPC you are talking to                                          					            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          Collider2D collider: the object you collided with                                                           */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == NPC)
		{
			npcAstridIsTalkingTo = other.gameObject.name;
		}
	}
}
