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
		set{ }
	}

    public TitleScreen TITLE;
    public PrologueScreen PROLOUGE;

    public SenecaCampsite SENECA_CAMPSITE;
    public SenecaFork SENECA_FORK;
    public SenecaFarm SENECA_FARM;
    public SenecaHunterCamp SENECA_HUNTER_CAMP;
    public SenecaRocks SENECA_ROCKS;
    public SenecaRadioTower SENECA_RADIO_TOWER;
    public SenecaRoad SENECA_ROAD;
    public SenecaMeadow SENECA_MEADOW;

    public UtanCampsite UTAN_CAMPSITE;
    public UtanFork UTAN_FORK;
    public UtanFarm UTAN_FARM;
    public UtanHunterCamp UTAN_HUNTER_CAMP;
    public UtanRocks UTAN_ROCKS;
    public UtanRadioTower UTAN_RADIO_TOWER;
    public UtanRoad UTAN_ROAD;
    public UtanMeadow UTAN_MEADOW;


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
       else if (scene == "Seneca_Farm")
       {
           SENECA_FARM.position = _position;
           SENECA_FARM.scale = _scale;
       }
       else if (scene == "Seneca_HunterCamp")
       {
           SENECA_HUNTER_CAMP.position = _position;
           SENECA_HUNTER_CAMP.scale = _scale;
       }
       else if (scene == "Seneca_Rocks")
       {
           SENECA_ROCKS.position = _position;
           SENECA_ROCKS.scale = _scale;
       }
       else if (scene == "Seneca_Road")
       {
           SENECA_ROAD.position = _position;
           SENECA_ROAD.scale = _scale;
       }
       else if (scene == "Seneca_Radio")
       {
           SENECA_RADIO_TOWER.position = _position;
           SENECA_RADIO_TOWER.scale = _scale;
       }
       else if(scene == "Seneca_Meadow")
       {
           SENECA_MEADOW.position = _position;
           SENECA_MEADOW.scale = _scale;
       }
        else if (scene == "Utan_Campsite")
        {
            UTAN_CAMPSITE.position = _position;
            UTAN_CAMPSITE.scale = _scale;
        }
        else if (scene == "Utan_Fork")
        {
            UTAN_FORK.position = _position;
            UTAN_FORK.scale = _scale;
        }
        else if (scene == "Utan_Farm")
        {
            UTAN_FARM.position = _position;
            UTAN_FARM.scale = _scale;
        }
        else if (scene == "Utan_HunterCamp")
        {
            UTAN_HUNTER_CAMP.position = _position;
            UTAN_HUNTER_CAMP.scale = _scale;
        }
        else if (scene == "Utan_Rocks")
        {
            UTAN_ROCKS.position = _position;
            UTAN_ROCKS.scale = _scale;
        }
        else if (scene == "Utan_Road")
        {
            UTAN_ROAD.position = _position;
            UTAN_ROAD.scale = _scale;
        }
        else if (scene == "Utan_Radio")
        {
            UTAN_RADIO_TOWER.position = _position;
            UTAN_RADIO_TOWER.scale = _scale;
        }
        else if (scene == "Utan_Meadow")
        {
            UTAN_MEADOW.position = _position;
            UTAN_MEADOW.scale = _scale;
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

    public struct SenecaFarm
    {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
    }

    public struct SenecaHunterCamp
    {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
    }

    public struct SenecaRocks
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

    public struct SenecaRadioTower
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

    public struct UtanCampsite
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

    public struct UtanFarm
    {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
    }

    public struct UtanHunterCamp
    {
        public bool visitedScene;
        public Vector3 position;
        public Vector3 scale;
    }

    public struct UtanRocks
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

    public struct UtanRadioTower
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
}