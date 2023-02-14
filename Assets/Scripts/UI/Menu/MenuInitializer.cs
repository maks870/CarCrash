using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using YG;

public class MenuInitializer : MonoBehaviour
{
    [SerializeField] private List<MenuManager> menuManagers = new List<MenuManager>();
    [SerializeField] private EarningUIController earningUIController;
    [SerializeField] private SoundController soundController;
    [SerializeField] private StartingTraining startingTraining;
    [SerializeField] private AssetReference assetReference;

    private bool isInitializeProcess = false;
    private void OnEnable()
    {
        //SOLoader.EndLoadingEvent += StartInitialize;
        YandexGame.GetDataEvent += StartInitialize;
        YandexGame.EndDataLoadingEvent += soundController.Initialize;
    }

    private void OnDisable()
    {
        //SOLoader.EndLoadingEvent -= StartInitialize;
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

        SOLoader.LoadAsset<Material>(assetReference, (result) => Debug.Log(result.name));

        for (int i = 0; i < menuManagers.Count; i++)
        {
            if (menuManagers[i].GetType() == typeof(MainMenuManager))
                mainMenu = menuManagers[i];

            if (YandexGame.savesData.isFirstSession2)
                menuManagers[i].SaveDefaultSO();

            menuManagers[i].SOLoaderInitialize();
        }

        if (YandexGame.savesData.isFirstSession2)
        {
            YandexGame.savesData.playerWrapper.lastMap = "";
            YandexGame.savesData.playerWrapper.lastMapPlaces.Clear();
            YandexGame.savesData.isFirstSession2 = false;
            startingTraining.StartTraining();
        }

        //for (int i = 0; i < menuManagers.Count; i++)
        //{
        //    menuManagers[i].OpenMenu();

        //    if (menuManagers[i] != mainMenu)
        //        menuManagers[i].objectUI.SetActive(false);
        //}

        MainMenuManager newMainMenu = (MainMenuManager)mainMenu;
        newMainMenu.AddDataLoadingListener();
        earningUIController.UpdateEarnings();
        SceneTransition.instance.EndPreload();
    }

    public void ResetProgressTEST()//�������� �����
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();

        StartInitialize();
    }
}
