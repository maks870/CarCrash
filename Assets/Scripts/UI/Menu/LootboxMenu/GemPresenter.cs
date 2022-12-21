using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

enum AdvertisementType
{
    SingleAd = 1,
    DoubleAd = 2
}

public class GemPresenter : MonoBehaviour
{
    [SerializeField] private int exchangeCoinCost = 50;
    [SerializeField] private int exchangeGemReward = 1;
    [SerializeField] private int smallAdReward = 1;
    [SerializeField] private int mediumAdReward = 2;
    [SerializeField] private int mediumAdViews = 2;
    [SerializeField] private GemPresenterUI gemPresenterUI;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        YandexGame.GetDataEvent += LoadUI;
    }

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
        YandexGame.GetDataEvent -= LoadUI;
    }

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            LoadUI();
        }
    }

    private void LoadUI()
    {
        gemPresenterUI.LoadValuesUI(exchangeCoinCost, exchangeGemReward, smallAdReward, mediumAdReward, mediumAdViews);
    }

    private void Rewarded(int index)
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
                    YandexGame.SaveProgress();
                }
                else
                {
                    YandexGame.savesData.mediumGemAdViewed = 0;
                    EarningManager.AddGem(mediumAdReward);
                    YandexGame.SaveProgress();
                };

                gemPresenterUI.UpdateDoubleAdUI();
                break;
        }
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
