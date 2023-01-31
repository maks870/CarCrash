using System.Collections.Generic;
using UnityEngine;
using YG;

public class MenuInitializer : MonoBehaviour
{
    [SerializeField] private List<MenuManager> menuManagers = new List<MenuManager>();
    [SerializeField] private EarningUIController earningUIController;
    [SerializeField] private SoundController soundController;
    [SerializeField] private StartingTraining startingTraining;
    private bool isInitializeProcess = false;
    private void OnEnable()
    {
        YandexGame.GetDataEvent += StartInitialize;
        YandexGame.EndDataLoadingEvent += soundController.Initialize;
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
        MenuManager mainMenu = menuManagers[0];

        for (int i = 0; i < menuManagers.Count; i++)
        {
            if (menuManagers[i].GetType() == typeof(MainMenuManager))
                mainMenu = (MainMenuManager)menuManagers[i];

            if (YandexGame.savesData.isFirstSession2)
                menuManagers[i].SaveDefaultSO();
        }

        if (YandexGame.savesData.isFirstSession2)
        {
            YandexGame.savesData.playerWrapper.lastMap = "";
            YandexGame.savesData.playerWrapper.lastMapPlaces.Clear();
            YandexGame.savesData.isFirstSession2 = false;
            startingTraining.StartTraining();
        }

        for (int i = 0; i < menuManagers.Count; i++)
        {
            menuManagers[i].OpenMenu();

            if (menuManagers[i] != mainMenu)
                menuManagers[i].objectUI.SetActive(false);
        }

        earningUIController.UpdateEarnings();
        SceneTransition.instance.EndPreload();
    }
    public void ResetProgressTEST()//тестовый метод
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();

        StartInitialize();
    }
}
