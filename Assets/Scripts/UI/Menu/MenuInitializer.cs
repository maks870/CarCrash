using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MenuInitializer : MonoBehaviour
{
    [SerializeField] private List<MenuManager> menuManagers = new List<MenuManager>();
    private bool isInitializeProcess = false;
    private void OnEnable()
    {
        YandexGame.GetDataEvent += StartInitialize;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= StartInitialize;
    }

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
            StartInitialize();
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

        Debug.Log("ISFIRSTSESSION " + YandexGame.savesData.isFirstSession2);
        Debug.Log("LastMap " + YandexGame.savesData.playerWrapper.lastMap);
        Debug.Log("LastMapplacesCount " + YandexGame.savesData.playerWrapper.lastMapPlaces.Count);

        if (YandexGame.savesData.isFirstSession2)
        {
            YandexGame.savesData.playerWrapper.lastMap = "";
            YandexGame.savesData.playerWrapper.lastMapPlaces.Clear();
            YandexGame.savesData.isFirstSession2 = false;
        }

        for (int i = 0; i < menuManagers.Count; i++)
        {
            menuManagers[i].OpenMenu();
            if (menuManagers[i] != mainMenu)
                menuManagers[i].objectUI.SetActive(false);
        }

        SceneTransition.instance.EndPreload();
    }
}
