using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;

public class TitleSceneScript : Scene<TransitionData> 
{
	internal override void OnEnter(TransitionData data)
	{
        TransitionData.Instance.SENECA_CAMPSITE.visitedScene = false;
        TransitionData.Instance.SENECA_FORK.visitedScene = false;
        TransitionData.Instance.SENECA_FARM.visitedScene = false;
        TransitionData.Instance.SENECA_HUNTER_CAMP.visitedScene = false;
        TransitionData.Instance.SENECA_MEADOW.visitedScene = false;
        TransitionData.Instance.SENECA_RADIO_TOWER.visitedScene = false;
        TransitionData.Instance.SENECA_ROAD.visitedScene = false;
        TransitionData.Instance.SENECA_ROCKS.visitedScene = false;

        TransitionData.Instance.UTAN_CAMPSITE.visitedScene = false;
        TransitionData.Instance.UTAN_FARM.visitedScene = false;
        TransitionData.Instance.UTAN_HUNTER_CAMP.visitedScene = false;
        TransitionData.Instance.UTAN_FORK.visitedScene = false;
        TransitionData.Instance.UTAN_MEADOW.visitedScene = false;
        TransitionData.Instance.UTAN_RADIO_TOWER.visitedScene = false;
        TransitionData.Instance.UTAN_ROAD.visitedScene = false;
        TransitionData.Instance.UTAN_ROCKS.visitedScene = false;



    }

	internal override void OnExit()
	{
	}
}
