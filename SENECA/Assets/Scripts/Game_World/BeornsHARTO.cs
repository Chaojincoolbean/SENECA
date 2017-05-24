using System.Collections;
using UnityEngine;
using SenecaEvents;

#region BeornHARTO.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for Beorn's existance in the scenes and starting Astrid finding Beorn's HARTO scene                   */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void OnMouseEnter()                                                                          */
/*                 private void OnMouseExit()                                                                           */
/*                 private void OnMouseDown()                                                                           */
/*                 private void OnTriggerEnter2D(Collider2D collider)                                                   */
/*                 private IEnumerator BringUpHARTO()                                                                   */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class BeornsHARTO : MonoBehaviour
{
    public bool hasBeenClicked;
    public bool clipHasPlayed;
    public AudioClip clip;
    public AudioSource audioSource;
    public Collider2D myCollider;
    public Texture2D hoverCursor;
    public CursorMode cursorMode = CursorMode.Auto;

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
        hasBeenClicked = false;
        if (GameManager.instance.pickedUpBeornsHARTO)
        {
            Destroy(gameObject);
        }
        clipHasPlayed = false;
        audioSource = GetComponent<AudioSource>();	
	}

    #region Overview private void OnMouseEnter()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Changing the cursor image when ontop of Beorn's HARTO                                                           */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnMouseEnter()
    {
        if (!hasBeenClicked)
        {
            hoverCursor = Resources.Load("Sprites/UI_Images/handcursor") as Texture2D;
            Cursor.SetCursor(hoverCursor, Vector2.zero, cursorMode);
        }
    }

    #region Overview private void OnMouseExit()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Changing the cursor image when moving off of Beorn's HARTO                                                      */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    #region Overview private void OnMouseEnter()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*          Starting the Astrid picking up HARTO scene when clicking on Beron's HARTO		    			            */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnMouseDown()
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            GameManager.instance.HARTOinUtan = true;
            GameManager.instance.pickedUpBeornsHARTO = true;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            clip = Resources.Load("Audio/VO/Astrid/SCENE_2/VO_Event/PickUpHARTO") as AudioClip;
            Services.Events.Fire(new InteractableEvent(true, false, true));
            StartCoroutine(BringUpHARTO());
            audioSource.PlayOneShot(clip);
        }
        clipHasPlayed = true;
    }

    #region Overview private void OnTriggerEnter2D(Collider2D collider)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Starting the Astrid picking up HARTO scene when colliding with Beron's HARTO					            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          Collider2D collider: the object you collided with                                                           */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasBeenClicked = true;
        GameManager.instance.HARTOinUtan = true;
        GameManager.instance.pickedUpBeornsHARTO = true;
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        clip = Resources.Load("Audio/VO/Astrid/SCENE_2/VO_Event/PickUpHARTO") as AudioClip;
        Services.Events.Fire(new InteractableEvent(true, false, true));
        StartCoroutine(BringUpHARTO());
        audioSource.PlayOneShot(clip);
        clipHasPlayed = true;
        
    }

    #region Overview IEnumerator BringUpHARTO()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Freezing player's animation until the scene is over					                                        */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The type of objects to enumerate.                                                                           */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private IEnumerator BringUpHARTO()
    {
        
        yield return new WaitForSeconds(7.5f);
        Services.Events.Fire(new InteractableEvent(false, true, true));

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
    private void Update ()
    {
		if(clipHasPlayed && !audioSource.isPlaying)
        {
            GameManager.instance.HARTOinUtan = false;
            Services.Events.Fire(new InteractableEvent(false, false, false));
            Destroy(gameObject);
        }
	}
}
