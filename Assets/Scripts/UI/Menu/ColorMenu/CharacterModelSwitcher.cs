using System.Collections.Generic;
using UnityEngine;
using YG;

public class CharacterModelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private CharacterType characterType;
    private CharacterTabSwitcher tabSwitcher;
    private CollectibleSO currentCharacter;

    private bool isFirstLoad = true;

    private Transform currentCharacterTransform;
    private List<CharacterModelSO> charactersSO = new List<CharacterModelSO>();
    private List<CharacterModelSO> openedCharacters = new List<CharacterModelSO>();
    private List<CharacterModelSO> closedCharacters = new List<CharacterModelSO>();
    private List<ButtonCollectibleUI> buttons = new List<ButtonCollectibleUI>();

    public CollectibleSO CurrentCharacter { get => currentCharacter; }
    public CharacterTabSwitcher TabSwitcher { set => tabSwitcher = value; }
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
        for (int i = 0; i < openCharacters.Count; i++)
        {
            buttons[i].Image.sprite = openCharacters[i].Sprite;
            buttons[i].ClosedImage.gameObject.SetActive(false);
            buttons[i].CollectibleSO = openCharacters[i];
            buttons[i].Button.onClick.RemoveAllListeners();
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
        List<string> collectedItems = YandexGame.savesData.playerWrapper.collectibles;
        closedCharacters.AddRange(charactersSO);

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

    public void InitializeUI()
    {
        if (isFirstLoad)
            CreateButtons();

        LoadCharactersSO();
    }

    public void FillListBySO(List<CharacterModelSO> characters)
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
        GameObject character = Instantiate(characterCollectible.Prefab, currentCharacterTransform);
        character.transform.parent = null;
        Destroy(currentCharacterTransform.gameObject);
        tabSwitcher.UpdateCurrentCharacter(character.transform);
        currentCharacter = characterCollectible;
        tabSwitcher.CurrentSwitcher = this;
    }


}
