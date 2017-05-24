using System.Collections.Generic;
using UnityEngine;
using SenecaEvents;

#region DrawLine.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for drawing lines on the HARTO puzzle                                                                 */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void Update ()                                                                               */
/*                 private bool CheckIfAudioPlayedInOrder()                                                             */
/*                 private bool CheckIfEveryNodeIsReached()                                                             */
/*                 private void CheckIfNoLongerDrawing()                                                                */
/*                 private void DrawNewLine()                                                                           */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class DrawLine : MonoBehaviour 
{
	//	Public Constant Variables
	public const string HARTO_NODE = "HARTONode";				//	String reference to the HARTONODE tag.

	public bool puzzleToggle;

	//	Public Variables
	public bool drawingLine;									//	Returns true if you are drawing a line
	public Vector3 origin;										//	Origin point for the line
	public Vector3 destination;									//	Where the line ends up
	public GameObject thisLine;									//	Literally this line. Assign "DrawLine" component to this in the inspector

	//	Private Variables
	private RaycastHit2D hit;									//	For cursour detection on HARTONODES
	private Ray2D ray;											//	For cursor detection on HARTONODES
	private LineRenderer lineRenderer;							//	Reference to Line Renderer

	private List<GameObject> lines;                             //  List that holds the GameObjects with LineRenderer
	public List<GameObject> nodes;
	private List<GameObject> usedNodes;


	public GameObject currentNode;
	private GameObject prevNode;

	private int lastNodeIndex;

	private bool solved;

	public Material[] materials;

	private Renderer ringRenderer;
	private AudioManager_prototype audiomanager;

	public AudioSource[] audios;

	public float boundaryY;

	private int audioCount;
	private bool[] audioCheck;
    public float t;
	public GameObject particleSystem;

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
        t = 0;
		lines = new List<GameObject> ();
		nodes = new List<GameObject> ();
		usedNodes = new List<GameObject> ();

		foreach (GameObject go in GameObject.FindGameObjectsWithTag(HARTO_NODE))
        {
			nodes.Add (go);
		}

		lastNodeIndex = Random.Range(0,nodes.Count);


		if (puzzleToggle) 
		{
			int materialIndex = 0;
			foreach (GameObject node in nodes) 
			{
				node.GetComponent<Renderer> ().material = materials [materialIndex];
				materialIndex++;
			}

			ringRenderer = GameObject.Find ("UtanRing").GetComponent<Renderer> ();
			ringRenderer.material = materials [lastNodeIndex];
		} 
		else 
		{
            if(tag == "Puzzle1")
            {
                audiomanager = GameObject.Find("AudioManager").GetComponent<AudioManager_prototype>();
            }
            else
            {
                audiomanager = GameObject.Find("AudioManager2").GetComponent<AudioManager_prototype>();
            }
			
			audios = new AudioSource[nodes.Count];
			for(int i = 0; i < audios.Length; i++)
			{
				audios[i] = audiomanager.notes[i];
			}
			audioCheck = new bool[audios.Length];
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
    private void Update () 
	{
		if (!solved)
        { 
			if (puzzleToggle)
            { 
                //  Color Puzzle
				//	Connects mose position on screen to game screen

				hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

				//	If the mouse ray collides with something go into this if-statement
				if (hit.collider != null)
                {
					//	If it collides with a HARTONODE go into this if-statement
					//	Waits for player to left click [INITIAL]
					if (Input.GetKeyDown (KeyCode.Mouse0))
                    {
						if (hit.collider.tag == HARTO_NODE)
                        {
							currentNode = hit.collider.gameObject;
							currentNode.GetComponent<Animator>().SetBool("IsActive", true);
							usedNodes.Add (hit.collider.transform.gameObject);
							DrawNewLine ();

							lineRenderer.startColor = hit.collider.transform.gameObject.GetComponent<Renderer> ().material.color;
							lineRenderer.endColor = new Color (0, 0, 0, 0);
							particleSystem.SetActive (true);
							particleSystem.GetComponent<ParticleSystem>().Play();
							particleSystem.transform.position = hit.collider.transform.position;
						}	
					}
				}

				//	Keep left mouse button down to keep drawing. 
				if (Input.GetKey (KeyCode.Mouse0) && drawingLine) 
				{			
					if (hit.collider != null) 
					{
						if (hit.collider.tag == HARTO_NODE) 
						{
							currentNode = hit.collider.gameObject;
							currentNode.GetComponent<Animator>().SetBool("IsActive", true);
							if (hit.collider.transform.gameObject.GetInstanceID () != usedNodes [usedNodes.Count - 1].GetInstanceID ()) 
							{
								lineRenderer.endColor = hit.collider.transform.gameObject.GetComponent<Renderer> ().material.color;
								usedNodes.Add (hit.collider.transform.gameObject);
								//finish drawing the previous line
								lineRenderer.SetPosition (1, usedNodes [usedNodes.Count - 1].transform.position);

								DrawNewLine ();
								lineRenderer.startColor = hit.collider.transform.gameObject.GetComponent<Renderer> ().material.color;
								lineRenderer.endColor = new Color (0, 0, 0, 0);

								particleSystem.GetComponent<ParticleSystem>().Play();
								particleSystem.transform.position = hit.collider.transform.position;
							} 
							if (usedNodes.Count >= nodes.Count && CheckIfEveryNodeIsReached () && hit.collider.transform.gameObject.GetInstanceID () == nodes [lastNodeIndex].GetInstanceID ()) 
							{
								//check if the end condition has been met
								solved = true;
							}
						}
					}
					//	Sets the end point of the line to where ever your mouse position is on screen (and off screen?)
					destination = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					lineRenderer.SetPosition (1, new Vector3 (destination.x, destination.y, 0));
				}

				//	When you release the left mouse button
				if (Input.GetKeyUp (KeyCode.Mouse0)) 
				{
					CheckIfNoLongerDrawing ();
				}
			} 
			else 
			{ 
                //  Music puzzle
				//	Connects mose position on screen to game screen
				if (Camera.main != null)
                {
					hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				}

				//	If the mouse ray collides with something go into this if-statement
				if (hit.collider != null) 
				{
					if (hit.collider.tag == HARTO_NODE) 
					{
						if (Input.GetKeyDown (KeyCode.Mouse0)) 
						{
							currentNode = hit.collider.gameObject;
							currentNode.GetComponent<Animator>().SetBool("IsActive", true);
							usedNodes.Add (hit.collider.transform.gameObject);
							DrawNewLine ();

							//initial check
							for (int i = 0; i < nodes.Count; i++) 
							{
								if (hit.collider.transform.gameObject.GetInstanceID () == nodes [i].GetInstanceID ()) 
								{
									if (audioCount == i) 
									{
										audioCheck [audioCount] = true;
										audioCount++;
									}
								}
							}
							particleSystem.SetActive (true);
							particleSystem.GetComponent<ParticleSystem>().Play();
							particleSystem.transform.position = hit.collider.transform.position;
							
						}
						for (int i = 0; i < nodes.Count; i++) 
						{
							if (hit.collider.transform.gameObject.GetInstanceID () == nodes [i].GetInstanceID ()) 
							{
								if (!audios [i].isPlaying ) 
								{
									audios [i].PlayOneShot (audios [i].clip);
									if (audioCount == i && usedNodes.Count > 0) 
									{
										audioCheck [audioCount] = true;
										audioCount++;
										Debug.Log (audioCount);
									}
								}
							}
						}
					}
				}

				//	Keep left mouse button down to keep drawing. 
				if (Input.GetKey (KeyCode.Mouse0) && drawingLine) 
				{			
					if (hit.collider != null) 
					{
						if (hit.collider.tag == HARTO_NODE) 
						{
							currentNode = hit.collider.gameObject;
							currentNode.GetComponent<Animator>().SetBool("IsActive", true);
							if (!usedNodes.Contains (hit.collider.transform.gameObject)) 
							{
								usedNodes.Add (hit.collider.transform.gameObject);
								//finish drawing the previous line
								lineRenderer.SetPosition (1, usedNodes [usedNodes.Count - 1].transform.position);
                                lineRenderer.transform.parent = transform;
                                DrawNewLine ();
								particleSystem.GetComponent<ParticleSystem>().Play();
								particleSystem.transform.position = hit.collider.transform.position;
							} 

							if (usedNodes.Count >= nodes.Count && CheckIfEveryNodeIsReached () && CheckIfAudioPlayedInOrder()) 
							{
								//check if the end condition has been met
								solved = true;								
							}
						}
					}

					//	Sets the end point of the line to where ever your mouse position is on screen (and off screen?)
					destination = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					lineRenderer.SetPosition (1, new Vector3 (destination.x, destination.y, 0));
				}

				//	When you release the left mouse button
				if (Input.GetKeyUp (KeyCode.Mouse0)) 
				{
					CheckIfNoLongerDrawing ();
				}

                if (solved && !Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Services.Events.Fire(new PuzzleCompletedEvent());
                    Transform[] lines = transform.GetComponentsInChildren<Transform>();
                    
                    foreach(Transform lineTransforms in lines)
                    {
                        Destroy(lineTransforms.gameObject);
                    }
                }

			}
		}

		if (Input.GetKey (KeyCode.Space))
        {
			Services.Events.Fire(new PuzzleCompletedEvent());
		}
	}

    #region Overview private bool CheckIfAudioPlayedInOrder()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Checking if the audio was played in order                                                                       */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          True if audio was played in order                                                                           */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private bool CheckIfAudioPlayedInOrder()
    {
		int count = 0;
		for (int i = 0; i < audioCheck.Length; i++)
        {
			if (audioCheck [i])
            {
				count++;
			}
		}

		if (count == audioCheck.Length)
        {
			return true;
		}
		return false;
	}

    #region Overview private bool CheckIfEveryNodeIsReached()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Checking if every node is connected                                                                             */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          True if node has been connected                                                                             */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private bool CheckIfEveryNodeIsReached()
    {
		int count = 0;
		for (int j = 0; j < nodes.Count; j++)
        {
			for (int i = 0; i < usedNodes.Count; i++)
            {
				if (usedNodes [i].GetInstanceID () == nodes [j].GetInstanceID())
                {
					count++;
					break;
				}
			}

			if (count == nodes.Count)
            {

				return true;
			}
		}

		return false;
	}

    #region Overview private void CheckIfNoLongerDrawing()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Check if we are not drawing anymore					                                                        */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void CheckIfNoLongerDrawing()
	{
		for(int i = 0; i < nodes.Count; i++)
		{
			nodes[i].GetComponent<Animator>().SetBool("IsActive", false);
		}

		//	When you release the left mouse button you are no longer drawing the line
		drawingLine = false;
		//destroy every line
		if(lines.Count > 0){
			for (int i = lines.Count - 1; i >= 0; i--)
            {
				GameObject line = lines [i];
				lines.RemoveAt (i);
				Destroy (line);
			}
		}

		if (usedNodes.Count > 0)
        {
			for (int i = usedNodes.Count - 1; i >= 0; i--)
            {
				GameObject node = usedNodes [i];
				usedNodes.RemoveAt (i);
			}
		}

		if (!puzzleToggle)
        {
			for (int i = 0; i < audioCheck.Length; i++)
            {
				audioCheck [i] = false;
			}
			audioCount = 0; 
		}

		particleSystem.SetActive (false);
	}

    #region Overview private void DrawNewLine()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Drawing a new line starting at the node					                                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void DrawNewLine()
    {
		//	Makes a new line at the origin point of the line based on this gameObject
		GameObject newLine = (GameObject)Instantiate (thisLine, usedNodes[usedNodes.Count-1].transform.position, transform.rotation);

		//	We only need one game object with the DrawLine script. Destroy this script on the new game object
		Destroy (newLine.GetComponent<DrawLine> ());

		//	Have lineRenderer reference the LineRenderer component on the new line
		lineRenderer = newLine.GetComponent<LineRenderer> ();
        lineRenderer.transform.parent = transform;

		//	Sets the starting and end width of the line
		lineRenderer.startWidth = 0.06f;
		lineRenderer.endWidth = 0.06f;

		//	We are now drawing a line
		drawingLine = true;

		//	Sets position on the line renderer
		lineRenderer.SetPosition (0, usedNodes[usedNodes.Count-1].transform.position);
		lineRenderer.sortingLayerName = "Line";

		//adds the line to the array
		lines.Add (newLine);
	}
}
