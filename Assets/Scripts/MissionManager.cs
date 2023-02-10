using UnityEngine;
using YG;

public class MissionManager : MonoBehaviour
{

    [SerializeField] private GameObject missionsObj;
    [SerializeField] private GameObject goalMissionObj;
    [SerializeField] private GameObject endGoalMissionObj;
    [SerializeField] private Looker missionPointer;
    private Mission[] missions;
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
        Mission[] missions = new Mission[missionsObj.transform.childCount];

        for (int i = 0; i < missionsObj.transform.childCount; i++)
        {
            GameObject goalMission = goalMissionObj.transform.GetChild(i).gameObject;
            GameObject missionObj = missionsObj.transform.GetChild(i).gameObject;

            missionObj.SetActive(true);
            missions[i] = missionObj.GetComponent<Mission>();
            missions[i].goalText = goalMission;
            if (missions[i].Map != null)
                missions[i].goalText = goalMission;
        }

        int endGoalCounter = 0;
        foreach (Mission mission in missions)
        {
            mission.Initialize();

            if (mission.Map == null)
                mission.Dialogue.EndDialogueAction += NextMission;
            else
            {
                GameObject endGoalMission = endGoalMissionObj.transform.GetChild(endGoalCounter).gameObject;
                endGoalCounter++;
                mission.endGoalText = endGoalMission;
            }

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
