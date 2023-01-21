using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopUIController : MonoBehaviour
{
    [SerializeField] private Text exchangeCoinCost;
    [SerializeField] private Text exchangeGemReward;
    [SerializeField] private Text lootboxCost;
    [SerializeField] private Text smallGemAd;
    [SerializeField] private Text mediumGemAd;
    [SerializeField] private Text mediumAdViewed;
    [SerializeField] private Text mediumAdViews;

    public void LoadValuesUI(int exchangeCoinCost, int exchangeGemReward, int smallGemAd, int mediumGemAd, int mediumAdViews, int lootboxCost)
    {
        this.exchangeCoinCost.text = exchangeCoinCost.ToString();
        this.exchangeGemReward.text = exchangeGemReward.ToString();
        this.smallGemAd.text = smallGemAd.ToString();
        this.mediumGemAd.text = mediumGemAd.ToString();
        this.mediumAdViews.text = mediumAdViews.ToString();
        this.lootboxCost.text = lootboxCost.ToString();
        UpdateDoubleAdUI();
    }
    public void UpdateDoubleAdUI()
    {
        mediumAdViewed.text = YandexGame.savesData.mediumGemAdViewed.ToString();
    }
}
