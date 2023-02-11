using System.Collections.Generic;
using UnityEngine;
using YG;

public class Mission : MonoBehaviour
{
    [SerializeField] private MapSO map;
    [HideInInspector] public GameObject goalText;
    [HideInInspector] public GameObject endGoalText;
    private Dialogue dialogue;
    private ActionZone missionZone;

    public MapSO Map => map;
    public Dialogue Dialogue => dialogue;
    public Transform MissionZone => missionZone.transform;

    public void Initialize()
    {
        dialogue = GetComponent<Dialogue>();
        missionZone = GetComponent<ActionZone>();
        dialogue.EndDialogueAction += () => missionZone.SwitchCarControl(false);
        missionZone.StayZoneEvent.AddListener(() => goalText.SetActive(false));
        missionZone.ExitZoneEvent.AddListener(() => goalText.SetActive(true));
        missionZone.StayZoneEvent.AddListener(dialogue.StartDialogue);
        if (map != null)
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
