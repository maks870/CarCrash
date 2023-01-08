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

        if (YandexGame.savesData.isFirstSession2)
            YandexGame.savesData.isFirstSession2 = false;

        mainMenu.InitializeMenu();
    }
}
