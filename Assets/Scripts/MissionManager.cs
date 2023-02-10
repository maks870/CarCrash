using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private Mission[] missions;
    [SerializeField] private Looker missionPointer;
    private Mission currentMission;

    public void SwitchMissionPointer(bool isEnable)
    {
        missionPointer.gameObject.SetActive(isEnable);
        Looker looker = missionPointer.GetComponent<Looker>();

        if (looker.target == null)
            missionPointer.gameObject.SetActive(false);
    }

    private void Start()
    {
        InitializeMissions();
        int currentMissionIndex = YandexGame.savesData.currentMission;
        if (currentMissionIndex < missions.Length)
            InitializeCurrentMission();

        SwitchMissionPointer(true);
    }

    private void InitializeMissions()
    {
        foreach (Mission mission in missions)
        {
            mission.gameObject.SetActive(true);
            mission.Initialize();

            if (mission.Map == null)
                mission.Dialogue.EndDialogueAction += NextMission;

            mission.gameObject.SetActive(false);
        }

    }

    private bool CheckActiveMission(int currentMissionIndex)
    {
        bool isActive = false;

        if (missions[currentMissionIndex].Map != null)
        {
            MapInfo[] mapInfos = YandexGame.savesData.playerWrapper.maps.ToArray();
            foreach (MapInfo map in mapInfos)
            {
                if (map.mapName == missions[currentMissionIndex].Map.Name)
                    isActive = true;
            }
        }

        return isActive;
    }

    private void InitializeCurrentMission()
    {
        int currentMissionIndex = YandexGame.savesData.currentMission;
        currentMission = missions[currentMissionIndex];

        if (CheckActiveMission(currentMissionIndex))
        {
            currentMission.endGoalText.SetActive(true);
        }
        else
        {
            missionPointer.target = currentMission.MissionZone;
            currentMission.goalText.SetActive(true);
            currentMission.gameObject.SetActive(true);
        }
    }



    public void NextMission()
    {
        currentMission.gameObject.SetActive(false);
        YandexGame.savesData.currentMission += 1;
        YandexGame.SaveProgress();
        InitializeCurrentMission();
    }
}
