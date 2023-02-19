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

    private bool subYandexLoad = false;

    public bool SubYandexLoad { set => subYandexLoad = value; }

    private void SetSavedSO()
    {
        mapSwitcher.InitializeUI();
    }

    public void GetAwardsAfterMap()
    {
        if (YandexGame.savesData.playerWrapper.lastMap != "" && YandexGame.savesData.playerWrapper.lastMapPlaces.Count != 0)
        {
            presenter.GetAward();
            UpdateNewPossibilitiyWarnings();

            if (subYandexLoad)
                YandexGame.EndDataLoadingEvent -= GetAwardsAfterMap;
        }
    }

    public override void SOLoaderSubscribe()
    {
        mapSwitcher.LoadSOSubscribe();
    }

    public override void SaveDefaultSO()
    {

    }

    public override void OpenMenu()
    {
        base.OpenMenu();
        InitializeMenu();
    }

    public override void InitializeMenu()
    {

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

    public void ClearSOLoaderTEST()
    {
        SOLoader.instance.Clear();
    }

}
