using UnityEngine;
using ChrsUtils.ChrsEventSystem.GameEvents;
using SenecaEvents;

#region PuzzleWall.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Logic for Puzzle wall animations and managing the solved state are controlled here                                */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void OnDestroy()                                                                             */
/*                 private void OnPuzzleCompleted(GameEvent e)                                                          */
/*                 private void OnTriggerEnter2D(Collider2D collider)                                                   */
/*                 private void OnTriggerExit2D(Collider2D collider)                                                    */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class PuzzleWall : MonoBehaviour 
{
	public BoxCollider2D puzzleTrigger;
	public UtanPuzzle utanPuzzle;
	public PuzzleWallMover puzzleWall;

	private const string PLAYER = "Player";
	private BoxCollider2D[] colliders;

	private PuzzleCompletedEvent.Handler onPuzzleCompleted;

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
		puzzleTrigger = GetComponent<BoxCollider2D>();

		utanPuzzle = GameObject.FindGameObjectWithTag("UtanPuzzle").GetComponent<UtanPuzzle>();
		puzzleWall = GameObject.Find("PuzzleWall").GetComponent<PuzzleWallMover>();
		utanPuzzle.anim.SetBool("IsActive", false);
		puzzleWall.anim.SetBool("Solved", false);

		onPuzzleCompleted = new PuzzleCompletedEvent.Handler(OnPuzzleCompleted);
		Services.Events.Register<PuzzleCompletedEvent>(onPuzzleCompleted);
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
		Services.Events.Unregister<PuzzleCompletedEvent>(onPuzzleCompleted);
	}

    #region Overview private void OnSceneChange(GameEvent e)
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Setting the appropriate variables for a solved puzzle                                                           */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          GameEvent e: The event that was fired                                                                       */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnPuzzleCompleted(GameEvent e)
	{
		puzzleWall.anim.SetBool("Solved", true);
		utanPuzzle.anim.SetBool("IsActive", false);
		GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1);
	}

    #region Overview private void OnTriggerEnter2D(Collider2D collider)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Making the puzzle appear                                                        				            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          Collider2D collider: the object you collided with                                                           */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == PLAYER)
		{
			utanPuzzle.anim.SetBool("IsActive", true);
		}
	}

    #region Overview private void OnTriggerExit2D(Collider2D collider)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Making the puzzle disappear                                                 					            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          Collider2D collider: the object you collided with                                                           */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.tag == PLAYER)
		{
			utanPuzzle.anim.SetBool("IsActive", false);
		}
	}
}
