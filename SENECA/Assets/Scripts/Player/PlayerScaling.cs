using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;

public class PlayerScaling : MonoBehaviour 
{
	public const float MAX_SCALE = 1.5f;
	public const float MIN_SCALE = 0.07f;
	public const string DEFAULT = "";
	public const string CAMPSITE = "Campsite";
	public const string FARM = "Farm";
	public const string FORK = "Fork";
	public const string HUNTER = "Hunter";
	public const string MEADOW = "Meadow";
	public const string TOWER = "Tower";
	public const string ROAD = "Road";
	public const string ROCK = "Rock";

	public float nextTimeToSearch = 0;

	public string currentScene;
	public float scaleMod;
	public float currentScale;
	public float currentPlayerPosY;
	public float newPlayerPosY;
	public float deltaScale;
	public Scene<TransitionData> thisScene;
	public Transform player;
	public GameObject root;

	public delegate void ScalePlayer(Transform player);

	ScalePlayer currentScaleCurve;

	// Use this for initialization
	void Start () 
	{
		root = GameObject.Find ("Root");

		newPlayerPosY = 0;
		currentScale = 1.0f;
		currentScaleCurve = DefaultScalingCurve;
		deltaScale = 0.0f;
	}

	void FindPlayer()
	{
		if (nextTimeToSearch <= Time.time)
		{
			GameObject result = GameObject.FindGameObjectWithTag ("Player");
			if (result != null)
			{
				player = result.transform;
				currentPlayerPosY = player.transform.position.y;
			}
			nextTimeToSearch = Time.time + 2.0f;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player == null) 
		{
			FindPlayer ();
			return;
		}

		currentScene = root.transform.GetChild (0).tag;

		if (currentScene.Contains (CAMPSITE)) 
		{
			// campsite scaling
			currentScaleCurve = CampsiteScalingCurve;
		} 
		else if (currentScene.Contains (FORK)) 
		{

			//	fork scaling
			currentScaleCurve = ForkScalingCurve;
		}
		else if (currentScene.Contains(FARM))
		{
			// farm scaling
			currentScaleCurve = FarmScalingCurve;
		} 
		else if (currentScene.Contains(HUNTER))
		{
			// hunter scaling
			currentScaleCurve = HunterScalingCurve;
		} 
		else if (currentScene.Contains(MEADOW))
		{
			// meadow scaling
			currentScaleCurve = MeadowScalingCurve;
		} 
		else if (currentScene.Contains(TOWER))
		{
			//	tower scaling
			currentScaleCurve = TowerScalingCurve;
		} 
		else if (currentScene.Contains(ROAD))
		{
			//	road scaling
			currentScaleCurve = RoadScalingCurve;
		} 
		else if (currentScene.Contains(ROCK))
		{
			// rock scaling
			currentScaleCurve = RoadScalingCurve;
		} 
		else 
		{
			//	default scaling
			currentScaleCurve = DefaultScalingCurve;
		}

		currentPlayerPosY = player.position.y;

		if (currentPlayerPosY < newPlayerPosY) 
		{
			scaleMod = -1.0f;
		} 
		else 
		{
			scaleMod = 1.0f;
		}

		if (newPlayerPosY != currentPlayerPosY) 
		{
			currentScaleCurve (player);
		}

		newPlayerPosY = currentPlayerPosY;
	}

	float CalculateDeltaScale(Vector2 pointA, Vector2 pointB)
	{
		float result = (pointB.y - pointA.y) / (pointB.x - pointA.x);

		if (float.IsNaN (result)) 
		{
			result = 0;
		}

		return result * scaleMod;
	}

	void DefaultScalingCurve(Transform player)
	{
		deltaScale = 0.0f;
	}

