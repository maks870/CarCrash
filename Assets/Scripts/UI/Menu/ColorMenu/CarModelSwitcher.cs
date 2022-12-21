using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CarModelSwitcher : MonoBehaviour
{
    [SerializeField] private MeshFilter currentMeshFilter;
    [SerializeField] private GameObject button;
    [SerializeField] private List<ButtonCollectibleUI> buttons = new List<ButtonCollectibleUI>();
    [SerializeField] private List<CarModelSO> carModelsSO = new List<CarModelSO>();

    private CollectibleSO currentCarModel;

    private bool isButtonsCreated = false;

    private List<CarModelSO> openedCharacters = new List<CarModelSO>();
    private List<CarModelSO> closedCharacters = new List<CarModelSO>();

    public CollectibleSO CurrentCarModel { get => currentCarModel; }

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
        if (!isButtonsCreated)
            CreateButtons();

        LoadCarModelsSO();
    }

    private void CreateButtons()
    {
        for (int i = 0; i < carModelsSO.Count - 1; i++)
        {
            ButtonCollectibleUI newButton = Instantiate(button, transform).GetComponent<ButtonCollectibleUI>();
            buttons.Add(newButton);
        }

        isButtonsCreated = true;
    }

    private void UpdateUI(List<CarModelSO> openColors, List<CarModelSO> closedColors)
    {
        for (int i = 0; i < openColors.Count; i++)
        {
            buttons[i].Image.sprite = openColors[i].Sprite;
            buttons[i].ClosedImage.SetActive(false);
            buttons[i].CollectibleSO = openColors[i];
            buttons[i].Button.onClick.RemoveAllListeners();
            buttons[i].Button.onClick.AddListener(() => SetCurrentModel((CarModelSO)buttons[i].CollectibleSO));
        }

        for (int i = 0; i < closedColors.Count; i++)
        {
            int j = i + openColors.Count;
            buttons[j].Image.sprite = closedColors[i].Sprite;
            buttons[j].ClosedImage.SetActive(true);
            buttons[j].CollectibleSO = closedColors[i];
            buttons[j].Button.onClick.RemoveAllListeners();
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
        currentMeshFilter.mesh = characterCollectible.MeshFilter.mesh;
        currentCarModel = characterCollectible;
    }
}

