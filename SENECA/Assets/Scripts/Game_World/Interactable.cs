using UnityEngine;
using SenecaEvents;

#region Interactable.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for all Interactables that are not Beron's HARTO. Future interations could use inheritance            */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void OnMouseEnter()                                                                          */
/*                 private void OnMouseExit()                                                                           */
/*                 private void OnMouseDown()                                                                           */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour 
{
    public bool hasBeenClicked;
	public Collider2D myCollider;
    public Texture2D hoverCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public AudioSource myAudioSource;
	public AudioClip clip;

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
		myAudioSource = GetComponent<AudioSource> ();
        myCollider = GetComponent<Collider2D>();
		
	}

    #region Overview private void OnMouseEnter()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Changing the cursor image when ontop of an Interactable                                                         */
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
    /*      Changing the cursor image when moving off of an Interactable                                                    */
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

    #region Overview private void OnMouseDown()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*          Plays Astrid's line after clicking on the Interactable                      	    			            */
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
            //  The events that fire the event bring up Astrid's HARTO arm and starts her talking animation
            //  but don't lock her movement.

            if (transform.name == "Priya" && GameManager.instance.tutorialIsDone)
            {
                clip = Resources.Load("Audio/VO/Priya/SCENE_1/VO_EVENT/Priya_Hurry") as AudioClip;
            }
            else if (tag == "Rocks")
            {
                Services.Events.Fire(new InteractableEvent(true, true, false));
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Rocks") as AudioClip;
            }
            else if (tag == "Radio")
            {
                Services.Events.Fire(new InteractableEvent(true, true, false));
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Radio") as AudioClip;
            }
            else if (tag == "Sign")
            {
                Services.Events.Fire(new InteractableEvent(true, true, false));
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Sign") as AudioClip;
            }
            else if (tag == "Racks")
            {
                //  done
                Services.Events.Fire(new InteractableEvent(true, true, false));
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Rack") as AudioClip;
            }
            else if (tag == "Fence")
            {
                //  done
                Services.Events.Fire(new InteractableEvent(true, true, false));
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Fence") as AudioClip;
            }
            else if (tag == "Carving")
            {
                Services.Events.Fire(new InteractableEvent(true, true, false));
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Carving") as AudioClip;
            }
            else if (tag == "Backpack" && GameManager.instance.tutorialIsDone)
            {
                clip = Resources.Load("Audio/VO/Astrid/SCENE_1/VO_EVENT/Astrid_Backpack") as AudioClip;
            }

            hasBeenClicked = true;
            myAudioSource.PlayOneShot(clip);
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
        if(!myAudioSource.isPlaying && GameManager.instance.tutorialIsDone && !GameManager.instance.HARTOinUtan)
        {
            //  Have this in an event for better control of animations
            Services.Events.Fire(new InteractableEvent(false, false, false));

        }
    }
}
