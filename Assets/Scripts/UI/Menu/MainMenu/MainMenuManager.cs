using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MainMenuManager : MenuManager
{
    [SerializeField] private MapSwitcher mapSwitcher;
    [SerializeField] private AwardPresenter presenter;
    [SerializeField] private List<GameObject> gamemodePanels;

    private void SetSavedSO()
    {
        mapSwitcher.InitializeUI();
    }

    public override void SaveDefaultSO()
    {
        MapSO map = playerLoad.DefaultMap;
        Debug.Log(map.Name + " savedMap");
        MapInfo mapInfo = new MapInfo(map.Name);
        YandexGame.savesData.playerWrapper.maps.Add(mapInfo);
    }

    public override void OpenMenu()
    {
        base.OpenMenu();
        InitializeMenu();
    }

    public override void InitializeMenu()
    {
        Debug.Log("InitializeMainMenu");
        for (int i = 0; i < gamemodePanels.Count; i++)
        {
            gamemodePanels[i].SetActive(true);
        }

        string lastMapName = YandexGame.savesData.playerWrapper.lastMap;
        MapInfo mapInfo = YandexGame.savesData.playerWrapper.GetMapInfo(lastMapName);

        if (YandexGame.savesData.playerWrapper.lastMap != "" && YandexGame.savesData.playerWrapper.lastPlayedPlace != 0)
        {
            YandexGame.EndDataLoadingEvent += GetAwardsAfterMap;
        }

        SetSavedSO();

        for (int i = 0; i < gamemodePanels.Count; i++)
        {
            gamemodePanels[i].SetActive(false);
        }
    }

    private void GetAwardsAfterMap()
    {
        presenter.GetAward();
        YandexGame.savesData.playerWrapper.lastMap = "";
        YandexGame.savesData.playerWrapper.lastPlayedPlace = 0;
        YandexGame.SaveProgress();
        YandexGame.EndDataLoadingEvent -= GetAwardsAfterMap;
    }


}
