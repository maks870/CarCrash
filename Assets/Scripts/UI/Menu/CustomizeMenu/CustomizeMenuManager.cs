using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CustomizeMenuManager : MonoBehaviour
{
    [SerializeField] private CarTabSwitcher carTabSwitcher;
    [SerializeField] private CharacterTabSwitcher characterTabSwitcher;
    [SerializeField] private PlayerLoad playerLoad;
    [SerializeField] private GameObject objectUI;

    private bool isInitializeProcess = false;
    private bool isStartLoad = true;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += InitializeMenu;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= InitializeMenu;
    }

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
            InitializeMenu();
    }

    private void SetSavedSO()
    {
        characterTabSwitcher.SetSavedCharacter(playerLoad.CurrentCharacter);
        carTabSwitcher.SetSavedCar(playerLoad.CurrentCarColor, playerLoad.CurrentCarModel);
        isInitializeProcess = false;
    }

    private void SaveDefaultSO()
    {
        CollectibleSO character = playerLoad.DefaultCharacter;
        CollectibleSO carColor = playerLoad.DefaultCarColor;
        CollectibleSO carModel = playerLoad.DefaultCarModel;

        YandexGame.savesData.playerWrapper.collectibles.Add(character.Name);
        YandexGame.savesData.playerWrapper.collectibles.Add(carColor.Name);
        YandexGame.savesData.playerWrapper.collectibles.Add(carModel.Name);

        YandexGame.savesData.playerWrapper.currentCharacterItem = character.Name;
        YandexGame.savesData.playerWrapper.currentCarColorItem = carColor.Name;
        YandexGame.savesData.playerWrapper.currentCarModelItem = carModel.Name;

        YandexGame.savesData.isFirstSession2 = false;

        YandexGame.SaveProgress();
    }

    public void InitializeMenu()
    {
        if (isInitializeProcess)
            return;

        isInitializeProcess = true;

        if (YandexGame.savesData.isFirstSession2)
            SaveDefaultSO();

        playerLoad.LoadPlayerItems();
        SetSavedSO();

        if (!isStartLoad)
            return;

        objectUI.SetActive(false);
        isStartLoad = false;
    }

    public void SavePlayer()
    {

        CollectibleSO characterItem = characterTabSwitcher.CurrentSwitcher.CurrentCharacter;
        CollectibleSO carColorItem = carTabSwitcher.CarColorSwitcher.CurrentCarColor;
        CollectibleSO carModelItem = carTabSwitcher.CarModelSwitcher.CurrentCarModel;


        YandexGame.savesData.playerWrapper.currentCharacterItem = characterItem.Name;
        YandexGame.savesData.playerWrapper.currentCarColorItem = carColorItem.Name;
        YandexGame.savesData.playerWrapper.currentCarModelItem = carModelItem.Name;

        YandexGame.SaveProgress();
    }

    public void AddCharacter()//тестовый метод
    {
        List<CharacterModelSO> characters = SOLoader.LoadSOByType<CharacterModelSO>();
        int rand = Random.Range(0, characters.Count);
        YandexGame.savesData.playerWrapper.collectibles.Add(characters[rand].Name);
        Debug.Log("Получен персонаж " + characters[rand].Name);
        YandexGame.SaveProgress();
    }

    public void ShowOurCollectibles()//тестовый метод
    {
        Debug.Log($"В нашей коллекции {YandexGame.savesData.playerWrapper.collectibles.Count} элементов");
        foreach (string str in YandexGame.savesData.playerWrapper.collectibles)
        {
            Debug.Log("У нас есть " + str);
        }
    }

    public void ResetProgress()//тестовый метод
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
