using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private List<Mission> missions = new List<Mission>();
    [SerializeField] private Looker missionPointer;
    private Mission currentMission;

    public void SwitchMissionPointer(bool isEnable)
    {
        missionPointer.gameObject.SetActive(isEnable);
    }

    private void Start()
    {
        InitializeMissions();
        InitializeCurrentMission();
        SwitchMissionPointer(true);
    }

    private void InitializeMissions()
    {
        foreach (Mission mission in missions)
        {
            mission.gameObject.SetActive(true);
            mission.Dialogue.EndDialogueAction += NextMission;
            mission.Initialize();
            mission.gameObject.SetActive(false);
        }

    }

    public void InitializeCurrentMission()
    {
        int currentMissionIndex = YandexGame.savesData.currentMission;
        currentMission = missions[currentMissionIndex];
        missionPointer.target = currentMission.MissionZone;
        currentMission.gameObject.SetActive(true);

    }

    public void NextMission()
    {
        currentMission.gameObject.SetActive(false);
        YandexGame.savesData.currentMission += 1;
        YandexGame.SaveProgress();
        InitializeCurrentMission();
    }
}
