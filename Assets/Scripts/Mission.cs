using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Mission : MonoBehaviour
{
    [SerializeField] private MapSO map;
    [SerializeField] public GameObject goalText;
    private Dialogue dialogue;
    private ActionZone missionZone;

    public MapSO Map => map;
    public StartingTraining StartingTraining => dialogue;
    public Transform MissionZoine => missionZone.transform;

    private void Start()
    {
        dialogue = GetComponent<Dialogue>();
        missionZone = GetComponent<ActionZone>();
        missionZone.StayZoneEvent.AddListener(dialogue.StartTraining);
        dialogue.EndDialogueAction += StartMap;
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
