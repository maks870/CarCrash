using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CustomizeMenuManager : MenuManager
{
    [SerializeField] private CarTabSwitcher carTabSwitcher;
    [SerializeField] private CharacterTabSwitcher characterTabSwitcher;
    public static Action<bool> ExitCustomizeMenu;
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

    public override void SOLoaderSubscribe()
    {
        characterTabSwitcher.SubscribeSwitchers();
        carTabSwitcher.SubscribeSwitchers();
    }

    public override void SaveDefaultSO()
    {
        SOLoader.LoadAssetReference<CharacterModelSO>(playerLoad.DefaultCharacter, (result) =>
        {
            YandexGame.savesData.playerWrapper.collectibles.Add(result.Name);
            YandexGame.savesData.playerWrapper.currentCharacterItem = result.Name;
        });

        SOLoader.LoadAssetReference<CarColorSO>(playerLoad.DefaultCarColor, (result) =>
        {
            YandexGame.savesData.playerWrapper.collectibles.Add(result.Name);
            YandexGame.savesData.playerWrapper.currentCarColorItem = result.Name;
        });

        SOLoader.LoadAssetReference<CarModelSO>(playerLoad.DefaultCarModel, (result) =>
        {
            YandexGame.savesData.playerWrapper.collectibles.Add(result.Name);
            YandexGame.savesData.playerWrapper.currentCarModelItem = result.Name;
        });
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

    public void AddLootboxTEST()//тестовый метод
    {
        EarningManager.AddLootbox();
        YandexGame.SaveProgress();
    }

    public void OpenAllMapsTEST()//тестовый метод
    {
        List<MapSO> mapList = (List<MapSO>)SOLoader.mapHandle.Result;

        foreach (MapSO map in mapList)
        {
            MapInfo mapInfo = new MapInfo(map.name, map.MaxPoints);
            YandexGame.savesData.playerWrapper.maps.Add(mapInfo);
            Debug.Log("Получена карта " + mapInfo.mapName);
        }
        YandexGame.SaveProgress();
    }

    public void OpenAllCarModelsTEST()//тестовый метод
    {
        List<CarModelSO> carList = (List<CarModelSO>)SOLoader.carModelHandle.Result;

        foreach (CarModelSO car in carList)
        {
            YandexGame.savesData.playerWrapper.collectibles.Add(car.Name);
            Debug.Log("Получен автомобиль " + car.Name);
        }

        YandexGame.SaveProgress();
        InitializeMenu();
    }

    public void ShowOurCollectiblesTEST()//тестовый метод
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
}
