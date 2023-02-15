using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CarColorSwitcher : MonoBehaviour
{
    [SerializeField] private MeshRenderer currentRenderer;
    [SerializeField] private GameObject button;
    [SerializeField] private PurchaseColor purchaseColor;
    [SerializeField] private CarTabSwitcher carTabSwitcher;
    private CollectibleSO currentCarColor;

    private bool isFirstLoad = true;
    private List<CarColorSO> carColorsSO = new List<CarColorSO>();
    private List<CarColorSO> openedCarColors = new List<CarColorSO>();
    private List<CarColorSO> closedCarColors = new List<CarColorSO>();
    private List<ButtonCollectibleUI> buttons = new List<ButtonCollectibleUI>();

    public CollectibleSO CurrentCarColor => currentCarColor;
    public PurchaseColor PurchaseColor => purchaseColor;

    private void Awake()
    {
        purchaseColor.CarColorSwitcher = this;
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
            buttons[i].ClosedImage.gameObject.SetActive(false);
            buttons[i].CollectibleSO = openColors[i];
            SetColorImages(buttons[i]);
            buttons[i].Button.onClick.RemoveAllListeners();
            CarColorSO carColor = (CarColorSO)buttons[i].CollectibleSO;
            buttons[i].Button.onClick.AddListener(() => SetCurrentColor(carColor));
            buttons[i].Button.onClick.AddListener(() => purchaseColor.HidePurchaseButton());
        }

        for (int i = 0; i < closedColors.Count; i++)
        {
            int j = i + openColors.Count;
            buttons[j].ClosedImage.gameObject.SetActive(true);
            buttons[j].CollectibleSO = closedColors[i];
            SetColorImages(buttons[j]);
            buttons[j].Button.onClick.RemoveAllListeners();
            CarColorSO carColor = (CarColorSO)buttons[j].CollectibleSO;
            Transform buttonTransform = buttons[j].transform;
            buttons[j].Button.onClick.AddListener(() => purchaseColor.ShowPurchaseButton(carColor));
            buttons[j].Button.onClick.AddListener(() => carTabSwitcher.SelectButton(buttonTransform));
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

    private void SetColorImages(ButtonCollectibleUI colorButton)
    {
        List<Image> images = new List<Image>();
        Image[] imageComponents = colorButton.transform.GetComponentsInChildren<Image>();

        for (int i = 0; i < imageComponents.Length; i++)
        {
            if (imageComponents[i] != colorButton.ClosedImage.GetComponent<Image>() && imageComponents[i] != colorButton.Image)
            {
                images.Add(imageComponents[i]);
            }
        }

        SetColors((CarColorSO)colorButton.CollectibleSO, images);
    }

    private void SetColors(CarColorSO carColor, List<Image> images)
    {
        Texture2D texture = carColor.Texture;
        int maxYPixel = carColor.Texture.height;
        for (int i = 0; i < 3; i++)
        {
            images[i].color = texture.GetPixel(i, maxYPixel - 1);
        }
    }

    public void InitializeUI()
    {
        if (isFirstLoad)
            CreateButtons();

        LoadCarColorsSO();
    }

    public void SelectCurrentButton()
    {
        Transform buttonTransform = buttons[0].transform;

        for (int i = 0; i < openedCarColors.Count; i++)
        {
            if (openedCarColors[i] == (CarColorSO)currentCarColor)
                buttonTransform = buttons[i].transform;
        }

        carTabSwitcher.SelectButton(buttonTransform);
    }

    public void LoadSOSubscribe()
    {
        SOLoader.instance.OnLoadingEvent += (scriptableObj) =>
        {
            if (scriptableObj.GetType() == typeof(CarColorSO))
                carColorsSO.Add((CarColorSO)scriptableObj);
        };
    }

    public void FillListBySO(List<CarColorSO> carColors)
    {
        carColorsSO.AddRange(carColors);
    }

    public void SetCurrentColor(CarColorSO carColorCollectible)
    {
        currentRenderer.material.mainTexture = carColorCollectible.Texture;
        currentCarColor = carColorCollectible;
        SelectCurrentButton();
    }
}
