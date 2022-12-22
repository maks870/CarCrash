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

    private void OnEnable()
    {
        if (isFirstLoad)
            YandexGame.GetDataEvent += InitializeUI;
    }

    private void OnDisable()
    {
        //YandexGame.GetDataEvent -= InitializeUI;
    }

    void Start()
    {
        purchaseColor.CarColorSwitcher = this;

        if (YandexGame.SDKEnabled == true)
        {
            InitializeUI();
        }
    }

    void Update()
    {
    }

    private void InitializeUI()
    {
        if (isFirstLoad)
            CreateButtons();

        LoadCarColorsSO();
    }

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
        for (int i = 0; i < openColors.Count; i++)
        {
            //buttons[i].Image.sprite = openColors[i].Sprite; //Сделать нахождение цвеитов пикселей и окрашивать изображение цвета в них в них
            buttons[i].ClosedImage.SetActive(false);
            buttons[i].CollectibleSO = openColors[i];
            buttons[i].Button.onClick.RemoveAllListeners();
            buttons[i].Button.onClick.AddListener(() => SetCurrentRenderer((CarColorSO)buttons[i].CollectibleSO));
            buttons[i].Button.onClick.AddListener(() => purchaseColor.HidePurchaseButton());
        }

        for (int i = 0; i < closedColors.Count; i++)
        {
            int j = i + openColors.Count;
            //buttons[j].Image.sprite = closedColors[i].Sprite;//Сделать нахождение цвеитов пикселей и окрашивать изображение цвета в них в них
            buttons[j].ClosedImage.SetActive(true);
            buttons[j].CollectibleSO = closedColors[i];
            buttons[j].Button.onClick.RemoveAllListeners();
            buttons[j].Button.onClick.AddListener(() => purchaseColor.ShowPurchaseButton((CarColorSO)buttons[j].CollectibleSO));
        }
    }

    public void LoadCarColorsSO()
    {
        openedCarColors.Clear();
        List<string> collectedItems = YandexGame.savesData.collectedItems;
        closedCarColors.AddRange(carColorsSO);

        foreach (string itemName in collectedItems)
        {
            CarColorSO collectible = closedCarColors.Find(item => item.Name == itemName);
            openedCarColors.Add(collectible);
            closedCarColors.Remove(collectible);
        }

        UpdateUI(openedCarColors, closedCarColors);
    }

    public void SetCurrentRenderer(CarColorSO carColorCollectible)
    {
        currentRenderer.material.mainTexture = carColorCollectible.Texture;
        currentCarColor = carColorCollectible;
    }
}
