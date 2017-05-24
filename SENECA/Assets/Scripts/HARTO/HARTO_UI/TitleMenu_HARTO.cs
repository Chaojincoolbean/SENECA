using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SenecaEvents;

#region TitleMenu_HARTO.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    TitleMEnu_HARTO.cs is responsible for rotating the icons and connecting the title scene to the other scenes       */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          public:                                                                                                     */
/*                  public void Start()                                                                                 */
/*                  public void SpawnIcons(HARTO_UI_Interface.Action[] actions)                                         */
/*                                                                                                                      */
/*          private:                                                                                                    */
/*                  private void RotateIconWheel(float scrollWheel)                                                     */
/*                  private void SelectIcon()                                                                           */
/*                  private void SelectOption(RadialIcon icon)                                                          */
/*                  private void ForceStart()                                                                           */
/*                  private void Update()                                                                               */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class TitleMenu_HARTO : MonoBehaviour
{ 
    public bool iconSelected;                                       //  A bool check to see if an icon has been selected
	public bool canSelect;                                          //  A bool to see if we can select an icon
	public float rotationSpeed = 5.0f;                              //  Rotation speed of the icon wheel
	public RadialIcon radialIconPrefab;                             //  A reference to the prefab of the icons
	public Image selectionArea;                                     //  Reference of selection area image
	public Image screenHARTO;		                                //  DEPRECIATED: Reference of HARTO screen image
	public Sprite emptyAreaSprite;                                  //  Reference of the empty area sprite
	public AudioClip clip;                                          //  Holder for all sound effects on the title screen
	public AudioSource audioSource;                                 //  Reference to audioSource
    public Animator _anim;                                          //  Reference to the Title Screen's animator
    public List<RadialIcon> iconList;                               //  A list of all icons on the title menu

    private const string SCROLLWHEEL = "Mouse ScrollWheel";
    private const string HARTO_SCREEN = "HARTO_Screen";

    private float rotateSelectionWheel;                             //  Stores the previous position of the rotation wheel

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
    public void Start()
	{
        // Changes rotation speed based on the platform you are running on
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            rotationSpeed = 50.0f;
        }

        iconSelected = false;
        canSelect = true;

        _anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		screenHARTO = GameObject.Find(HARTO_SCREEN).GetComponent<Image>();

		SpawnIcons(HARTO_UI_Interface.HARTOSystem.titleMenu);
    }

    #region Overview public void SpawnIcons (HARTO_UI_Interface.Action[] actions)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Putting the icons on the wheel                                                                              */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          HARTO_UI_Interface.Action[] actions :                                                                       */
    /*          This variable in HARTO_UI_Interface tells which icons need to be spwawned.                                  */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    public void SpawnIcons (HARTO_UI_Interface.Action[] actions) 
	{
		RadialIcon newRadialIcon;
		for (int i = 0 ; i < actions.Length; i++)
		{
			newRadialIcon = Instantiate(radialIconPrefab) as RadialIcon; 
			iconList.Add(newRadialIcon);
			newRadialIcon.transform.SetParent(transform);

            //  Positions the icons in a circle around the center of the gameobject
			float theta = (2 * Mathf.PI / actions.Length) * i;
			float xPos = Mathf.Sin(theta);
			float yPos = Mathf.Cos(theta);
			newRadialIcon.transform.localPosition = new Vector3(xPos, yPos, 0.0f);

			newRadialIcon.icon.color = actions[i].color;
			newRadialIcon.alreadySelected = actions[i].alreadySelected;
			newRadialIcon.transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
			newRadialIcon.icon.sprite = actions[i].sprite;
			newRadialIcon.title = actions[i].title;
		}
	}

    #region Overview private void RotateIconWheel(float schrollWheel)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Rotation of icon wheel                                                                                      */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          float scrollWheel: Input axis from "Mouse ScrollWheel"                                                      */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void RotateIconWheel(float scrollWheel)
	{	
		for (int i = 0; i < iconList.Count; i++)
		{
			float theta = (2 * Mathf.PI / iconList.Count) * i;
            
            //  These are multiplied by 1.1f to move the icons outward
			float xPos = Mathf.Sin(theta + scrollWheel) * 1.1f;
			float yPos = Mathf.Cos(theta + scrollWheel) * 1.1f;

			iconList[i].transform.localPosition = new Vector3(xPos, yPos) * (GetComponent<RectTransform>().rect.width * 0.3f);
		}
	}

    #region Overview private void SelectIcon()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Selecting an icon                                                                                           */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void SelectIcon()
	{
		
		for (int i = 0; i < iconList.Count; i++)
		{	
			if (Vector3.Distance (iconList [i].icon.rectTransform.localPosition, selectionArea.rectTransform.localPosition) < 4.0f)
            {
                _anim.SetBool ("IconInRange", true);
				clip = Resources.Load ("Audio/SFX/HARTO_Icon_Passes_Into_Circle") as AudioClip;

                if (Input.GetKeyDown (KeyCode.Mouse0))
                {
					if (!iconList [i].alreadySelected)
                    {
						clip = Resources.Load ("Audio/SFX/HARTO_Select") as AudioClip;
						if (!audioSource.isPlaying)
                        {
							audioSource.PlayOneShot (clip, 0.5f);
						}
                        iconSelected = true;
						

					}
                    else
                    {
						clip = Resources.Load ("Audio/SFX/HARTO_Negative_Feedback") as AudioClip;
						if (!audioSource.isPlaying)
                        {
							audioSource.PlayOneShot (clip);
						}
					}
				}
                
                if (iconSelected && !audioSource.isPlaying)
                {
                    SelectOption(iconList[i]);
                }
			} 
			else 
			{
				_anim.SetBool ("IconInRange", false);
            }
		}
    }

    #region Overview private void SelectOption(RadialIcon icon)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Starting the icon you selected					                                                            */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          RadialIcon icon: the icon you selcted in the SelctIcon  function                                            */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void SelectOption(RadialIcon icon)
	{
	    if(icon.title == "StartGame")
		{
			Services.Events.Fire(new SceneChangeEvent("_Prologue"));
			TransitionData.Instance.TITLE.visitedScene = true;
			TransitionData.Instance.TITLE.position = Vector3.zero;
			TransitionData.Instance.TITLE.scale = Vector3.zero;
			Services.Scenes.Swap<PrologueSceneScript>(TransitionData.Instance);
            //  This takes you to PrologueSceneScript.cs
            //  Located in Scripts -> Game_World -> SceneScripts
        }
        else if(icon.title == "PuzzleProtoTypes")
       {
             Services.Scenes.Swap<PrototypeSceneScript>(TransitionData.Instance);
            //  This takes you to PrototypeSceneScript.cs
            //  Located in Scripts -> Game_World -> SceneScripts
        }
        else if (icon.title == "Credits")
        {
            Services.Scenes.Swap<CreditSceneScript>(TransitionData.Instance);
            //  This takes you to CreditSceneScript.cs
            //  Located in Scripts -> Game_World -> SceneScripts
        }
    }

    #region Overview private void ForceStart()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          	Starting the game with the press of the Spacebar	                                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void ForceStart()
    {
        Services.Events.Fire(new SceneChangeEvent("_Prologue"));
        TransitionData.Instance.TITLE.visitedScene = true;
        TransitionData.Instance.TITLE.position = Vector3.zero;
        TransitionData.Instance.TITLE.scale = Vector3.zero;
        Services.Scenes.Swap<PrologueSceneScript>(TransitionData.Instance);
        //  This takes you to PrologueSceneScript.cs
        //  Located in Scripts -> Game_World -> SceneScripts
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ForceStart();
        }

		if (screenHARTO == null)
		{
			screenHARTO = GameObject.Find(HARTO_SCREEN).GetComponent<Image>();
		}

		if(selectionArea == null)
		{
			selectionArea = GameObject.Find("SelectionArea").GetComponent<Image>();
		}

		float rotate = rotateSelectionWheel +  Input.GetAxis (SCROLLWHEEL) * rotationSpeed * Time.deltaTime;
		if(rotateSelectionWheel != rotate)
		{
			clip = Resources.Load("Audio/SFX/HARTO_Scroll") as AudioClip;

			if(!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(clip, Mathf.Abs(Input.GetAxis(SCROLLWHEEL)));
			}
		}

		rotateSelectionWheel = 	rotate;
		RotateIconWheel(rotateSelectionWheel);

		SelectIcon();

		for (int i = 0; i < iconList.Count; i++) {
			iconList [i].color.CrossFadeAlpha (selectionArea.color.a, 0, true);
		}
	}
}
