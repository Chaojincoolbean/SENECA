using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour 
{

	public KeyCode startGame = KeyCode.Tab;
	public const string OPENING_SCENE = "Seneca_Campsite";
	public const string PREAMBLE_SCENE = "_Prologue";

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(startGame))
		{
			SceneManager.LoadScene(OPENING_SCENE);
		}	
	}
}
