using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CustomizeMenuManager : MenuManager
{
    [SerializeField] private CarTabSwitcher carTabSwitcher;
    [SerializeField] private CharacterTabSwitcher characterTabSwitcher;


    private void SetSavedSO()
    {
        characterTabSwitcher.SetSavedCharacter(playerLoad.CurrentCharacter);
        carTabSwitcher.SetSavedCar(playerLoad.CurrentCarColor, playerLoad.CurrentCarModel);
    }

    protected override void SavePlayer()
    {
        CollectibleSO characterItem = characterTabSwitcher.CurrentSwitcher.CurrentCharacter;
        CollectibleSO carColorItem = carTabSwitcher.CarColorSwitcher.CurrentCarColor;
        CollectibleSO carModelItem = carTabSwitcher.CarModelSwitcher.CurrentCarModel;

        YandexGame.savesData.playerWrapper.currentCharacterItem = characterItem.Name;
        YandexGame.savesData.playerWrapper.currentCarColorItem = carColorItem.Name;
        YandexGame.savesData.playerWrapper.currentCarModelItem = carModelItem.Name;

        YandexGame.SaveProgress();
    }

    public override void SaveDefaultSO()
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
    }

    public override void OpenMenu()
    {
        base.OpenMenu();
        InitializeMenu();
    }
    public override void CloseMenu()
    {
        SavePlayer();
        base.CloseMenu();
    }

    public override void InitializeMenu()
    {
        playerLoad.LoadPlayerItems();
        SetSavedSO();
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

        Debug.Log($"В нашей коллекции {YandexGame.savesData.playerWrapper.maps.Count} карт");
        foreach (MapInfo map in YandexGame.savesData.playerWrapper.maps)
        {
            Debug.Log($"У нас есть карта {map.mapName}");
        }

    }

    public void ResetProgress()//тестовый метод
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
