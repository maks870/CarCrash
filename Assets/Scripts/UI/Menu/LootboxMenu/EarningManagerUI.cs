using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class EarningManagerUI : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text gemText;

    [SerializeField] private GameObject lackCoinsWarning;
    [SerializeField] private GameObject lackGemsWarning;

    private void OnEnable()
    {
        EarningManager.changeEarnings += UpdateEarnings;
        EarningManager.lackCoins += ShowLackCoinsWarning;
        EarningManager.lackGems += ShowLackGemsWarning;
    }

    private void OnDisable()
    {
        EarningManager.changeEarnings -= UpdateEarnings;
        EarningManager.lackCoins -= ShowLackCoinsWarning;
        EarningManager.lackGems -= ShowLackGemsWarning;
    }

    private void Start()
    {
        UpdateEarnings();
    }

    public void UpdateEarnings()
    {

        if (coinText != null)
            coinText.text = YandexGame.savesData.coins.ToString();

        if (gemText != null)
            gemText.text = YandexGame.savesData.gems.ToString();
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
