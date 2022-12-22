using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CharacterModelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject currentCharacterObject;
    [SerializeField] private GameObject button;
    [SerializeField] private List<ButtonCollectibleUI> buttons = new List<ButtonCollectibleUI>();
    [SerializeField] private List<CharacterModelSO> charactersSO = new List<CharacterModelSO>();
    private CharacterTabSwitcher tabSwitcher;
    private CollectibleSO currentCharacter;

    private bool isFirstLoad = true;
    public CharacterTabSwitcher TabSwitcher { set => tabSwitcher = value; }
    public CollectibleSO CurrentCharacter { get => currentCharacter; }

    private List<CharacterModelSO> openedCharacters = new List<CharacterModelSO>();
    private List<CharacterModelSO> closedCharacters = new List<CharacterModelSO>();

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
            LoadCharactersSO();
        }
    }

    private void InitializeUI()
    {
        if (isFirstLoad)
            CreateButtons();

        LoadCharactersSO();
    }

    private void CreateButtons()
    {
        buttons.Add(button.GetComponent<ButtonCollectibleUI>());
        for (int i = 0; i < charactersSO.Count - 1; i++)
        {
            ButtonCollectibleUI newButton = Instantiate(button, transform).GetComponent<ButtonCollectibleUI>();
            buttons.Add(newButton);
        }

        isFirstLoad = false;
    }

    private void UpdateUI(List<CharacterModelSO> openColors, List<CharacterModelSO> closedColors)
    {
        for (int i = 0; i < openColors.Count; i++)
        {
            Debug.Log(i + "i");
            buttons[i].Image.sprite = openColors[i].Sprite;
            buttons[i].ClosedImage.SetActive(false);
            buttons[i].CollectibleSO = openColors[i];
            buttons[i].Button.onClick.RemoveAllListeners();
            buttons[i].Button.onClick.AddListener(() => SetCurrentModel((CharacterModelSO)buttons[i].CollectibleSO));
        }
        Debug.Log(openColors.Count);
        for (int i = 0; i < closedColors.Count; i++)
        {
            Debug.Log(j = "j");
            int j = i + openColors.Count;
            buttons[j].Image.sprite = closedColors[i].Sprite;
            buttons[j].ClosedImage.SetActive(true);
            buttons[j].CollectibleSO = closedColors[i];
            buttons[j].Button.onClick.RemoveAllListeners();
        }
        Debug.Log(closedColors.Count);
    }

    public void LoadCharactersSO()
    {
        openedCharacters.Clear();
        List<string> collectedItems = YandexGame.savesData.collectedItems;
        closedCharacters.AddRange(charactersSO);

        foreach (string itemName in collectedItems)
        {
            CharacterModelSO collectible = closedCharacters.Find(item => item.Name == itemName);
            openedCharacters.Add(collectible);
            closedCharacters.Remove(collectible);
        }

        UpdateUI(openedCharacters, closedCharacters);
    }

    public void SetCurrentModel(CharacterModelSO characterCollectible)
    {
        currentCharacterObject = characterCollectible.Prefab;
        currentCharacter = characterCollectible;
        tabSwitcher.CurrentSwitcher = this;
    }
}
