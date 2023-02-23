using System.Collections.Generic;
using UnityEngine;
using YG;

public class CharacterModelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private CharacterType characterType;
    [SerializeField] private CharacterTabSwitcher tabSwitcher;
    [SerializeField] private GameObject newCollectiblesWarning;
    private CollectibleSO currentCharacter;

    private bool isFirstLoad = true;

    private Transform currentButton;
    private Transform currentCharacterTransform;
    private List<CharacterModelSO> charactersSO = new List<CharacterModelSO>();
    private List<CharacterModelSO> openedCharacters = new List<CharacterModelSO>();
    private List<CharacterModelSO> closedCharacters = new List<CharacterModelSO>();
    private List<CollectibleSO> newCollectibles = new List<CollectibleSO>();
    private List<ButtonCollectibleUI> buttons = new List<ButtonCollectibleUI>();


    public bool HaveNewCollectibles => newCollectibles.Count != 0 ? true : false;
    public CollectibleSO CurrentCharacter { get => currentCharacter; }
    public Transform CurrentButton { get => currentButton; set => currentButton = value; }
    public Transform CurrentCharacterTransform { set => currentCharacterTransform = value; }

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

    private void UpdateUI(List<CharacterModelSO> openCharacters, List<CharacterModelSO> closedCharacters)
    {
        newCollectiblesWarning.SetActive(HaveNewCollectibles);

        for (int i = 0; i < openCharacters.Count; i++)
        {
            buttons[i].Image.sprite = openCharacters[i].Sprite;
            buttons[i].ClosedImage.gameObject.SetActive(false);
            buttons[i].CollectibleSO = openCharacters[i];
            buttons[i].Button.onClick.RemoveAllListeners();
            CheckNewCollectible(buttons[i]);

            CharacterModelSO characterModel = (CharacterModelSO)buttons[i].CollectibleSO;
            buttons[i].Button.onClick.AddListener(() => SetCurrentCharacter(characterModel));
        }

        for (int i = 0; i < closedCharacters.Count; i++)
        {
            int j = i + openCharacters.Count;
            buttons[j].Image.sprite = closedCharacters[i].Sprite;
            buttons[j].ClosedImage.gameObject.SetActive(true);
            buttons[j].CollectibleSO = closedCharacters[i];
            buttons[j].Button.onClick.RemoveAllListeners();
        }
    }

    private void LoadCharactersSO()
    {
        openedCharacters.Clear();
        closedCharacters.Clear();
        newCollectibles.Clear();

        List<string> collectedItems = YandexGame.savesData.playerWrapper.collectibles;
        List<string> newItems = YandexGame.savesData.playerWrapper.newCollectibles;

        closedCharacters.AddRange(charactersSO);

        foreach (string itemName in newItems)
        {
            CollectibleSO newCollectible = charactersSO.Find(item => item.Name == itemName);

            if (newCollectible != null)
                newCollectibles.Add(newCollectible);
        }

        foreach (string itemName in collectedItems)
        {
            CharacterModelSO collectible = closedCharacters.Find(item => item.Name == itemName);

            if (collectible != null)
            {
                openedCharacters.Add(collectible);
                closedCharacters.Remove(collectible);
            }
        }

        UpdateUI(openedCharacters, closedCharacters);
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

        LoadCharactersSO();
    }

    public void SelectCurrentButton()
    {
        Transform buttonTransform = buttons[0].transform;

        for (int i = 0; i < openedCharacters.Count; i++)
        {
            if (openedCharacters[i] == (CharacterModelSO)currentCharacter)
                buttonTransform = buttons[i].transform;
        }

        tabSwitcher.SelectButton(buttonTransform);
    }

    public void LoadSOSubscribe()
    {
        SOLoader.instance.EndLoadingEvent += () =>
        {
            List<CharacterModelSO> charList = SOLoader.instance.GetSOList<CharacterModelSO>();
            foreach (CharacterModelSO character in charList)
            {
                if (character.CharacterType == characterType)
                    charactersSO.Add(character);
            }
        };
    }

    public void FillListBySO(List<CharacterModelSO> characters)//Legacy
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].CharacterType == characterType)
                charactersSO.Add(characters[i]);
        }
    }

    public bool FindStartCharacter(CharacterModelSO characterSO)
    {
        bool isFounded = false;
        for (int i = 0; i < charactersSO.Count; i++)
        {
            if (charactersSO[i] == characterSO)
                isFounded = true;
        }
        return isFounded;
    }

    public void SetCurrentCharacter(CharacterModelSO characterCollectible)
    {
        GameObject newCharacter = Instantiate(characterCollectible.Prefab, currentCharacterTransform);
        newCharacter.transform.parent = null;
        Destroy(currentCharacterTransform.gameObject);
        tabSwitcher.UpdateCurrentCharacter(newCharacter.transform);
        currentCharacter = characterCollectible;
        tabSwitcher.CurrentSwitcher = this;
        SelectCurrentButton();
    }


}
