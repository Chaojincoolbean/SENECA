using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;

public class TitleSceneScript : Scene<TransitionData> 
{
	internal override void OnEnter(TransitionData data)
	{
		Debug.Log ("Entered Title");
	}

	internal override void OnExit()
	{
	}
}
