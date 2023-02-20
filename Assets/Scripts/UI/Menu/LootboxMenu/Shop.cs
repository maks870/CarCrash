using UnityEngine;
using YG;

enum AdvertisementType
{
    NoLootboxAdd = 0,
    SingleAd = 1,
    DoubleAd = 2
}

public class Shop : MonoBehaviour
{
    [SerializeField] private int exchangeCoinCost = 50;
    [SerializeField] private int exchangeGemReward = 1;
    [SerializeField] private int smallAdReward = 1;
    [SerializeField] private int mediumAdReward = 2;
    [SerializeField] private int mediumAdViews = 2;
    [SerializeField] private int lootboxCost = 5;
    [SerializeField] private GameObject openLootboxButton;
    [SerializeField] private GameObject noLootboxButton;
    [SerializeField] private LootBox lootbox;
    [SerializeField] private AwardUIController lootboxAwardUI;
    [SerializeField] private ShopUIController gemPresenterUI;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += GetAdReward;
        lootboxAwardUI.OnAwardsEnd += lootbox.Close;
        //YandexGame.CheaterVideoEvent
        //YandexGame.ErrorVideoEvent

        //YandexGame.OpenVideoEvent
        //YandexGame.CloseVideoEvent
    }

    private void Awake()
    {
        lootbox.ActionEndOpen += OpenLootbox;
        lootbox.ActionEndClose += () =>
        {
            openLootboxButton.SetActive(true);
            UpdateUI();
        };
    }

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable()
    {
        lootboxAwardUI.OnAwardsEnd -= lootbox.Close;
        YandexGame.RewardVideoEvent -= GetAdReward;
    }

    private void GetAdReward(int index)
    {
        AdvertisementType type = (AdvertisementType)index;

        switch (type)
        {
            case AdvertisementType.SingleAd:
                EarningManager.AddCoin(smallAdReward);
                break;

            case AdvertisementType.DoubleAd:
                if (YandexGame.savesData.mediumGemAdViewed < mediumAdViews - 1)
                {
                    YandexGame.savesData.mediumGemAdViewed++;
                }
                else
                {
                    YandexGame.savesData.mediumGemAdViewed = 0;
                    EarningManager.AddGem(mediumAdReward);
                }

                gemPresenterUI.UpdateDoubleAdUI();
                break;

            case AdvertisementType.NoLootboxAdd:
                EarningManager.AddLootbox();
                UpdateUI();
                break;
        }

        YandexGame.SaveProgress();
    }

    public void InitializeUI()
    {
        gemPresenterUI.LoadValuesUI(exchangeCoinCost, exchangeGemReward, smallAdReward, mediumAdReward, mediumAdViews, lootboxCost);
    }

    public void Exchange()
    {
        if (EarningManager.SpendCoin(exchangeCoinCost))
            EarningManager.AddGem(exchangeGemReward);

        YandexGame.SaveProgress();
    }

    public void ShowAd(int AdIndex)
    {
        YandexGame.RewVideoShow(AdIndex);
    }

    public void BuyLootbox()
    {
        if (!EarningManager.SpendGem(lootboxCost))
        {
            return;
        }

        EarningManager.AddLootbox();
        UpdateUI();
    }

    public void OpenLootbox()
    {
        if (!EarningManager.SpendLootbox())
        {
            return;
        }

        int coinValue;
        int gemValue;
        CollectibleSO collectibleItem;

        lootbox.GetReward(out coinValue, out gemValue, out collectibleItem);

        if (coinValue != 0)
            EarningManager.AddCoin(coinValue);

        if (gemValue != 0)
            EarningManager.AddGem(gemValue);

        if (collectibleItem != null)
        {
            YandexGame.savesData.playerWrapper.collectibles.Add(collectibleItem.Name);
            YandexGame.savesData.playerWrapper.newCollectibles.Add(collectibleItem.Name);
        }

        YandexGame.SaveProgress();

        lootboxAwardUI.ShowAwards(coinValue, gemValue, collectibleItem);
    }

    public void UpdateUI()
    {
        bool haveLootbox = YandexGame.savesData.lootboxes > 0;
        openLootboxButton.SetActive(haveLootbox);
        noLootboxButton.SetActive(!haveLootbox);
    }


}
