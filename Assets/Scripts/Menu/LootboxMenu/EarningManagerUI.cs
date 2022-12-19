using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarningManagerUI : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text gemText;

    [SerializeField] private GameObject lackGemsWarning;
    [SerializeField] private GameObject lackCoinsWarning;

    private void OnEnable()
    {
        EarningManager.changeEarnings += UpdateEarnings;
    }

    private void OnDisable()
    {
        EarningManager.changeEarnings -= UpdateEarnings;
    }

    public void UpdateEarnings(int coins, int gems)
    {
        if (coinText != null)
            coinText.text = coins.ToString();

        if (gemText != null)
            gemText.text = gems.ToString();
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
