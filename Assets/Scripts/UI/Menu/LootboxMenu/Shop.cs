using UnityEngine;
using YG;

enum AdvertisementType
{
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
    [SerializeField] private GameObject noLootboxImage;
    [SerializeField] private LootBox lootbox;
    [SerializeField] private AwardUIController lootboxAwardUI;
    [SerializeField] private ShopUIController gemPresenterUI;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += GetAdReward;

        //YandexGame.CheaterVideoEvent
        //YandexGame.ErrorVideoEvent

        //YandexGame.OpenVideoEvent
        //YandexGame.CloseVideoEvent
    }

    private void Awake()
    {
        lootbox.endAnimation += OpenLootbox;
    }

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= GetAdReward;
    }

    private void GetAdReward(int index)
    {
        AdvertisementType type = (AdvertisementType)index;

        switch (type)
        {
            case AdvertisementType.SingleAd:
                EarningManager.AddGem(smallAdReward);
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
        }

        YandexGame.SaveProgress();
    }

    public void InitializeUI()
    {
        gemPresenterUI.LoadValuesUI(exchangeCoinCost, exchangeGemReward, smallAdReward, mediumAdReward, mediumAdViews);
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
            //сообзение о нехватке гемов
            return;
        }

        EarningManager.AddLootbox();
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
            EarningManager.AddCoin(coinValue);

        if (gemValue != 0)
            EarningManager.AddGem(gemValue);

        YandexGame.savesData.playerWrapper.collectibles.Add(collectibleItem.Name);
        YandexGame.savesData.playerWrapper.newCollectibles.Add(collectibleItem.Name);

        YandexGame.SaveProgress();

        lootboxAwardUI.ShowAwards(coinValue, gemValue, collectibleItem);
        UpdateUI();
    }

    public void UpdateUI()
    {
        bool haveLootbox = YandexGame.savesData.lootboxes > 0;
        openLootboxButton.SetActive(haveLootbox);
        noLootboxImage.SetActive(!haveLootbox);
    }

}
