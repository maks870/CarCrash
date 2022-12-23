using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CarColorSwitcher : MonoBehaviour
{
    [SerializeField] private MeshRenderer currentRenderer;
    [SerializeField] private GameObject button;
    [SerializeField] private List<ButtonCollectibleUI> buttons = new List<ButtonCollectibleUI>();
    [SerializeField] private List<CarColorSO> carColorsSO = new List<CarColorSO>();
    [SerializeField] private PurchaseColor purchaseColor;
    private CollectibleSO currentCarColor;

    private bool isFirstLoad = true;
    private List<CarColorSO> openedCarColors = new List<CarColorSO>();
    private List<CarColorSO> closedCarColors = new List<CarColorSO>();
    public CollectibleSO CurrentCarColor { get => currentCarColor; }
    public PurchaseColor PurchaseColor => purchaseColor;

    private void CreateButtons()
    {
        buttons.Add(button.GetComponent<ButtonCollectibleUI>());
        for (int i = 0; i < carColorsSO.Count - 1; i++)
        {
            ButtonCollectibleUI newButton = Instantiate(button, transform).GetComponent<ButtonCollectibleUI>();
            buttons.Add(newButton);
        }

        isFirstLoad = false;
    }

    private void UpdateUI(List<CarColorSO> openColors, List<CarColorSO> closedColors)
    {
        Debug.Log(111);
        for (int i = 0; i < openColors.Count; i++)
        {
            //buttons[i].Image.sprite = openColors[i].Sprite; //Сделать нахождение цвеитов пикселей и окрашивать изображение цвета в них в них
            buttons[i].ClosedImage.SetActive(false);
            buttons[i].CollectibleSO = openColors[i];
            buttons[i].Button.onClick.RemoveAllListeners();
            CarColorSO carColor = (CarColorSO)buttons[i].CollectibleSO;
            buttons[i].Button.onClick.AddListener(() => SetCurrentColor(carColor));
            buttons[i].Button.onClick.AddListener(() => purchaseColor.HidePurchaseButton());
        }

        for (int i = 0; i < closedColors.Count; i++)
        {
            int j = i + openColors.Count;
            //buttons[j].Image.sprite = closedColors[i].Sprite;//Сделать нахождение цвеитов пикселей и окрашивать изображение цвета в них в них
            buttons[j].ClosedImage.SetActive(true);
            buttons[j].CollectibleSO = closedColors[i];
            buttons[j].Button.onClick.RemoveAllListeners();
            CarColorSO carColor = (CarColorSO)buttons[i].CollectibleSO;
            buttons[j].Button.onClick.AddListener(() =>
            {
                purchaseColor.gameObject.SetActive(true);
                purchaseColor.ShowPurchaseButton(carColor);
            });
        }
    }
    private void LoadCarColorsSO()
    {
        openedCarColors.Clear();
        closedCarColors.Clear();
        List<string> collectedItems = YandexGame.savesData.playerWrapper.collectibles;
        closedCarColors.AddRange(carColorsSO);


        foreach (string itemName in collectedItems)
        {
            CarColorSO collectible = closedCarColors.Find(item => item.Name == itemName);

            if (collectible != null)
            {
                openedCarColors.Add(collectible);
                closedCarColors.Remove(collectible);
            }
        }

        UpdateUI(openedCarColors, closedCarColors);
    }

    public void InitializeUI()
    {
        if (isFirstLoad)
            CreateButtons();

        LoadCarColorsSO();
    }

    public void SetCurrentColor(CarColorSO carColorCollectible)
    {
        currentRenderer.material.mainTexture = carColorCollectible.Texture;
        currentCarColor = carColorCollectible;
    }
}
