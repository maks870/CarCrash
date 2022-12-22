using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PurchaseColor : MonoBehaviour
{
    [SerializeField] private Text costText;
    [SerializeField] private Button buttonPurchase;
    private CarColorSO currentCarColorSO;
    private CarColorSwitcher carColorSwitcher;

    public CarColorSwitcher CarColorSwitcher { set => carColorSwitcher = value; }

    void Start()
    {
        gameObject.SetActive(false);
    }

    private void BuyColor()
    {
        if (!EarningManager.SpendCoin(currentCarColorSO.Cost))
            return;

        YandexGame.savesData.collectedItems.Add(currentCarColorSO.Name);
        YandexGame.SaveProgress();
        carColorSwitcher.LoadCarColorsSO();
        carColorSwitcher.SetCurrentColor(currentCarColorSO);
    }

    public void ShowPurchaseButton(CarColorSO collectible)
    {
        currentCarColorSO = collectible;
        costText.text = currentCarColorSO.Cost.ToString();
        buttonPurchase.onClick.RemoveAllListeners();
        buttonPurchase.onClick.AddListener(BuyColor);
        gameObject.SetActive(true);
    }

    public void HidePurchaseButton()
    {
        gameObject.SetActive(false);
    }


}