	void CampsiteScalingCurve(Transform player)
	{
		Vector2 lowerLimit = new Vector2(1.5f, -21.0f);
		Vector2 tier1 = new Vector2(1.0f, -12.0f);
		Vector2 tier2 = new Vector2(0.7f, -5.7f);
		Vector2 tier3 = new Vector2(0.48f, -2.2f);
		Vector2 upperLimit = new Vector2(0.34f, 0.2f);

		if (player.localPosition.y < lowerLimit.y) 
		{
			deltaScale = CalculateDeltaScale (lowerLimit, lowerLimit) * player.position.y;
			player.localScale = new Vector3 (lowerLimit.x, lowerLimit.x, lowerLimit.x);
		} 
		else if (lowerLimit.y < player.localPosition.y && player.localPosition.y < tier1.y) 
		{
			//	calcualte growth scale
			deltaScale =  player.position.y / CalculateDeltaScale(lowerLimit, tier1);
			//	find player's posiiton on scale
			//	set scale
			currentScale = player.localScale.y + deltaScale;
			player.localScale = new Vector3(tier1.x, tier1.x,tier1.x);
		}
		else if (tier1.y < player.localPosition.y && player.localPosition.y < tier2.y) 
		{
			//	calcualte growth scale
			deltaScale = player.position.y / CalculateDeltaScale(tier1, tier2);
			//	find player's posiiton on scale
			//	set scale
			currentScale = player.localScale.y + deltaScale;

			player.localScale = new Vector3(tier2.x, tier2.x,tier2.x);
		}
		else if (tier2.y < player.localPosition.y && player.localPosition.y < tier3.y) 
		{
			//	calcualte growth scale
			deltaScale = player.position.y / CalculateDeltaScale(tier2, tier3);
			//	find player's posiiton on scale
			//	set scale
			currentScale = player.localScale.y + deltaScale;

			player.localScale = new Vector3(tier3.x, tier3.x,tier3.x);
		}
		else if (tier3.y < player.localPosition.y && player.localPosition.y < upperLimit.y) 
		{
			//	calcualte growth scale
			deltaScale = player.position.y / CalculateDeltaScale(tier3, upperLimit);
			//	find player's posiiton on scale
			//	set scale
			currentScale = player.localScale.y + deltaScale;

			player.localScale = new Vector3(tier3.x, tier3.x,tier3.x);
		}
		else if (player.localPosition.y > upperLimit.y) 
		{
			//	find player's posiiton on scale
			//	set scale
			deltaScale = CalculateDeltaScale(upperLimit, upperLimit);
			player.localScale = new Vector3(upperLimit.x, upperLimit.x, upperLimit.x);
		}


		//player.localScale = new Vector3(currentScale, currentScale,currentScale);
	}

