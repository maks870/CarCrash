using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private List<Mission> missions = new List<Mission>();
    [SerializeField] private Looker missionPointer;
    private Mission currentMission;
    private bool isPointerEnabled = false;
    public bool IsPointerEnabled { set => isPointerEnabled = value; }

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

    }

    public void NextMission()
    {
        YandexGame.savesData.currentMission += 1;
        YandexGame.SaveProgress();
        InitializeMission();
    }
}
