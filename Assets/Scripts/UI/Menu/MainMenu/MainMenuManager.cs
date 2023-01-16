using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MainMenuManager : MenuManager
{
    [SerializeField] private MapSwitcher mapSwitcher;
    [SerializeField] private AwardPresenter presenter;
    [SerializeField] private GameObject newCollectiblesWarning;
    [SerializeField] private List<GameObject> gamemodePanels;

    private void SetSavedSO()
    {
        mapSwitcher.InitializeUI();
    }

    private void GetAwardsAfterMap()
    {
        Debug.Log("RewardAction");
        presenter.GetAward();
        YandexGame.savesData.playerWrapper.lastMap = "";
        YandexGame.savesData.playerWrapper.lastMapPlaces.Clear();
        YandexGame.SaveProgress();
        UpdateNewCollectiblesWarnings();
        YandexGame.EndDataLoadingEvent -= GetAwardsAfterMap;
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
        Debug.Log("openMainMenu");

        Debug.Log("LastMap " + YandexGame.savesData.playerWrapper.lastMap);
        Debug.Log("LastMapplacesCount " + YandexGame.savesData.playerWrapper.lastMapPlaces.Count);
        if (YandexGame.savesData.playerWrapper.lastMap != "" && YandexGame.savesData.playerWrapper.lastMapPlaces.Count != 0)
        {
            Debug.Log("AddRewardEvent");
            YandexGame.EndDataLoadingEvent += GetAwardsAfterMap;
        }

        for (int i = 0; i < gamemodePanels.Count; i++)
        {
            gamemodePanels[i].SetActive(true);
        }

        SetSavedSO();

        for (int i = 0; i < gamemodePanels.Count; i++)
        {
            gamemodePanels[i].SetActive(false);
        }

        UpdateNewCollectiblesWarnings();
    }

    public void UpdateNewCollectiblesWarnings()
    {
        if (YandexGame.savesData.playerWrapper.newCollectibles.Count != 0)
            newCollectiblesWarning.SetActive(true);
        else
            newCollectiblesWarning.SetActive(false);
    }



}
