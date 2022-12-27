using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private MapSwitcher mapSwitcher;
    [SerializeField] private PlayerLoad playerLoad;
    [SerializeField] private GameObject objectUI;

    [SerializeField] private List<GameObject> gamemodePanels;

    private bool isInitializeProcess = false;
    private bool isStartLoad = true;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += InitializeMenu;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= InitializeMenu;
    }

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
            InitializeMenu();
    }

    private void SetSavedSO()
    {
        mapSwitcher.InitializeUI();
        isInitializeProcess = false;
    }

    private void SaveDefaultSO()
    {
        MapSO map = playerLoad.DefaultMap;
        MapInfo mapInfo = new MapInfo(map.Name);
        YandexGame.savesData.playerWrapper.maps.Add(mapInfo);
        YandexGame.savesData.isFirstSession2 = false;

        YandexGame.SaveProgress();
    }

    public void InitializeMenu()
    {
        if (isInitializeProcess)
            return;

        isInitializeProcess = true;

        if (YandexGame.savesData.isFirstSession2)
            SaveDefaultSO();

        SetSavedSO();

        if (!isStartLoad)
            return;

        for (int i = 0; i < gamemodePanels.Count; i++)
        {
            gamemodePanels[i].SetActive(false);
        }

        isStartLoad = false;
    }

}