	void ForkScalingCurve(Transform player)
	{
		Vector2 lowerLimit = new Vector2(0.82f, -11.61f);
		Vector2 tier1 = new Vector2(0.55f, -4.03f);
		Vector2 tier2 = new Vector2(0.4f, -1.98f);
		Vector2 tier3 = new Vector2(0.33f, -0.94f);
		Vector2 tier4 = new Vector2(0.3f ,-0.39f);
		Vector2 upperLimit = new Vector2(0.27f, 0.14f);

		if (player.localPosition.y < lowerLimit.y) 
		{
			player.localScale = new Vector3 (lowerLimit.x, lowerLimit.x, lowerLimit.x);
		} 
		else if (lowerLimit.y < player.localPosition.y && player.localPosition.y < tier1.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(lowerLimit, tier1);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier1.y < player.localPosition.y && player.localPosition.y < tier2.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier1, tier2);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier2.y < player.localPosition.y && player.localPosition.y < tier3.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier2, tier3);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier3.y < player.localPosition.y && player.localPosition.y < tier4.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier3, upperLimit);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier4.y < player.localPosition.y && player.localPosition.y < upperLimit.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier3, upperLimit);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (player.localPosition.y > upperLimit.y) 
		{
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(upperLimit.x, upperLimit.x, upperLimit.x);
		}
	}


	void FarmScalingCurve(Transform player)
	{
		Vector2 lowerLimit = new Vector2 (1.5f, -25.11f);
		Vector2 tier1 = new Vector2(1.3f, -20.42f);;
		Vector2 tier2 = new Vector2(1.1f, -16.22f);;
		Vector2 tier3 = new Vector2(1.0f, -13.0f);
		Vector2 tier4 = new Vector2(0.85f, -9.47f);;
		Vector2 tier5 = new Vector2(0.71f, -7.38f);;
		Vector2 tier6 = new Vector2(0.63f, -6.0f);
		Vector2 tier7 = new Vector2(0.59f, -5.54f);;
		Vector2 tier8 = new Vector2(0.43f, -4.28f);;
		Vector2 tier9 = new Vector2(0.31f, -3.5f);
		Vector2 upperLimit = new Vector2 (0.27f, -3.0f);

		if (player.localPosition.y < lowerLimit.y) 
		{
			player.localScale = new Vector3 (lowerLimit.x, lowerLimit.x, lowerLimit.x);
		} 
		else if (lowerLimit.y < player.localPosition.y && player.localPosition.y < tier1.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(lowerLimit, tier1);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier1.y < player.localPosition.y && player.localPosition.y < tier2.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier1, tier2);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier2.y < player.localPosition.y && player.localPosition.y < tier3.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier2, tier3);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier3.y < player.localPosition.y && player.localPosition.y < tier4.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier3, tier4);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier4.y < player.localPosition.y && player.localPosition.y < tier5.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier4, tier5);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier5.y < player.localPosition.y && player.localPosition.y < tier6.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier5, tier6);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier6.y < player.localPosition.y && player.localPosition.y < tier7.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier6, tier7);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier7.y < player.localPosition.y && player.localPosition.y < tier8.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier7, tier8);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier8.y < player.localPosition.y && player.localPosition.y < tier9.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier8, tier9);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier9.y < player.localPosition.y && player.localPosition.y < upperLimit.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier9, upperLimit);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (player.localPosition.y > upperLimit.y) 
		{
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(upperLimit.x, upperLimit.x, upperLimit.x);
		}
	}

	void HunterScalingCurve(Transform player)
	{
		Vector2 lowerLimit = new Vector2(0.48f, -3.71f);
		Vector2 tier1 = new Vector2(0.34f, -2.47f);
		Vector2 upperLimit = new Vector2(0.3f, -1.73f);

		if (player.localPosition.y < lowerLimit.y) 
		{
			player.localScale = new Vector3 (lowerLimit.x, lowerLimit.x, lowerLimit.x);
		} 
		else if (lowerLimit.y < player.localPosition.y && player.localPosition.y < tier1.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(lowerLimit, tier1);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier1.y < player.localPosition.y && player.localPosition.y < upperLimit.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier1, upperLimit);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (player.localPosition.y > upperLimit.y) 
		{
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(upperLimit.x, upperLimit.x, upperLimit.x);
		}
	}

	void MeadowScalingCurve(Transform player)
	{
		Vector2 lowerLimit = new Vector2(0.5f, -11.43f);
		Vector2 tier1 = new Vector2(0.28f, -3.67f);
		Vector2 tier2 = new Vector2(0.22f, -2.42f);
		Vector2 tier3 = new Vector2(0.18f, -1.44f);
		Vector2 upperLimit = new Vector2(0.14f ,0.58f);

		if (player.localPosition.y < lowerLimit.y) 
		{
			player.localScale = new Vector3 (lowerLimit.x, lowerLimit.x, lowerLimit.x);
		} 
		else if (lowerLimit.y < player.localPosition.y && player.localPosition.y < tier1.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(lowerLimit, tier1);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier1.y < player.localPosition.y && player.localPosition.y < tier2.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier1, tier2);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier2.y < player.localPosition.y && player.localPosition.y < tier3.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier2, tier3);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier3.y < player.localPosition.y && player.localPosition.y < upperLimit.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier3, upperLimit);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (player.localPosition.y > upperLimit.y) 
		{
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(upperLimit.x, upperLimit.x, upperLimit.x);
		}
	}

	void TowerScalingCurve(Transform player)
	{
		Vector2 lowerLimit = new Vector2(0.1f, -5.45f);
		Vector2 upperLimit = new Vector2(0.07f, -3.0f);

		if (player.localPosition.y < lowerLimit.y) 
		{
			player.localScale = new Vector3 (lowerLimit.x, lowerLimit.x, lowerLimit.x);
		}
		else if (lowerLimit.y < player.localPosition.y && player.localPosition.y < upperLimit.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(lowerLimit, upperLimit);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (player.localPosition.y > upperLimit.y) 
		{
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(upperLimit.x, upperLimit.x, upperLimit.x);
		}
	}

	void RoadScalingCurve(Transform player)
	{
		Vector2 lowerLimit = new Vector2(0.7f, -11.0f);
		Vector2 tier1 = new Vector2(0.6f, -7.5f);
		Vector2 tier2 = new Vector2(0.53f, -4.25f);
		Vector2 tier3 = new Vector2(0.4f, -1.25f);
		Vector2 tier4 = new Vector2(0.35f, -0.19f);
		Vector2 upperLimit = new Vector2(0.32f, 1.12f);

		if (player.localPosition.y < lowerLimit.y) 
		{
			player.localScale = new Vector3 (lowerLimit.x, lowerLimit.x, lowerLimit.x);
		} 
		else if (lowerLimit.y < player.localPosition.y && player.localPosition.y < tier1.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(lowerLimit, tier1);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier1.y < player.localPosition.y && player.localPosition.y < tier2.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier1, tier2);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier2.y < player.localPosition.y && player.localPosition.y < tier3.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier2, tier3);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier3.y < player.localPosition.y && player.localPosition.y < tier4.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier3, tier4);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (tier4.y < player.localPosition.y && player.localPosition.y < upperLimit.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(tier4, upperLimit);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (player.localPosition.y > upperLimit.y) 
		{
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(upperLimit.x, upperLimit.x, upperLimit.x);
		}
	}

	void RockScalingCurve(Transform player)
	{
		Vector2 upperLimit = new Vector2(0.43f,-3.23f);
		Vector2 lowerLimit =new Vector2(0.43f, -3.23f);

		if (player.localPosition.y < lowerLimit.y) 
		{
			player.localScale = new Vector3 (lowerLimit.x, lowerLimit.x, lowerLimit.x);
		}
		else if (lowerLimit.y < player.localPosition.y && player.localPosition.y < upperLimit.y) 
		{
			//	calcualte growth scale
			deltaScale = CalculateDeltaScale(lowerLimit, upperLimit);
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(player.localScale.y + deltaScale, player.localScale.y + deltaScale, player.localScale.z + deltaScale);
		}
		else if (player.localPosition.y > upperLimit.y) 
		{
			//	find player's posiiton on scale
			//	set scale
			player.localScale = new Vector3(upperLimit.x, upperLimit.x, upperLimit.x);
		}
	}
}
