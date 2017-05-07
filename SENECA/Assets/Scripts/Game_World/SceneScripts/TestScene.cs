using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;

public class TestScene : Scene<TransitionData> 
{
	internal override void OnEnter(TransitionData data)
	{
		GameManager.instance.isTestScene = true;
	}

	internal override void OnExit()
	{
	}
}
