﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChrsUtils.ChrsEventSystem.EventsManager;
using SenecaEvents;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	TitleScreenPuzzle: Draws the line                          							*/
/*			Functions:																	*/
/*					public:																*/
/*						                        										*/
/*					protected:															*/
/*                                                                                      */
/*					private:															*/
/*						void Start ()													*/
/*						void Update ()													*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class TitleScreenPuzzle : MonoBehaviour 
{
	//	Public Constant Variables
	public const string HARTO_NODE = "HARTONode";				//	String reference to the HARTONODE tag.

	public bool puzzleToggle;

	//	Public Variables
	public bool drawingLine;									//	Returns true if you are drawing a line
	public Vector3 origin;										//	Origin point for the line
	public Vector3 destination;									//	Where the line ends up
	public GameObject thisLine;									//	Literally this line. Assign "DrawLine" component to this in the inspector
	//public GameObject newLine;									//	The next line to be drawn

	//	Private Variables
	private RaycastHit2D hit;										//	For cursour detection on HARTONODES
	private Ray2D ray;											//	For cursor detection on HARTONODES
	private LineRenderer lineRenderer;							//	Reference to Line Renderer

	private List<GameObject> lines; //list that holds the GameObjects with LineRenderer
	public List<GameObject> nodes;
	private List<GameObject> usedNodes;


	public GameObject currentNode;
	private GameObject prevNode;

	private int lastNodeIndex;

	public bool solved;

	public Material[] materials;
	//List<Material> materials;

	private Renderer ringRenderer;
	private AudioManager_prototype audiomanager;

	private AudioSource[] audios;

	public float boundaryY;

	private int audioCount;
	private bool[] audioCheck;

	public GameObject particleSystem;
	
	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Start: Runs once at the begining of the game. Initalizes variables.					*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Start () 
	{
		lines = new List<GameObject> ();
		nodes = new List<GameObject> ();
		usedNodes = new List<GameObject> ();

		foreach (GameObject go in GameObject.FindGameObjectsWithTag(HARTO_NODE)) {
			nodes.Add (go);
			//go.transform.position = new Vector3 (go.transform.position.x, Random.Range (-boundaryY, boundaryY), go.transform.position.z);
		}

		//shuffleGameObjects (nodes);

		lastNodeIndex = Random.Range(0,nodes.Count);


		if (puzzleToggle) {
			//shuffleMaterials (materials);
			int materialIndex = 0;
			foreach (GameObject node in nodes) {
				node.GetComponent<Renderer> ().material = materials [materialIndex];
				materialIndex++;
			}
			ringRenderer = GameObject.Find ("UtanRing").GetComponent<Renderer> ();
			ringRenderer.material = materials [lastNodeIndex];
		} else {
			audiomanager = GameObject.Find ("AudioManager").GetComponent<AudioManager_prototype> ();
			audios = new AudioSource[nodes.Count];
			for(int i = 0; i <audios.Length; i++)
			{
				audios[i] = audiomanager.notes[i];
			}
			audioCheck = new bool[audios.Length];
			audioCheck = new bool[audios.Length];
		}
	}

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Update: Called once per frame														*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Update () 
	{

		if(solved && Input.GetKeyUp (KeyCode.Mouse0))
		{
			drawingLine = false;
			StartCoroutine(LoadGame());	
		}
		else if (!solved) 
		{
				//	Connects mose position on screen to game screen
				hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

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
								if (!audios [i].isPlaying && !solved) 
								{
									Debug.Log("WhyTHO? " + solved);
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
								Vector3 linePosition = new Vector3(usedNodes [usedNodes.Count - 1].transform.position.x, usedNodes [usedNodes.Count - 1].transform.position.y, 9);
								lineRenderer.SetPosition (1, linePosition);
								DrawNewLine ();
								particleSystem.GetComponent<ParticleSystem>().Play();
								particleSystem.transform.position = hit.collider.transform.position;
							} 

							if (usedNodes.Count >= nodes.Count && CheckIfEveryNodeIsReached ()) 
							{
								//check if the end condition has been met
								drawingLine = false;
								solved = true;
								Debug.Log ("the extremely hard puzzle has been conquered");
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

		
	}

	IEnumerator LoadGame()
	{
		float t = 0;
		while(t < 1)
		{
			//float fadingVolume = 
			
			audiomanager.notes[0].volume = Mathf.Lerp(1, 0, t);
			audiomanager.notes[1].volume = Mathf.Lerp(1, 0, t );
			t = t +  Time.deltaTime;
		}
		yield return new WaitForSeconds(2.0f);
		GameEventsManager.Instance.Fire(new SceneChangeEvent("_Prologue"));
		SceneManager.LoadScene("_Prologue");
	}

	bool CheckIfEveryNodeIsReached(){
		int count = 0;
		for (int j = 0; j < nodes.Count; j++) {
			for (int i = 0; i < usedNodes.Count; i++) {
				if (usedNodes [i].GetInstanceID () == nodes [j].GetInstanceID()) {
					count++;
					break;
				}
			}

			if (count == nodes.Count) {

				return true;
			}
		}

		return false;
	}


	void CheckIfNoLongerDrawing()
	{

		if(solved)
		{
			drawingLine = false;
		}

		for(int i = 0; i < nodes.Count; i++)
		{
			nodes[i].GetComponent<Animator>().SetBool("IsActive", false);
		}
		//	When you release the left mouse button you are no longer drawing the line
		drawingLine = false;
		//destroy every line
		if(lines.Count > 0 || solved){
			for (int i = lines.Count - 1; i >= 0; i--) {
				GameObject line = lines [i];
				lines.RemoveAt (i);
				Destroy (line);
			}
		}
		if (usedNodes.Count > 0) {
			for (int i = usedNodes.Count - 1; i >= 0; i--) {
				GameObject node = usedNodes [i];
				usedNodes.RemoveAt (i);
			}
		}
		if (!puzzleToggle) {
			for (int i = 0; i < audioCheck.Length; i++) {
				audioCheck [i] = false;
			}
			audioCount = 0; 
		}

		particleSystem.SetActive (false);
	}

	void DrawNewLine()
	{
		//	Makes a new line at the origin point of the line based on this gameObject
		GameObject newLine = (GameObject)Instantiate (thisLine, usedNodes[usedNodes.Count-1].transform.position, transform.rotation);

		//	We only need one game object with the DrawLine script. Destroy this script on the new game object
		Destroy (newLine.GetComponent<DrawLine> ());

		//	Have lineRenderer reference the LineRenderer component on the new line
		lineRenderer = newLine.GetComponent<LineRenderer> ();

		//	Sets the starting and end width of the line
		lineRenderer.startWidth = 0.06f;
		lineRenderer.endWidth = 0.06f;

		//	We are now drawing a line
		
			drawingLine = true;

		//	Sets position on the line renderer
		lineRenderer.SetPosition (0, usedNodes[usedNodes.Count-1].transform.position);

		//adds the line to the array
		lines.Add (newLine);
	}
}
