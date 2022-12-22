using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CarModelSwitcher : MonoBehaviour
{
    [SerializeField] private MeshFilter currentMeshFilter;
    [SerializeField] private GameObject button;
    [SerializeField] private List<ButtonCollectibleUI> buttons = new List<ButtonCollectibleUI>();
    [SerializeField] private GameObject carStatWindow;
    [SerializeField] private float maxAcceleration;
    [SerializeField] private float maxHandleability;
    [SerializeField] private Image accelerationImage;
    [SerializeField] private Image handleability;
    [SerializeField] private List<CarModelSO> carModelsSO = new List<CarModelSO>();

    private bool isFirstLoad = true;
    private CollectibleSO currentCarModel;
    private List<CarModelSO> openedCharacters = new List<CarModelSO>();
    private List<CarModelSO> closedCharacters = new List<CarModelSO>();

    public CollectibleSO CurrentCarModel { get => currentCarModel; }
    public GameObject CarStatWindow => carStatWindow;

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

        if (YandexGame.SDKEnabled == true)
        {
            LoadCarModelsSO();
        }
    }

    void Update()
    {
    }

    private void InitializeUI()
    {
        if (isFirstLoad)
            CreateButtons();

        LoadCarModelsSO();
    }

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
    private void UpdateCarStatWindow(CarModelSO carModelSO)
    {
        float currentAccel = carModelSO.Acceleration / maxAcceleration;
        float currentHandleability = carModelSO.Handleability / maxHandleability;
        accelerationImage.fillAmount = currentAccel;
        handleability.fillAmount = currentHandleability;
    }
    private void UpdateUI(List<CarModelSO> openCarModels, List<CarModelSO> closedCarModels)
    {
        for (int i = 0; i < openCarModels.Count; i++)
        {
            buttons[i].Image.sprite = openCarModels[i].Sprite;
            buttons[i].ClosedImage.SetActive(false);
            buttons[i].CollectibleSO = openCarModels[i];
            buttons[i].Button.onClick.RemoveAllListeners();
            buttons[i].Button.onClick.AddListener(() => SetCurrentModel((CarModelSO)buttons[i].CollectibleSO));
            buttons[i].Button.onClick.AddListener(() => UpdateCarStatWindow((CarModelSO)buttons[i].CollectibleSO));
        }

        for (int i = 0; i < closedCarModels.Count; i++)
        {
            int j = i + openCarModels.Count;
            buttons[j].Image.sprite = closedCarModels[i].Sprite;
            buttons[j].ClosedImage.SetActive(true);
            buttons[j].CollectibleSO = closedCarModels[i];
            buttons[j].Button.onClick.RemoveAllListeners();
            buttons[i].Button.onClick.AddListener(() => UpdateCarStatWindow((CarModelSO)buttons[i].CollectibleSO));
        }
    }

    public void LoadCarModelsSO()
    {
        openedCharacters.Clear();
        List<string> collectedItems = YandexGame.savesData.collectedItems;
        closedCharacters.AddRange(carModelsSO);

        foreach (string itemName in collectedItems)
        {
            CarModelSO collectible = closedCharacters.Find(item => item.Name == itemName);
            openedCharacters.Add(collectible);
            closedCharacters.Remove(collectible);
        }

        UpdateUI(openedCharacters, closedCharacters);
    }

    public void SetCurrentModel(CarModelSO characterCollectible)
    {
        currentMeshFilter.mesh = characterCollectible.Mesh;
        currentCarModel = characterCollectible;
    }
}

