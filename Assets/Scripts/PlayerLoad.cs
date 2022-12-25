using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerLoad : MonoBehaviour
{

    [SerializeField] private CharacterModelSO currentCharacter;
    [SerializeField] private CarColorSO currentCarColor;
    [SerializeField] private CarModelSO currentCarModel;

    [SerializeField] private CharacterModelSO defaultCharacter;
    [SerializeField] private CarColorSO defaultCarColor;
    [SerializeField] private CarModelSO defaultCarModel;

    [SerializeField] private List<CharacterModelSO> characterItems;
    [SerializeField] private List<CarColorSO> carColorsItems;
    [SerializeField] private List<CarModelSO> carModelsItems;

    public CharacterModelSO CurrentCharacter => currentCharacter;
    public CarColorSO CurrentCarColor => currentCarColor;
    public CarModelSO CurrentCarModel => currentCarModel;
    public CharacterModelSO DefaultCharacter { get => defaultCharacter; }
    public CarColorSO DefaultCarColor { get => defaultCarColor; }
    public CarModelSO DefaultCarModel { get => defaultCarModel; }

    private void Awake()
    {
        List<CharacterModelSO> characterSO = CollectibleLoader.LoadCollectiblesByType<CharacterModelSO>();
        List<CarColorSO> carColorsSO = CollectibleLoader.LoadCollectiblesByType<CarColorSO>();
        List<CarModelSO> carModelsSO = CollectibleLoader.LoadCollectiblesByType<CarModelSO>();

        characterItems.AddRange(characterSO);
        carColorsItems.AddRange(carColorsSO);
        carModelsItems.AddRange(carModelsSO);
    }
    public void LoadPlayerItems()
    {
        if (YandexGame.savesData.playerWrapper.currentCharacterItem != null)
        {
            currentCharacter = characterItems.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCharacterItem);
        }
        else
        {
            currentCharacter = defaultCharacter;
        }

        if (YandexGame.savesData.playerWrapper.currentCarColorItem != null)
            currentCarColor = carColorsItems.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarColorItem);
        else
            currentCarColor = defaultCarColor;

        if (YandexGame.savesData.playerWrapper.currentCarModelItem != null)
            currentCarModel = carModelsItems.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarModelItem);
        else
            currentCarModel = defaultCarModel;
    }

    private void OnDestroy()
    {
        currentCharacter = null;
        currentCarColor = null;
        currentCarModel = null;

        defaultCharacter = null;
        defaultCarColor = null;
        defaultCarModel= null;

        characterItems= null;
        carColorsItems= null;
        carModelsItems= null;
    }
}
