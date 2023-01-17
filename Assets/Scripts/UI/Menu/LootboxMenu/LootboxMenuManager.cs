using UnityEngine;
using YG;

public class LootboxMenuManager : MenuManager
{
    [SerializeField] private LootBox lootbox;

    [SerializeField] private GameObject openLootboxButton;
    [SerializeField] private GameObject noLootboxImage;
    [SerializeField] private AwardUIController lootboxAwardUI;
    [SerializeField] private EarningUIController earningManagerUI;
    [SerializeField] private Shop shop;

    private void Awake()
    {
        lootbox.endAnimation += OpenLootbox;
    }

    private void SetSavedSO()
    {
        shop.InitializeUI();
    }

    protected override void SavePlayer()
    {
        base.SavePlayer();
        YandexGame.SaveProgress();
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
        shop.InitializeUI();
        SetSavedSO();
        UpdateUI();
    }

    public void OpenLootbox()
    {
        if (!EarningManager.SpendLootbox())
        {
            //вывести сообщение об нехватке лутбоксов
            return;
        }

        int coinValue;
        int gemValue;
        CollectibleSO collectibleItem;

        lootbox.GetReward(out coinValue, out gemValue, out collectibleItem);

        if (coinValue != 0)
            YandexGame.savesData.coins += coinValue;

        if (gemValue != 0)
            YandexGame.savesData.gems += gemValue;

        YandexGame.savesData.playerWrapper.collectibles.Add(collectibleItem.Name);
        YandexGame.savesData.playerWrapper.newCollectibles.Add(collectibleItem.Name);

        YandexGame.SaveProgress();

        lootboxAwardUI.ShowAwards(coinValue, gemValue, collectibleItem);
    }

    public void UpdateUI()
    {
        bool haveLootbox = YandexGame.savesData.lootboxes > 0;
        openLootboxButton.SetActive(haveLootbox);
        noLootboxImage.SetActive(!haveLootbox);
    }

}
