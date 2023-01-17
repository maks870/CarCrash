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
    [SerializeField] private ShopUIController gemPresenterUI;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += GetAdReward;

        //YandexGame.CheaterVideoEvent
        //YandexGame.ErrorVideoEvent

        //YandexGame.OpenVideoEvent
        //YandexGame.CloseVideoEvent
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
}
