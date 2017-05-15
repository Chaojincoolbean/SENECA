using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;
using ChrsUtils.ChrsExtensionMethods;

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
	
	public float moveSpeed;
	public Animator _animator;

	private const float MAX_SCALE = 0.7f;
	private const float MIN_SCALE = 0.2f;
	private AudioSource _audioSource;
	private AudioClip _clip;

	private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _renderer;
    private AstridTalksToHARTOEvent.Handler onAstridTalksToHARTO;
	private DisablePlayerMovementEvent.Handler onToggleDisableMovement;
    private BeginTutorialEvent.Handler onBeginTutorial;
	private ToggleHARTOEvent.Handler onToggleHARTO;
	private ClosingHARTOForTheFirstTimeEvent.Handler onClosingHARTOForTheFirstTime;

	// Use this for initialization
	void Start () 
	{
		_rigidBody2D = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
		_audioSource = GetComponent<AudioSource>();

        onAstridTalksToHARTO = new AstridTalksToHARTOEvent.Handler(OnAstridTalksToHARTO);
		onToggleDisableMovement = new DisablePlayerMovementEvent.Handler(OnToggleDisableMovement);
        onBeginTutorial = new BeginTutorialEvent.Handler(OnBeginTutorial);
		onToggleHARTO = new ToggleHARTOEvent.Handler(OnToggleHARTO);
		onClosingHARTOForTheFirstTime = new ClosingHARTOForTheFirstTimeEvent.Handler(OnClosingHARTOForTheFirstTime);

        Services.Events.Register<AstridTalksToHARTOEvent>(onAstridTalksToHARTO);
		Services.Events.Register<DisablePlayerMovementEvent>(onToggleDisableMovement);
        Services.Events.Register<BeginTutorialEvent>(onBeginTutorial);
		Services.Events.Register<ToggleHARTOEvent>(onToggleHARTO);
		Services.Events.Register<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
	}

    void OnAstridTalksToHARTO(GameEvent e)
    {
        Debug.Log("Here we go: " + ((AstridTalksToHARTOEvent)e).talkingToHARTO);
        _animator.SetBool("HARTOActive", ((AstridTalksToHARTOEvent)e).talkingToHARTO);
        _animator.SetBool("IsTalking", ((AstridTalksToHARTOEvent)e).talkingToHARTO);
        disableMovement = ((AstridTalksToHARTOEvent)e).talkingToHARTO;
    }

	void OnToggleDisableMovement(GameEvent e)
	{
        Debug.Log("Movement Disabled: " + ((DisablePlayerMovementEvent)e).disableMovement);
		disableMovement = ((DisablePlayerMovementEvent)e).disableMovement;
	}

	void OnToggleHARTO(GameEvent e)
	{
		_animator.SetBool("HARTOActive", HARTO_UI_Interface.HARTOSystem.isHARTOActive);
	}

	void OnClosingHARTOForTheFirstTime(GameEvent e)
	{
		npcAstridIsTalkingTo = "";
		
	}

    void OnBeginTutorial(GameEvent e)
    {
        npcAstridIsTalkingTo = "Priya";
    }

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Move: Handles player movement														*/
	/*		param: float dx - Horizontal Input												*/
	/*			   float dy - Vertical Input												*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Move(float dx, float dy)
	{
        
		//	Adds force to rigidbody based on the input
		_rigidBody2D.velocity = new Vector2(dx * moveSpeed, dy * moveSpeed);
		_animator.SetFloat("SpeedX", Mathf.Abs(dx));
		_animator.SetFloat("SpeedY", dy);

	}

	public void PlayFootStepAudio()
	{
		_clip = Resources.Load("Audio/SFX/Footstep01") as AudioClip;

		if(!_audioSource.isPlaying)
		{
			_audioSource.pitch = Random.Range(0.85f, 1.2f);
			_audioSource.PlayOneShot(_clip);
		}
	}

	
	// Update is called once per frame
	void FixedUpdate () 
	{

        if(Input.GetKeyDown(leftKey) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _renderer.flipX = false;
        }
        else if(Input.GetKeyDown(rightKey) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _renderer.flipX = true;
        }

		float x = Input.GetAxis(HORIZONTAL_AXIS);
		float y = Input.GetAxis(VERICLE_AXIS);
		_animator.SetBool("HARTOActive", HARTO_UI_Interface.HARTOSystem.isHARTOActive);
		if(!disableMovement)
		{
			Move(x, y);
		}
        else
        {
            Move(0, 0);
        }
    }

	void OnTriggerEnter2D(Collider2D other) 
	{

		if (other.gameObject.tag == NPC)
		{
			npcAstridIsTalkingTo = other.gameObject.name;
		}

		if (other.gameObject.name == WITCHLIGHT) 
		{
			Debug.Log ("touch");
		}
			
	}
}
