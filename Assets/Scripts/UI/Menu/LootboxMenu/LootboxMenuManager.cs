using UnityEngine;
using YG;

public class LootboxMenuManager : MenuManager
{
    [SerializeField] private LootBox lootbox;
    [SerializeField] private GameObject openLootboxButton;
    [SerializeField] private GameObject noLootboxImage;
    [SerializeField] private AwardUIController lootboxAwardUI;
    [SerializeField] private Shop shop;

    private void SetSavedSO()
    {
        shop.InitializeUI();
    }

    protected override void SavePlayer()
    {
        base.SavePlayer();
        YandexGame.SaveProgress();
    }

    public override void SOLoaderSubscribe()
    {
        lootbox.CollectibledLoadSubscribe();
    }

    public override void SaveDefaultSO()
    {
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
        shop.UpdateUI();
    }

}
