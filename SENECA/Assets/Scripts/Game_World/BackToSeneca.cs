using UnityEngine;
using SenecaEvents;

#region BackToSeneca.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    This script initiates the last conversation and transisitons to the credit scene                                  */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*           private:                                                                                                   */
/*                 private void Start()                                                                                 */
/*                 private void OnTriggerEnter2D(Collider2D collider)                                                   */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/*           public:                                                                                                    */
/*                 public void RollCredits()                                                                            */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class BackToSeneca : MonoBehaviour 
{
    public bool fireOnce;
    public float volume;
	public Animator anim;
	public AudioClip clip;
	public AudioSource audioSource;
	public BoxCollider2D characterCollider;
	public BoxCollider2D triggerArea;
	private BoxCollider2D[] colliders;

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
    private void Start () 
	{
        fireOnce = false;
		audioSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		colliders = GetComponents<BoxCollider2D>();
        volume = BGM_Singleton.instance.audioSource.volume;

        for (int i = 0; i < colliders.Length; i++)
		{
			if(colliders[i].isTrigger)
			{
				triggerArea = colliders[i];
			}
			else
			{
				characterCollider = colliders[i];
			}
		}
	}

    #region Overview private void OnTriggerEnter2D(Collider2D collider)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Starting the conversation with Ruth in Utan Forest Fork					                                    */
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
		if (collider.tag == "Player" && !fireOnce)
		{
            fireOnce = true;
            //	Play Ruth Audio and flash the screen.
            Services.Events.Fire(new DisablePlayerMovementEvent(true));
            Services.Events.Fire(new EndGameEvent());
			
		}
	}

    #region Overview public void Rollcredits()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          swaping to the credit scene				                                                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void RollCredits()
	{
        Services.Events.Fire(new SceneChangeEvent("Credits"));
        BGM_Singleton.instance.clip = Resources.Load("Audio/Music/Title_Theme") as AudioClip;
        BGM_Singleton.instance.audioSource.Stop();
        BGM_Singleton.instance.audioSource.volume = 0.3f;
        Services.Scenes.Swap<CreditSceneScript>(TransitionData.Instance);
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
		if (GameManager.instance.endGame)
		{
            volume -= 0.08f;
            BGM_Singleton.instance.audioSource.volume = volume;

            if(BGM_Singleton.instance.audioSource.volume < 0)
            {
                BGM_Singleton.instance.audioSource.volume = 0;
            }
            // put this in an event. probably EndGameEvent
            anim.SetBool("Flash", true);
		}

        if(Input.GetKeyDown(KeyCode.C))
        {
            RollCredits();
        }

		
	}
}
