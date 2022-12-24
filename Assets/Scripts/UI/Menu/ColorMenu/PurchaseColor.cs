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

    private void BuyColor()
    {
        if (!EarningManager.SpendCoin(currentCarColorSO.Cost))
            return;
        Debug.Log("œŒ ”œŒ◊ ¿");

        YandexGame.savesData.playerWrapper.collectibles.Add(currentCarColorSO.Name);
        YandexGame.SaveProgress();
        Debug.Log("œÓÍÛÔÍ‡ - " + currentCarColorSO.Name);

        carColorSwitcher.InitializeUI();
        carColorSwitcher.SetCurrentColor(currentCarColorSO);
        HidePurchaseButton();
    }

    public void ShowPurchaseButton(CarColorSO collectible)
    {
        gameObject.SetActive(true);
        currentCarColorSO = collectible;
        costText.text = currentCarColorSO.Cost.ToString();
        buttonPurchase.onClick.RemoveAllListeners();
        buttonPurchase.onClick.AddListener(BuyColor);
    }

    public void HidePurchaseButton()
    {
        gameObject.SetActive(false);
    }


}
