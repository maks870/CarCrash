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

    [SerializeField] private List<CollectibleSO> characterItems;
    [SerializeField] private List<CollectibleSO> carColorsItems;
    [SerializeField] private List<CollectibleSO> carModelsItems;

    public CharacterModelSO CurrentCharacter => currentCharacter;
    public CarColorSO CurrentCarColor => currentCarColor;
    public CarModelSO CurrentCarModel => currentCarModel;
    public CharacterModelSO DefaultCharacter { get => defaultCharacter; }
    public CarColorSO DefaultCarColor { get => defaultCarColor; }
    public CarModelSO DefaultCarModel { get => defaultCarModel; }

    public void LoadPlayerItems()
    {
        if (YandexGame.savesData.playerWrapper.currentCharacterItem != null)
            currentCharacter = (CharacterModelSO)characterItems.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCharacterItem);
        else
            currentCharacter = defaultCharacter;

        if (YandexGame.savesData.playerWrapper.currentCarColorItem != null)
            currentCarColor = (CarColorSO)carColorsItems.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarColorItem);
        else
            currentCarColor = defaultCarColor;

        if (YandexGame.savesData.playerWrapper.currentCarModelItem != null)
            currentCarModel = (CarModelSO)carModelsItems.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarModelItem);
        else
            currentCarModel = defaultCarModel;
    }
}
