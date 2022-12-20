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
    [SerializeField] private List<�ollectibleSO> carColorsSO = new List<�ollectibleSO>();
    [SerializeField] private PurchaseColor purchaseColor;

    private bool isButtonsCreated = false;
    private List<�ollectibleSO> openedCarColors = new List<�ollectibleSO>();
    private List<�ollectibleSO> closedCarColors = new List<�ollectibleSO>();



    private void OnEnable()
    {
        YandexGame.GetDataEvent += InitializeUI;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= InitializeUI;
    }

    void Start()
    {
        purchaseColor.CarColorSwitcher = this;

        if (YandexGame.SDKEnabled == true)
        {
            LoadCarColorsSO();
        }
    }

    void Update()
    {
    }

    private void InitializeUI()
    {
        if (!isButtonsCreated)
            CreateButtons();

        LoadCarColorsSO();
    }

    private void CreateButtons()
    {
        for (int i = 0; i < carColorsSO.Count; i++)
        {
            ButtonCollectibleUI newButton = Instantiate(button, transform).GetComponent<ButtonCollectibleUI>();
            buttons.Add(newButton);
        }

        isButtonsCreated = true;
    }

    private void UpdateUI(List<�ollectibleSO> openColors, List<�ollectibleSO> closedColors)
    {
        for (int i = 0; i < openColors.Count; i++)
        {
            buttons[i].Image.sprite = openColors[i].Sprite;
            buttons[i].ClosedImage.SetActive(false);
            buttons[i].CollectibleSO = openColors[i];
            buttons[i].Button.onClick.RemoveAllListeners();
            buttons[i].Button.onClick.AddListener(() => SetCurrentRenderer(buttons[i].CollectibleSO));
            buttons[i].Button.onClick.AddListener(() => purchaseColor.HidePurchaseButton());
        }

        for (int i = 0; i < closedColors.Count; i++)
        {
            int j = i + openColors.Count;
            buttons[j].Image.sprite = closedColors[i].Sprite;
            buttons[j].ClosedImage.SetActive(true);
            buttons[j].CollectibleSO = closedColors[i];
            buttons[j].Button.onClick.RemoveAllListeners();
            buttons[j].Button.onClick.AddListener(() => purchaseColor.ShowPurchaseButton(buttons[j].CollectibleSO));
        }
    }

    public void LoadCarColorsSO()
    {
        openedCarColors.Clear();
        List<string> collectedItems = YandexGame.savesData.collectedItems;
        closedCarColors.AddRange(carColorsSO);

        foreach (string itemName in collectedItems)
        {
            �ollectibleSO collectible = closedCarColors.Find(item => item.Sprite.name == itemName);
            openedCarColors.Add(collectible);
            closedCarColors.Remove(collectible);
        }

        UpdateUI(openedCarColors, closedCarColors);
    }

    public void SetCurrentRenderer(�ollectibleSO collectible)
    {
        CarColorSO carColorCollectible = (CarColorSO)collectible;
        currentRenderer.material.mainTexture = carColorCollectible.Texture;
    }
}
