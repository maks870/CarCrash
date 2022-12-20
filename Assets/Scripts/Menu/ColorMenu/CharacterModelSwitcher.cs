using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CharacterModelSwitcher : MonoBehaviour
{
    [SerializeField] private MeshFilter currentMeshFilter;
    [SerializeField] private MeshRenderer currentRenderer;
    [SerializeField] private GameObject button;
    [SerializeField] private List<ButtonCollectibleUI> buttons = new List<ButtonCollectibleUI>();
    [SerializeField] private List<ÑollectibleSO> charactersSO = new List<ÑollectibleSO>();

    private bool isButtonsCreated = false;
    private List<ÑollectibleSO> openedCharacters = new List<ÑollectibleSO>();
    private List<ÑollectibleSO> closedCharacters = new List<ÑollectibleSO>();


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
            LoadCharactersSO();
        }
    }

    void Update()
    {
    }

    private void InitializeUI()
    {
        if (!isButtonsCreated)
            CreateButtons();

        LoadCharactersSO();
    }

    private void CreateButtons()
    {
        for (int i = 0; i < charactersSO.Count; i++)
        {
            ButtonCollectibleUI newButton = Instantiate(button, transform).GetComponent<ButtonCollectibleUI>();
            buttons.Add(newButton);
        }

        isButtonsCreated = true;
    }

    private void UpdateUI(List<ÑollectibleSO> openColors, List<ÑollectibleSO> closedColors)
    {
        for (int i = 0; i < openColors.Count; i++)
        {
            buttons[i].Image.sprite = openColors[i].Sprite;
            buttons[i].ClosedImage.SetActive(false);
            buttons[i].CollectibleSO = openColors[i];
            buttons[i].Button.onClick.RemoveAllListeners();
            buttons[i].Button.onClick.AddListener(() => SetCurrentModel(buttons[i].CollectibleSO));
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

    public void LoadCharactersSO()
    {
        openedCharacters.Clear();
        List<string> collectedItems = YandexGame.savesData.collectedItems;
        closedCharacters.AddRange(charactersSO);

        foreach (string itemName in collectedItems)
        {
            ÑollectibleSO collectible = closedCharacters.Find(item => item.Sprite.name == itemName);
            openedCharacters.Add(collectible);
            closedCharacters.Remove(collectible);
        }

        UpdateUI(openedCharacters, closedCharacters);
    }

    public void SetCurrentModel(ÑollectibleSO collectible)
    {
        CharacterCollectibleSO characterCollectible = (CharacterCollectibleSO)collectible;
        currentMeshFilter.sharedMesh = characterCollectible.Prefab.GetComponent<MeshFilter>().mesh;
        currentRenderer.sharedMaterials = characterCollectible.Prefab.GetComponent<MeshRenderer>().materials;
    }
}
