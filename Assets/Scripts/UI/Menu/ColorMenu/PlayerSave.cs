using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerSave : MonoBehaviour
{
    [SerializeField] private CarModelSwitcher carModelSwitcher;
    [SerializeField] private CarColorSwitcher carColorSwitcher;
    [SerializeField] private CarTabSwitcher carTabSwitcher;
    [SerializeField] private CharacterTabSwitcher characterTabSwitcher;

    [SerializeField] private PlayerLoad playerLoad;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += InitializeSO;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent += InitializeSO;
    }

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
            InitializeSO();
    }
    private void InitializeSO()
    {
        playerLoad.LoadPlayerItems();
        SetSavedSO();
    }

    private void SetSavedSO()
    {
        characterTabSwitcher.SetSavedCharacter(playerLoad.CurrentCharacter);
        carTabSwitcher.SetSavedCar(playerLoad.CurrentCarColor, playerLoad.CurrentCarModel);
    }

    public void SavePlayer()
    {
        //if (characterTabSwitcher.CurrentSwitcher.CurrentCharacter != null)
        //    characterItem = characterTabSwitcher.CurrentSwitcher.CurrentCharacter;
        //else
        //    characterItem = playerLoad.DefaultCharacter;

        //if (carTabSwitcher.CarColorSwitcher.CurrentCarColor != null)
        //    carColorItem = carTabSwitcher.CarColorSwitcher.CurrentCarColor;
        //else
        //    carColorItem = playerLoad.DefaultCarColor;

        //if (carTabSwitcher.CarModelSwitcher.CurrentCarModel != null)
        //    carModelItem = carTabSwitcher.CarModelSwitcher.CurrentCarModel;
        //else
        //    carModelItem = playerLoad.DefaultCarModel;

        CollectibleSO characterItem = characterTabSwitcher.CurrentSwitcher.CurrentCharacter;
        CollectibleSO carColorItem = carTabSwitcher.CarColorSwitcher.CurrentCarColor;
        CollectibleSO carModelItem = carTabSwitcher.CarModelSwitcher.CurrentCarModel;
        YandexGame.savesData.currentCharacterItem = characterItem.Name;
        YandexGame.savesData.currentCarColorItem = carColorItem.Name;
        YandexGame.savesData.currentCarModelItem = carModelItem.Name;

        YandexGame.SaveProgress();
    }
}
