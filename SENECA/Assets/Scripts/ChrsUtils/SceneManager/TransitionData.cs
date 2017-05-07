using System.Collections.Generic;
using UnityEngine;

public class TransitionData
{
	private static TransitionData instance;
	public static TransitionData Instance {
		get {
			if (instance == null)
				instance = new TransitionData ();
			
				return instance;
		}
		private set{ }
	}

    public TitleScreen TITLE;
    public PrologueScreen PROLOUGE;
    public SenecaCampsite SENECA_CAMPSITE;
    public SenecaFork SENECA_FORK;
    public SenecaMeadow SENECA_MEADOW;
    public SenecaRoad SENECA_ROAD;
    public UtanMeadow UTAN_MEADOW;
    public UtanRoad UTAN_ROAD;
    public UtanFork UTAN_FORK;


    public readonly Dictionary<string, bool> difficulty;
    public readonly int score;

	public TransitionData()
	{
	}

    public TransitionData(string scene, Vector3 _position, Vector3 _scale)
    {
       if (scene == "_TitleScreen")
       {
           TITLE.position = _position;
           TITLE.scale = _scale;
       }
       else if (scene == "_Prolouge")
       {
           PROLOUGE.position = _position;
           PROLOUGE.scale = _scale;
       }
       else if (scene == "Seneca_Campsite")
       {
           SENECA_CAMPSITE.position = _position;
           SENECA_CAMPSITE.scale = _scale;
       }
       else if (scene == "Seneca_Fork")
       {
           SENECA_FORK.position = _position;
           SENECA_FORK.scale = _scale;
       }
       else if(scene == "Seneca_Meadow")
       {
           SENECA_MEADOW.position = _position;
           SENECA_MEADOW.scale = _scale;
       }
       else if(scene == "Seneca_Road")
       {
           SENECA_ROAD.position = _position;
           SENECA_ROAD.scale = _scale;
       }
       else if(scene == "Utan_Meadow")
       {
           UTAN_MEADOW.position = _position;
           UTAN_MEADOW.scale = _scale;
       }
       else if(scene == "Utan_Road")
       {
           UTAN_ROAD.position = _position;
           UTAN_ROAD.scale = _scale;
       }
       else if(scene == "Utan_Fork")
       {
           UTAN_FORK.position = _position;
           UTAN_FORK.scale = _scale;
       }
    }

    public struct TitleScreen
     {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
     }

     public struct PrologueScreen
     {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
     }

     public struct SenecaCampsite
     {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
     }

     public struct SenecaFork
     {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
     }

     public struct SenecaMeadow
     {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
     }

     public struct SenecaRoad
     {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
     }

     public struct UtanMeadow
     {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
     }

     public struct UtanRoad
     {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
     }

     public struct UtanFork
     {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
     }
}