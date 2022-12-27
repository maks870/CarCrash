using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerSave : MonoBehaviour
{
    [SerializeField] private CarTabSwitcher carTabSwitcher;
    [SerializeField] private CharacterTabSwitcher characterTabSwitcher;
    [SerializeField] private MapSwitcher mapSwitcher;
    [SerializeField] private PlayerLoad playerLoad;
    [SerializeField] private GameObject customizeRoom;
    [SerializeField] private List<GameObject> gamemodsPanels;

    private bool isInitializeProcess = false;
    private bool isStartLoad = true;

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
        characterTabSwitcher.SetSavedCharacter(playerLoad.CurrentCharacter);
        carTabSwitcher.SetSavedCar(playerLoad.CurrentCarColor, playerLoad.CurrentCarModel);
        mapSwitcher.InitializeUI();

        isInitializeProcess = false;
    }

    private void SaveDefaultSO()
    {
        CollectibleSO character = playerLoad.DefaultCharacter;
        CollectibleSO carColor = playerLoad.DefaultCarColor;
        CollectibleSO carModel = playerLoad.DefaultCarModel;
        MapSO map = playerLoad.DefaultMap;


        YandexGame.savesData.playerWrapper.collectibles.Add(character.Name);
        YandexGame.savesData.playerWrapper.collectibles.Add(carColor.Name);
        YandexGame.savesData.playerWrapper.collectibles.Add(carModel.Name);

        YandexGame.savesData.playerWrapper.currentCharacterItem = character.Name;
        YandexGame.savesData.playerWrapper.currentCarColorItem = carColor.Name;
        YandexGame.savesData.playerWrapper.currentCarModelItem = carModel.Name;

        MapInfo mapInfo = new MapInfo(map.Name);
        YandexGame.savesData.playerWrapper.maps.Add(mapInfo);
        Debug.Log(mapInfo.mapName);
        Debug.Log(YandexGame.savesData.playerWrapper.maps[0].mapName);

        YandexGame.SaveProgress();
    }

    public void InitializeSO()
    {
        if (isInitializeProcess)
            return;

        isInitializeProcess = true;

        if (YandexGame.savesData.isFirstSession2)
        {
            Debug.Log("Первая загрузка");
            SaveDefaultSO();
            YandexGame.savesData.isFirstSession2 = false;
        }

        playerLoad.LoadPlayerItems();
        SetSavedSO();

        if (isStartLoad)
        {
            Debug.Log("Прячкем панелтьки");
            customizeRoom.SetActive(false);

            for (int i = 0; i < gamemodsPanels.Count; i++)
            {
                gamemodsPanels[i].SetActive(false);
            }

            isStartLoad = false;
        }

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

    public void AddCharacter()
    {
        List<CharacterModelSO> characters = SOLoader.LoadSOByType<CharacterModelSO>();
        int rand = Random.Range(0, characters.Count);
        YandexGame.savesData.playerWrapper.collectibles.Add(characters[rand].Name);
        Debug.Log("Получен персонаж " + characters[rand].Name);
        YandexGame.SaveProgress();
    }

    public void ShowOurCollectibles()
    {
        Debug.Log($"В нашей коллекции {YandexGame.savesData.playerWrapper.collectibles.Count} элементов");
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
