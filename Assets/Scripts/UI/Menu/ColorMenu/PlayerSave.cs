using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerSave : MonoBehaviour
{
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

    private void SetSavedSO()
    {
        characterTabSwitcher.SetSavedCharacter(playerLoad.CurrentCharacter);
        carTabSwitcher.SetSavedCar(playerLoad.CurrentCarColor, playerLoad.CurrentCarModel);
    }

    public void InitializeSO()
    {
        playerLoad.LoadPlayerItems();

        if (YandexGame.savesData.isFirstSession)
        {
            Debug.Log("firstSession");
            SavePlayer();
        }

        SetSavedSO();

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

    public void ResetProgress()
    {
        YandexGame.ResetSaveProgress();
    }
}
