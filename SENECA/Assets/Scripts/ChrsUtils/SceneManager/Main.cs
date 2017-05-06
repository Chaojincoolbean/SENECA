using UnityEngine;
using UnityEngine.Assertions;
using ChrsUtils.ChrsEventSystem.EventsManager;
using GameSceneManagerSystem;
using PrefabDataBase;

public class Main : MonoBehaviour
{
    private void Awake()
    {
        Assert.raiseExceptions = true;

        Services.Prefabs = Resources.Load<PrefabDB>("Prefabs/PrefabDB");
        Services.Events = new GameEventsManager();
		Services.Scenes = new GameSceneManager<TransitionData>(gameObject, Services.Prefabs.Scenes);

		Services.Scenes.PushScene<TitleSceneScript>();
    }
}
