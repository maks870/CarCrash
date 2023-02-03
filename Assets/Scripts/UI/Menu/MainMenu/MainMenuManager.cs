using System.Collections.Generic;
using UnityEngine;
using YG;

public class MainMenuManager : MenuManager
{
    [SerializeField] private MapSwitcher mapSwitcher;
    [SerializeField] private AwardPresenter presenter;
    [SerializeField] private GameObject newCollectiblesWarning;
    [SerializeField] private GameObject newLootbloxesWarning;
    [SerializeField] private List<GameObject> gamemodePanels;

    private void SetSavedSO()
    {
        mapSwitcher.InitializeUI();
    }

    private void GetAwardsAfterMap()
    {
        presenter.GetAward();
        YandexGame.savesData.playerWrapper.lastMap = "";
        YandexGame.savesData.playerWrapper.lastMapPlaces.Clear();
        YandexGame.SaveProgress();
        UpdateNewPossibilitiyWarnings();
        YandexGame.EndDataLoadingEvent -= GetAwardsAfterMap;
    }

    public override void SaveDefaultSO()
    {
        MapSO map = playerLoad.DefaultMap;
        MapInfo mapInfo = new MapInfo(map.Name, map.MaxPoints);
        YandexGame.savesData.playerWrapper.maps.Add(mapInfo);
    }

    public override void OpenMenu()
    {
        base.OpenMenu();
        InitializeMenu();
    }

    public override void InitializeMenu()
    {
        if (YandexGame.savesData.playerWrapper.lastMap != "" && YandexGame.savesData.playerWrapper.lastMapPlaces.Count != 0)
            YandexGame.EndDataLoadingEvent += GetAwardsAfterMap;


        for (int i = 0; i < gamemodePanels.Count; i++)
        {
            gamemodePanels[i].SetActive(true);
        }

        SetSavedSO();

        for (int i = 0; i < gamemodePanels.Count; i++)
        {
            gamemodePanels[i].SetActive(false);
        }

        UpdateNewPossibilitiyWarnings();
    }

    public void UpdateNewPossibilitiyWarnings()
    {
        if (YandexGame.savesData.playerWrapper.newCollectibles.Count != 0)
            newCollectiblesWarning.SetActive(true);
        else
            newCollectiblesWarning.SetActive(false);

        if (YandexGame.savesData.lootboxes > 0)
        {
            newLootbloxesWarning.SetActive(true);
        }
        else
            newLootbloxesWarning.SetActive(false);
    }



}
