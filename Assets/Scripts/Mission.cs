using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Mission : MonoBehaviour
{
    [SerializeField] private StartingTraining startingTraining;
    [SerializeField] private ActionZone missionZone;
    [SerializeField] private MapSO map;
    [SerializeField] public GameObject goalText;
    public Transform MissionZoine => missionZone.transform;

    private void Start()
    {
        startingTraining = GetComponent<StartingTraining>();
        missionZone = GetComponentInChildren<ActionZone>();
        missionZone.StayZoneEvent.AddListener(startingTraining.StartTraining);
    }

    public void StartMap()
    {
        List<MapInfo> mapInfos = YandexGame.savesData.playerWrapper.maps;
        bool isMapAdded = false;

        foreach (MapInfo itemName in mapInfos)
        {
            if (itemName.mapName == map.Name)
                isMapAdded = true;
        }

        if (!isMapAdded)
        {
            MapInfo mapInfo = new MapInfo(map.Name, map.MaxPoints);
            YandexGame.savesData.playerWrapper.maps.Add(mapInfo);
        }

        YandexGame.savesData.playerWrapper.lastMap = map.Name;
        YandexGame.SaveProgress();
        SceneTransition.SwitchScene(map.Name);
    }
}
