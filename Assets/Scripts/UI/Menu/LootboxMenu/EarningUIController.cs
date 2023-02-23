using UnityEngine;
using UnityEngine.UI;
using YG;

public class EarningUIController : MonoBehaviour
{
    [SerializeField] private Text coinCountText;
    [SerializeField] private Text gemCountText;
    [SerializeField] private Text lootboxCountText;

    [SerializeField] private Animator lackCoinsAnim;
    [SerializeField] private Animator lackGemsAnim;

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
        lackCoinsAnim.SetTrigger("On");
    }

    public void ShowLackGemsWarning()
    {
        lackGemsAnim.SetTrigger("On");
    }
}
