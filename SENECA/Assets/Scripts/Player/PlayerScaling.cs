using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScenes;
using ChrsUtils.ChrsExtensionMethods;

public class PlayerScaling : MonoBehaviour 
{
	public class ScaleBound
	{
		public float lowerBound;
		public float upperBound;
		public float lowerBoundScale;
		public float upperBoundScale;

		public ScaleBound()
		{
		}

		public ScaleBound(float _lowerBound, float _upperBound, float _lowerBoundScale, float _upperBoundScale)
		{
			lowerBound = _lowerBound;
			upperBound = _upperBound;
			lowerBoundScale = _lowerBoundScale;
			upperBoundScale = _upperBoundScale;
		}

		public float LerpBoundsMod(float t)
		{
			return (t - lowerBound) / (upperBound - lowerBound);
		}

		public float LerpScaleMod(float t)
		{
			return (t - lowerBoundScale) / (upperBoundScale - lowerBoundScale);
		}
	}

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

	public float faceLeft = 1.0f;
	public string currentScene;
	public float currentPlayerPosY;
	public float newPlayerPosY;
	public Scene<TransitionData> thisScene;
	public Transform player;
	public GameObject root;

	public Dictionary<string, ScaleBound[]> scalingReference;

	public delegate void ScalePlayer(Transform player);

	ScalePlayer currentScaleCurve;

	// Use this for initialization
	void Start () 
	{
		root = GameObject.Find ("Root");

		scalingReference = new Dictionary<string, ScaleBound[]> ();

		scalingReference.Add (CAMPSITE,new ScaleBound[4]);
		scalingReference.Add (FARM, new ScaleBound[10]);
		scalingReference.Add (FORK, new ScaleBound[5]);
		scalingReference.Add (HUNTER, new ScaleBound[2]);
		scalingReference.Add (MEADOW, new ScaleBound[4]);
		scalingReference.Add (TOWER, new ScaleBound[1]);
		scalingReference.Add (ROAD, new ScaleBound[5]);
		scalingReference.Add (ROCK, new ScaleBound[1]);
		PopulateScaleBounds ();

		newPlayerPosY = 0;
		currentScaleCurve = DefaultScalingCurve;
	}

