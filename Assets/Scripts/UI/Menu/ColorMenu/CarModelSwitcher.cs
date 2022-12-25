using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CarModelSwitcher : MonoBehaviour
{
    [SerializeField] private MeshFilter currentMeshFilter;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject carStatWindow;
    [SerializeField] private float maxAcceleration;
    [SerializeField] private float maxHandleability;
    [SerializeField] private Image accelerationImage;
    [SerializeField] private Image handleability;

    private bool isFirstLoad = true;
    private CollectibleSO currentCarModel;
    private CarTabSwitcher carTabSwitcher;
    private List<CarModelSO> carModelsSO = new List<CarModelSO>();
    private List<CarModelSO> openedCarModels = new List<CarModelSO>();
    private List<CarModelSO> closedCarModels = new List<CarModelSO>();
    private List<ButtonCollectibleUI> buttons = new List<ButtonCollectibleUI>();

    public CollectibleSO CurrentCarModel => currentCarModel;
    public GameObject CarStatWindow => carStatWindow;
    public CarTabSwitcher CarTabSwitcher { set => carTabSwitcher = value; }

    private void CreateButtons()
    {
        buttons.Add(button.GetComponent<ButtonCollectibleUI>());
        for (int i = 0; i < carModelsSO.Count - 1; i++)
        {
            ButtonCollectibleUI newButton = Instantiate(button, transform).GetComponent<ButtonCollectibleUI>();
            buttons.Add(newButton);
        }

        isFirstLoad = false;
    }

    private void UpdateUI(List<CarModelSO> openCarModels, List<CarModelSO> closedCarModels)
    {
        for (int i = 0; i < openCarModels.Count; i++)
        {
            buttons[i].Image.sprite = openCarModels[i].Sprite;
            buttons[i].ClosedImage.gameObject.SetActive(false);
            buttons[i].CollectibleSO = openCarModels[i];
            buttons[i].Button.onClick.RemoveAllListeners();
            CarModelSO carModel = (CarModelSO)buttons[i].CollectibleSO;
            buttons[i].Button.onClick.AddListener(() => SetCurrentModel(carModel));
        }

        for (int i = 0; i < closedCarModels.Count; i++)
        {
            int j = i + openCarModels.Count;
            buttons[j].Image.sprite = closedCarModels[i].Sprite;
            buttons[j].ClosedImage.gameObject.SetActive(true);
            buttons[j].CollectibleSO = closedCarModels[i];
            buttons[j].Button.onClick.RemoveAllListeners();
        }
    }

    private void LoadCarModelsSO()
    {
        openedCarModels.Clear();
        closedCarModels.Clear();
        List<string> collectedItems = YandexGame.savesData.playerWrapper.collectibles;
        closedCarModels.AddRange(carModelsSO);

        foreach (string itemName in collectedItems)
        {
            CarModelSO collectible = closedCarModels.Find(item => item.Name == itemName);

            if (collectible != null)
            {
                openedCarModels.Add(collectible);
                closedCarModels.Remove(collectible);
            }
        }

        UpdateUI(openedCarModels, closedCarModels);
    }

    public void InitializeUI()
    {
        if (isFirstLoad)
            CreateButtons();

        LoadCarModelsSO();
    }
    public void SelectCurrentButton()
    {
        Transform buttonTransform = buttons[0].transform;

        for (int i = 0; i < openedCarModels.Count; i++)
        {
            if (openedCarModels[i] == (CarModelSO)currentCarModel)
                buttonTransform = buttons[i].transform;
        }

        carTabSwitcher.SelectButton(buttonTransform);
    }

    public void FillListBySO(List<CarModelSO> carModels)
    {
        carModelsSO.AddRange(carModels);
    }

    public void UpdateCarStatWindow()
    {
        CarModelSO carModelSO = (CarModelSO)currentCarModel;
        float currentAccel = carModelSO.Acceleration / maxAcceleration;
        float currentHandleability = carModelSO.Handleability / maxHandleability;
        accelerationImage.fillAmount = currentAccel;
        handleability.fillAmount = currentHandleability;
    }

    public void SetCurrentModel(CarModelSO characterCollectible)
    {
        currentMeshFilter.mesh = characterCollectible.Mesh;
        currentCarModel = characterCollectible;
        SelectCurrentButton();
        UpdateCarStatWindow();
    }
}

