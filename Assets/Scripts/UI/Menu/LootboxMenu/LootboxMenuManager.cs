using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LootboxMenuManager : MenuManager
{
    [SerializeField] private LootBox lootbox;
    [SerializeField] private AwardUI lootboxAwardUI;
    [SerializeField] private EarningManagerUI earningManagerUI;
    [SerializeField] private GemPresenter gemPresenter;

    private void SetSavedSO()
    {
        gemPresenter.InitializeUI();
    }

    protected override void SavePlayer()
    {
        base.SavePlayer();
    }

    public override void SaveDefaultSO()
    {
        YandexGame.savesData.lootboxes += 1;
    }

    public override void OpenMenu()
    {
        base.OpenMenu();
        InitializeMenu();
    }

    public override void CloseMenu()
    {
        SavePlayer();
        base.CloseMenu();
    }

    public override void InitializeMenu()
    {
        SetSavedSO();
    }

    public void OpenLootbox()
    {
        if (YandexGame.savesData.lootboxes == 0)
        {
            //вывести сообщение об нехватке лутбоксов
            return;
        }

        if (!EarningManager.SpendGem(lootbox.Cost))
            return;

        int coinValue;
        int gemValue;
        CollectibleSO collectibleItem;

        lootbox.GetReward(out coinValue, out gemValue, out collectibleItem);

        if (coinValue != 0)
            YandexGame.savesData.coins += coinValue;

        if (gemValue != 0)
            YandexGame.savesData.gems += gemValue;

        YandexGame.savesData.playerWrapper.collectibles.Add(collectibleItem.Name);
        YandexGame.SaveProgress();

        lootboxAwardUI.ShowAwards(coinValue, gemValue, collectibleItem);
    }

}
