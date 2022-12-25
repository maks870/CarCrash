using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerSave : MonoBehaviour
{
    [SerializeField] private CarTabSwitcher carTabSwitcher;
    [SerializeField] private CharacterTabSwitcher characterTabSwitcher;

    [SerializeField] private PlayerLoad playerLoad;

    private bool isInitializeProcess = false;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += InitializeSO;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= InitializeSO;
    }

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
            InitializeSO();
    }

    private void SetSavedSO()
    {
        Debug.Log("ZALUPA");
        Debug.Log(playerLoad.CurrentCharacter);
        characterTabSwitcher.SetSavedCharacter(playerLoad.CurrentCharacter);
        carTabSwitcher.SetSavedCar(playerLoad.CurrentCarColor, playerLoad.CurrentCarModel);
        isInitializeProcess = false;
    }

    private void SaveDefaultSO()
    {
        CollectibleSO characterItem = playerLoad.CurrentCharacter;
        CollectibleSO carColorItem = playerLoad.CurrentCarColor;
        CollectibleSO carModelItem = playerLoad.CurrentCarModel;

        YandexGame.savesData.playerWrapper.collectibles.Add(characterItem.Name);
        YandexGame.savesData.playerWrapper.collectibles.Add(carColorItem.Name);
        YandexGame.savesData.playerWrapper.collectibles.Add(carModelItem.Name);

        YandexGame.savesData.playerWrapper.currentCharacterItem = characterItem.Name;
        YandexGame.savesData.playerWrapper.currentCarColorItem = carColorItem.Name;
        YandexGame.savesData.playerWrapper.currentCarModelItem = carModelItem.Name;

        Debug.Log(characterItem.Name);
        Debug.Log(YandexGame.savesData.playerWrapper.collectibles[0]);
        YandexGame.SaveProgress();
    }

    public void InitializeSO()
    {
        Debug.Log(isInitializeProcess);

        if (isInitializeProcess)
        {
            Debug.Log("RETURN");
            return;
        }

        isInitializeProcess = true;
        playerLoad.LoadPlayerItems();

        Debug.Log("SETSAVEDSO");
        if (YandexGame.savesData.isFirstSession2)
        {
            SaveDefaultSO();
            YandexGame.savesData.isFirstSession2 = false;
        }

        Debug.Log("SETSAVEDSO");
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

    public void ShowOurCollectibles()
    {
        foreach (string str in YandexGame.savesData.playerWrapper.collectibles)
        {
            Debug.Log("У нас есть " + str);
        }
    }

    public void ResetProgress()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
