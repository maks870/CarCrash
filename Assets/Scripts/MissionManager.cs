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
        InitializeMission();
        SwitchMissionPointer(true);
    }

    public void InitializeMission()
    {
        int currentMissionIndex = YandexGame.savesData.currentMission;
        currentMission = missions[currentMissionIndex];
        missionPointer.target = currentMission.MissionZoine;
        currentMission.goalText.SetActive(true);

    }

    public void NextMission()
    {
        currentMission.goalText.SetActive(false);
        YandexGame.savesData.currentMission += 1;
        YandexGame.SaveProgress();
        InitializeMission();
    }
}
