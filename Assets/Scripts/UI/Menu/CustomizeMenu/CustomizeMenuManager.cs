using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using YG;

public class CustomizeMenuManager : MenuManager
{
    [SerializeField] private CarTabSwitcher carTabSwitcher;
    [SerializeField] private CharacterTabSwitcher characterTabSwitcher;
    [SerializeField] private GameObject[] waiterBanners;
    public static Action<bool> ExitCustomizeMenu;

    private void SetSavedSO()
    {
        characterTabSwitcher.SetSavedCharacter(playerLoad.CurrentCharacter);
        carTabSwitcher.SetSavedCar(playerLoad.CurrentCarColor, playerLoad.CurrentCarModel);
    }

    private IEnumerator SOLoadWaiter()
    {
        foreach (GameObject banner in waiterBanners)
        {
            banner.SetActive(true);

        }

        while (!SOLoader.instance.IsResourcesLoaded)
        {
            yield return null;
        }

        InitializeMenu();

        foreach (GameObject banner in waiterBanners)
        {
            banner.SetActive(false);
        }
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
        SOLoader.instance.LoadAssetReference<CharacterModelSO>(playerLoad.DefaultCharacter, (result) =>
        {
            YandexGame.savesData.playerWrapper.collectibles.Add(result.Name);
            YandexGame.savesData.playerWrapper.currentCharacterItem = result.Name;
            Addressables.Release(result);
        });

        SOLoader.instance.LoadAssetReference<CarColorSO>(playerLoad.DefaultCarColor, (result) =>
        {
            YandexGame.savesData.playerWrapper.collectibles.Add(result.Name);
            YandexGame.savesData.playerWrapper.currentCarColorItem = result.Name;
            Addressables.Release(result);
        });

        SOLoader.instance.LoadAssetReference<CarModelSO>(playerLoad.DefaultCarModel, (result) =>
        {
            YandexGame.savesData.playerWrapper.collectibles.Add(result.Name);
            YandexGame.savesData.playerWrapper.currentCarModelItem = result.Name;
            Addressables.Release(result);
        });
    }

    public override void OpenMenu()
    {
        base.OpenMenu();
        StartCoroutine(SOLoadWaiter());
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

    public void AddLootboxTEST()//�������� �����
    {
        EarningManager.AddLootbox();
        YandexGame.SaveProgress();
    }

    public void OpenAllMapsTEST()//�������� �����
    {
        List<MapSO> mapList = SOLoader.instance.GetSOList<MapSO>();

        foreach (MapSO map in mapList)
        {
            MapInfo mapInfo = new MapInfo(map.name, map.MaxPoints);
            YandexGame.savesData.playerWrapper.maps.Add(mapInfo);
            Debug.Log("�������� ����� " + mapInfo.mapName);
        }
        YandexGame.SaveProgress();
    }

    public void OpenAllCarModelsTEST()//�������� �����
    {
        List<CarModelSO> carList = SOLoader.instance.GetSOList<CarModelSO>();

        foreach (CarModelSO car in carList)
        {
            YandexGame.savesData.playerWrapper.collectibles.Add(car.Name);
            Debug.Log("������� ���������� " + car.Name);
        }

        YandexGame.SaveProgress();
        InitializeMenu();
    }

    public void ShowOurCollectiblesTEST()//�������� �����
    {
        Debug.Log($"� ����� ��������� {YandexGame.savesData.playerWrapper.collectibles.Count} ���������");
        foreach (string str in YandexGame.savesData.playerWrapper.collectibles)
        {
            Debug.Log("� ��� ���� " + str);
        }

        Debug.Log($"� ����� ��������� {YandexGame.savesData.playerWrapper.maps.Count} ����");
        foreach (MapInfo map in YandexGame.savesData.playerWrapper.maps)
        {
            Debug.Log($"� ��� ���� ����� {map.mapName}");
        }
    }
}
