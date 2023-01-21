using UnityEngine;
using UnityEngine.UI;
using YG;

public class EarningUIController : MonoBehaviour
{
    [SerializeField] private Text coinCountText;
    [SerializeField] private Text gemCountText;
    [SerializeField] private Text lootboxCountText;

    [SerializeField] private GameObject lackCoinsWarning;
    [SerializeField] private GameObject lackGemsWarning;

    private void OnEnable()
    {
        EarningManager.OnChangeEarnings += UpdateEarnings;
        EarningManager.OnLackCoins += ShowLackCoinsWarning;
        EarningManager.OnLackGems += ShowLackGemsWarning;
    }

    private void OnDisable()
    {
        EarningManager.OnChangeEarnings -= UpdateEarnings;
        EarningManager.OnLackCoins -= ShowLackCoinsWarning;
        EarningManager.OnLackGems -= ShowLackGemsWarning;
    }

    public void UpdateEarnings()
    {

        if (coinCountText != null)
            coinCountText.text = YandexGame.savesData.coins.ToString();

        if (gemCountText != null)
            gemCountText.text = YandexGame.savesData.gems.ToString();

        if (lootboxCountText != null)
            lootboxCountText.text = YandexGame.savesData.lootboxes.ToString();
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
