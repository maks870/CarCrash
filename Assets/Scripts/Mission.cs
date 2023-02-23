using System.Collections.Generic;
using UnityEngine;
using YG;

public class Mission : MonoBehaviour
{
    [SerializeField] private MapSO map;
    [HideInInspector] public GameObject goalText;
    [HideInInspector] public GameObject endGoalText;
    private Transform camTransform;
    private Transform lookAtTransform;
    private Dialogue dialogue;
    private ActionZone missionZone;

    public MapSO Map => map;
    public Dialogue Dialogue => dialogue;
    public ActionZone MissionZone => missionZone;

    private void Awake()
    {
        camTransform = transform.GetChild(0);
        lookAtTransform = transform.GetChild(1);
    }

    public void Initialize()
    {
        dialogue = GetComponent<Dialogue>();
        missionZone = GetComponent<ActionZone>();
        DialogueCameraChanger dialogueCameraChanger = FindObjectOfType<DialogueCameraChanger>();

        missionZone.StayZoneEvent.AddListener(() => goalText.SetActive(false));
        missionZone.StayZoneEvent.AddListener(() => dialogueCameraChanger.SetDialogueTarget(camTransform, lookAtTransform));
        missionZone.StayZoneEvent.AddListener(() => GetComponent<MeshRenderer>().enabled = false);
        missionZone.StayZoneEvent.AddListener(dialogue.StartDialogue);
        dialogue.EndDialogueAction += () => missionZone.SwitchCarControl(false);
        dialogue.EndDialogueAction += dialogueCameraChanger.ResetDialogueTarget;

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
