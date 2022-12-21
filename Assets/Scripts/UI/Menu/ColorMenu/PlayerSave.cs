using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerSave : MonoBehaviour
{
    [SerializeField] private CarModelSwitcher carModelSwitcher;
    [SerializeField] private CarColorSwitcher carColorSwitcher;
    [SerializeField] private CharacterTabSwitcher characterSwitcher;

    [SerializeField] private PlayerLoad playerLoad;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += playerLoad.LoadPlayerItems;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent += playerLoad.LoadPlayerItems;
    }

    void Start()
    {

        if (YandexGame.SDKEnabled == true)
        {
            playerLoad.LoadPlayerItems();
        }
    }

    public void SavePlayer()
    {
        CollectibleSO characterItem;
        CollectibleSO carColorItem;
        CollectibleSO carModelItem;

        if (characterSwitcher.CurrentSwitcher.CurrentCharacter != null)
            characterItem = characterSwitcher.CurrentSwitcher.CurrentCharacter;
        else
            characterItem = playerLoad.DefaultCharacter;

        if (carColorSwitcher.CurrentCarColor != null)
            carColorItem = carColorSwitcher.CurrentCarColor;
        else
            carColorItem = playerLoad.DefaultCarColor;

        if (carModelSwitcher.CurrentCarModel != null)
            carModelItem = carModelSwitcher.CurrentCarModel;
        else
            carModelItem = playerLoad.DefaultCarModel;

        YandexGame.savesData.currentCharacterItem = characterItem.Name;
        YandexGame.savesData.currentCarColorItem = carColorItem.Name;
        YandexGame.savesData.currentCarModelItem = carModelItem.Name;

        YandexGame.SaveProgress();
    }
}