	void PopulateScaleBounds()
	{
		scalingReference [CAMPSITE] [0] = new ScaleBound (-21f, -13f, 1.5f, 1f);
		scalingReference [CAMPSITE] [1] = new ScaleBound (-12f, -5.8f, 1f, 0.7f);
		scalingReference [CAMPSITE] [2] = new ScaleBound (-5.2f, -3, 0.7f, 0.48f);
		scalingReference [CAMPSITE] [3] = new ScaleBound (-2.2f, -0.2f, 0.48f, 0.34f);

		scalingReference [FARM] [0] = new ScaleBound (-25.11f, -20.92f, 1.5f, 1.3f);
		scalingReference [FARM] [1] = new ScaleBound (-20.42f, -16.72f, 1.3f, 1.1f);
		scalingReference [FARM] [2] = new ScaleBound (-16.22f, -13.5f, 1.1f, 1f);
		scalingReference [FARM] [3] = new ScaleBound (-13.0f, -9.97f, 0.85f, 0.71f);
		scalingReference [FARM] [4] = new ScaleBound (-9.47f, -7.88f, 0.71f, 0.63f);
		scalingReference [FARM] [5] = new ScaleBound (-7.38f, -6.5f, 1f, 0.7f);
		scalingReference [FARM] [6] = new ScaleBound (-6f, -5.84f, 0.63f, 0.59f);
		scalingReference [FARM] [7] = new ScaleBound (-5.54f, -4.38f, 0.59f, 0.43f);
		scalingReference [FARM] [8] = new ScaleBound (-4.28f, -3.7f, 0.43f, 0.31f);
		scalingReference [FARM] [9] = new ScaleBound (-3.5f, -3f, 31f, 0.27f);

		scalingReference [FORK] [0] = new ScaleBound (-11.61f, -4.53f, 0.82f, 0.55f);
		scalingReference [FORK] [1] = new ScaleBound (-4.03f, -2.38f, 0.55f, 0.4f);
		scalingReference [FORK] [2] = new ScaleBound (-1.98f, -1.34f, 0.4f, 0.33f);
		scalingReference [FORK] [3] = new ScaleBound (-0.94f, -0.5f, 0.33f, 0.3f);
		scalingReference [FORK] [4] = new ScaleBound (-0.39f, 0.14f, 0.3f, 0.27f);

		scalingReference [HUNTER] [0] = new ScaleBound (-3.71f, -2.87f, 0.48f, 0.34f);
		scalingReference [HUNTER] [1] = new ScaleBound (-2.47f, -2.0f, 0.34f, 0.3f);

		scalingReference [MEADOW] [0] = new ScaleBound (-11.86f, -3.58f, 0.5f, 0.23f);
		scalingReference [MEADOW] [1] = new ScaleBound (-3.28f, -2.62f, 0.23f, 0.22f);
		scalingReference [MEADOW] [2] = new ScaleBound (-2.42f, -1.64f, 0.22f, 0.18f);
		scalingReference [MEADOW] [3] = new ScaleBound (-1.44f, -0.58f, 0.18f, 0.14f);

		scalingReference [TOWER] [0] = new ScaleBound (-5.45f, -3f, 0.1f, 0.07f);

		scalingReference [ROAD] [0] = new ScaleBound (-11f, -8.0f, 0.7f, 0.6f);
		scalingReference [ROAD] [1] = new ScaleBound (-7.5f, -4.75f, 0.6f, 0.53f);
		scalingReference [ROAD] [2] = new ScaleBound (-4.25f, -1.5f, 0.53f, 0.4f);
		scalingReference [ROAD] [3] = new ScaleBound (-1.25f, -0.68f, 0.4f, 0.35f);
		scalingReference [ROAD] [4] = new ScaleBound (-0.19f, 1.12f, 0.35f, 0.32f);

		scalingReference [ROCK] [0] = new ScaleBound (-3.23f, -3.23f, 0.43f, 0.43f);
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

		if (player.GetComponent<Player> ().facingLeft)
		{
			faceLeft = 1.0f;
		} 
		else 
		{
			faceLeft = 1.0f;
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

		if (newPlayerPosY != currentPlayerPosY) 
		{
			currentScaleCurve (player);
		}

		newPlayerPosY = currentPlayerPosY;
	}
		
	void CalculateScale(string currentScene, Transform player)
	{
		for (int i = 0; i < scalingReference [currentScene].Length; i++) 
		{
			if (player.position.y < scalingReference [currentScene] [0].lowerBound) 
			{
				player.localScale = ExtensionMethods.CreateVector3 (scalingReference [currentScene] [0].lowerBoundScale);
			}
			else if (scalingReference [currentScene] [i].lowerBound < player.position.y && player.position.y < scalingReference [currentScene] [i].upperBound) 
			{
				player.localScale = Vector3.Lerp(ExtensionMethods.CreateVector3(scalingReference[currentScene][i].lowerBoundScale), 
												 ExtensionMethods.CreateVector3(scalingReference[currentScene][i].upperBoundScale), 
												 scalingReference[currentScene][i].LerpBoundsMod(player.position.y));
			}
			else if (scalingReference [currentScene] [scalingReference[currentScene].Length - 1].upperBound < player.position.y)
			{
				player.localScale = ExtensionMethods.CreateVector3 (scalingReference [currentScene] [scalingReference[currentScene].Length - 1].upperBoundScale);
			}

			player.localScale = new Vector3 (player.localScale.x * faceLeft, player.localScale.y, player.localScale.z);
		}
	}

	void DefaultScalingCurve(Transform player)
	{
		
	}

	void CampsiteScalingCurve(Transform player)
	{
		CalculateScale (CAMPSITE, player);
	}

	void ForkScalingCurve(Transform player)
	{
		CalculateScale (FORK, player);
	}

	void FarmScalingCurve(Transform player)
	{
		CalculateScale (FARM, player);
	}
		
	void HunterScalingCurve(Transform player)
	{
		CalculateScale (HUNTER, player);
	}
		
	void MeadowScalingCurve(Transform player)
	{
		CalculateScale (MEADOW, player);
	}

	void TowerScalingCurve(Transform player)
	{
		CalculateScale (TOWER, player);
	}

	void RoadScalingCurve(Transform player)
	{
		CalculateScale (ROAD, player);
	}

	void RockScalingCurve(Transform player)
	{
		CalculateScale (ROCK, player);
	}
}
