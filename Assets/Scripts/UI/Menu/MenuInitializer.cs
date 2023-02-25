using System.Collections.Generic;
using UnityEngine;
using YG;

public class MenuInitializer : MonoBehaviour
{
    [SerializeField] private GameObject startRoom;
    [SerializeField] private List<MenuManager> menuManagers = new List<MenuManager>();
    [SerializeField] private EarningUIController earningUIController;
    [SerializeField] private SoundController soundController;
    [SerializeField] private StartingTraining startingTraining;

    private bool isInitializeProcess = false;
    private void OnEnable()
    {
        YandexGame.GetDataEvent += StartInitialize;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= StartInitialize;
        YandexGame.EndDataLoadingEvent -= soundController.Initialize;
    }

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            YandexGame.LoadProgress();
        }
    }

    private void StartInitialize()
    {
        if (isInitializeProcess)
            return;

        isInitializeProcess = true;

        startRoom.SetActive(true);
        YandexGame.EndDataLoadingEvent += soundController.Initialize;

        MenuManager mainMenu = menuManagers[0];

        for (int i = 0; i < menuManagers.Count; i++)
        {
            if (menuManagers[i].GetType() == typeof(MainMenuManager))
                mainMenu = menuManagers[i];

            if (YandexGame.savesData.isFirstSession2)
                menuManagers[i].SaveDefaultSO();

            menuManagers[i].SOLoaderSubscribe();
        }

        if (!YandexGame.savesData.playerWrapper.careerIsEnded && YandexGame.savesData.playerWrapper.maps[YandexGame.savesData.playerWrapper.maps.Count - 1].isPassed)
            YandexGame.savesData.playerWrapper.newMission = true;

        MainMenuManager newMainMenu = (MainMenuManager)mainMenu;
        newMainMenu.PlayerLoad.SOLoaderSubscribe();

        if (!SOLoader.instance.IsResourcesLoaded)
        {
            SOLoader.instance.EndLoadingEvent += newMainMenu.GetAwardsAfterMap;
        }
        else
        {
            YandexGame.EndDataLoadingEvent += newMainMenu.GetAwardsAfterMap;
            newMainMenu.SubYandexLoad = true;
        }

        SOLoader.instance.LoadAll();

        if (YandexGame.savesData.isFirstSession2)
        {
            YandexGame.savesData.playerWrapper.lastMap = "";
            YandexGame.savesData.playerWrapper.lastMapPlaces.Clear();
            YandexGame.savesData.isFirstSession2 = false;
            startingTraining.StartTraining();
        }

        earningUIController.UpdateEarnings();
        newMainMenu.UpdateNewPossibilitiyWarnings();
        SceneTransition.instance.EndPreload();
    }

    public void ResetProgressTEST()//�������� �����
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();

        StartInitialize();
    }
}
