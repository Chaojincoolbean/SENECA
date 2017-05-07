using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.GameEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

public class PuzzleWall : MonoBehaviour 
{
	public BoxCollider2D puzzleTrigger;
	public UtanPuzzle utanPuzzle;
	public PuzzleWallMover puzzleWall;
	private const string PLAYER = "Player";
	private BoxCollider2D[] colliders;
	private PuzzleCompletedEvent.Handler onPuzzleCompleted;
	// Use this for initialization
	void Start () 
	{
		puzzleTrigger = GetComponent<BoxCollider2D>();

		utanPuzzle = GameObject.FindGameObjectWithTag("UtanPuzzle").GetComponent<UtanPuzzle>();
		puzzleWall = GameObject.Find("PuzzleWall").GetComponent<PuzzleWallMover>();
		utanPuzzle.anim.SetBool("IsActive", false);
		puzzleWall.anim.SetBool("Solved", false);
		onPuzzleCompleted = new PuzzleCompletedEvent.Handler(OnPuzzleCompleted);
		Services.Events.Register<PuzzleCompletedEvent>(onPuzzleCompleted);
	}

	void OnDestroy()
	{
		Services.Events.Unregister<PuzzleCompletedEvent>(onPuzzleCompleted);
	}

	void OnPuzzleCompleted(GameEvent e)
	{
		Debug.Log("Nice job!");
		puzzleWall.anim.SetBool("Solved", true);
		utanPuzzle.anim.SetBool("IsActive", false);
		GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 0);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == PLAYER)
		{
			//	Animation to make puzzle appear goes here
			utanPuzzle.anim.SetBool("IsActive", true);
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.tag == PLAYER)
		{
			utanPuzzle.anim.SetBool("IsActive", false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
