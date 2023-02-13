using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CarModelSwitcher : MonoBehaviour
{
    [SerializeField] private MeshFilter currentMeshFilter;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject carStatWindow;
    [SerializeField] private GameObject newCollectiblesWarning;
    [SerializeField] private float minAcceleration;
    [SerializeField] private float maxAcceleration;
    [SerializeField] private float minHandleability;
    [SerializeField] private float maxHandleability;
    [SerializeField] private Image accelerationImage;
    [SerializeField] private Image handleability;
    [SerializeField] private CarTabSwitcher carTabSwitcher;

    private bool isFirstLoad = true;
    private CollectibleSO currentCarModel;
    private List<CarModelSO> carModelsSO = new List<CarModelSO>();
    private List<CarModelSO> openedCarModels = new List<CarModelSO>();
    private List<CarModelSO> closedCarModels = new List<CarModelSO>();
    private List<CollectibleSO> newCollectibles = new List<CollectibleSO>();
    private List<ButtonCollectibleUI> buttons = new List<ButtonCollectibleUI>();

    public bool HaveNewCollectibles => newCollectibles.Count != 0 ? true : false;
    public CollectibleSO CurrentCarModel => currentCarModel;
    public GameObject CarStatWindow => carStatWindow;

    //private void Awake()
    //{
    //    AwardPresenter.AddedNewCollectibles += UpdateNewCollectibles;
    //}

    //private void UpdateNewCollectibles(List<CollectibleSO> newCollectibles)
    //{
    //    this.newCollectibles.Clear();
    //    this.newCollectibles.AddRange(newCollectibles);
    //}

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
        newCollectiblesWarning.SetActive(HaveNewCollectibles);

        for (int i = 0; i < openCarModels.Count; i++)
        {
            buttons[i].Image.sprite = openCarModels[i].Sprite;
            buttons[i].ClosedImage.gameObject.SetActive(false);
            buttons[i].CollectibleSO = openCarModels[i];
            buttons[i].Button.onClick.RemoveAllListeners();
            CheckNewCollectible(buttons[i]);

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
        newCollectibles.Clear();
        List<string> collectedItems = YandexGame.savesData.playerWrapper.collectibles;
        List<string> newItems = YandexGame.savesData.playerWrapper.newCollectibles;

        closedCarModels.AddRange(carModelsSO);

        foreach (string itemName in newItems)
        {
            CollectibleSO newCollectible = carModelsSO.Find(item => item.Name == itemName);

            if (newCollectible != null)
                newCollectibles.Add(newCollectible);
        }

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

    private void CheckNewCollectible(ButtonCollectibleUI button)
    {
        for (int i = 0; i < newCollectibles.Count; i++)
        {
            if (button.CollectibleSO == newCollectibles[i])
            {
                button.NewCollectibleWarning.SetActive(true);
                button.Button.onClick.AddListener(() => RemoveNewCollectibleWarning(button));
                break;
            }
        }
    }

    private void RemoveNewCollectibleWarning(ButtonCollectibleUI button)
    {
        button.NewCollectibleWarning.SetActive(false);
        newCollectibles.Remove(button.CollectibleSO);
        YandexGame.savesData.playerWrapper.newCollectibles.Remove(button.CollectibleSO.Name);
        newCollectiblesWarning.SetActive(HaveNewCollectibles);
        button.Button.onClick.RemoveListener(() => RemoveNewCollectibleWarning(button));
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
        float currentAccel = Mathf.InverseLerp(minAcceleration, maxAcceleration, carModelSO.Acceleration);
        float currentHandleability = Mathf.InverseLerp(minHandleability, maxHandleability, carModelSO.Handleability);
        accelerationImage.fillAmount = currentAccel;
        handleability.fillAmount = currentHandleability;
    }

    public void SetCurrentModel(CarModelSO characterCollectible)
    {
        Mesh mesh = SOLoader.LoadAsset<Mesh>(characterCollectible.MeshAsset);
        currentMeshFilter.mesh = mesh;
        currentCarModel = characterCollectible;
        SelectCurrentButton();
        UpdateCarStatWindow();
    }
}

