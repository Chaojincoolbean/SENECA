using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;

public class Player : MonoBehaviour 
{
	public const string NPC = "NPC";
	public const string HORIZONTAL_AXIS = "Horizontal";
	public const string VERICLE_AXIS = "Vertical";
	public const string WALL = "Wall";
	public const string WITCHLIGHT = "Witchlight";

	public bool diableMovement;
	public bool facingLeft;
	public string npcAstridIsTalkingTo;
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;
	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;
	
	public float moveSpeed;

	private Animator _animator;
	private Rigidbody2D _rigidBody2D;
	private DisablePlayerMovementEvent.Handler onToggleDisableMovement;
	private ClosingHARTOForTheFirstTimeEvent.Handler onClosingHARTOForTheFirstTime;


	// Use this for initialization
	void Start () 
	{
		_rigidBody2D = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();

		onToggleDisableMovement = new DisablePlayerMovementEvent.Handler(OnToggleDisableMovement);
		onClosingHARTOForTheFirstTime = new ClosingHARTOForTheFirstTimeEvent.Handler(OnClosingHARTOForTheFirstTime);

		GameEventsManager.Instance.Register<DisablePlayerMovementEvent>(onToggleDisableMovement);
		GameEventsManager.Instance.Register<ClosingHARTOForTheFirstTimeEvent>(onClosingHARTOForTheFirstTime);
	}

	void OnToggleDisableMovement(GameEvent e)
	{
		diableMovement = ((DisablePlayerMovementEvent)e).disableMovement;
	}

	void OnClosingHARTOForTheFirstTime(GameEvent e)
	{
		npcAstridIsTalkingTo = "";
		
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

		if (dx > 0 && !facingLeft)
		{
			// Flips player
			Flip ();
		}
		// Otherwise player should be facing left
		else if (dx < 0 && facingLeft)
		{
			// Flips player
			Flip ();
		}

	}


	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Flip: FLips player sprite right or left												*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public void Flip ()
	{
		// Switch the way the player is labeled as facing
		facingLeft = !facingLeft;

		// Multiply the player's x local scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float x = Input.GetAxis(HORIZONTAL_AXIS);
		float y = Input.GetAxis(VERICLE_AXIS);

		if(!diableMovement)
		{
			Move(x, y);
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
