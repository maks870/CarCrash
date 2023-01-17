using UnityEngine;
using UnityEngine.UI;
using YG;

public class EarningUIController : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text gemText;
    [SerializeField] private Text lootboxText;

    [SerializeField] private GameObject lackCoinsWarning;
    [SerializeField] private GameObject lackGemsWarning;

    private void OnEnable()
    {
        YandexGame.EndDataLoadingEvent += UpdateEarnings;
        EarningManager.changeEarnings += UpdateEarnings;
        EarningManager.lackCoins += ShowLackCoinsWarning;
        EarningManager.lackGems += ShowLackGemsWarning;
    }

    private void OnDisable()
    {
        YandexGame.EndDataLoadingEvent -= UpdateEarnings;
        EarningManager.changeEarnings -= UpdateEarnings;
        EarningManager.lackCoins -= ShowLackCoinsWarning;
        EarningManager.lackGems -= ShowLackGemsWarning;
    }

    public void UpdateEarnings()
    {

        if (coinText != null)
            coinText.text = YandexGame.savesData.coins.ToString();

        if (gemText != null)
            gemText.text = YandexGame.savesData.gems.ToString();

        if (lootboxText != null)
            lootboxText.text = YandexGame.savesData.lootboxes.ToString();
    }

    public void ShowLackCoinsWarning()
    {
        lackCoinsWarning.SetActive(true);
    }

    public void ShowLackGemsWarning()
    {
        lackGemsWarning.SetActive(true);
    }

    public void CloseLackCoinWarning()
    {
        lackCoinsWarning.SetActive(false);
    }

    public void CloseLackGemsWarning()
    {
        lackGemsWarning.SetActive(false);
    }
}
