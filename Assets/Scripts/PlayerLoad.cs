using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerLoad : MonoBehaviour
{

    [SerializeField] private GameObject currentCharacter;
    [SerializeField] private MeshRenderer currentCarColor;
    [SerializeField] private MeshFilter currentCarModel;

    [SerializeField] private CharacterModelSO defaultCharacter;
    [SerializeField] private CarColorSO defaultCarColor;
    [SerializeField] private CarModelSO defaultCarModel;

    [SerializeField] private List<CollectibleSO> characterItems;
    [SerializeField] private List<CollectibleSO> carColorsItems;
    [SerializeField] private List<CollectibleSO> carModelsItems;

    public CharacterModelSO DefaultCharacter { get => defaultCharacter; }
    public CarColorSO DefaultCarColor { get => defaultCarColor; }
    public CarModelSO DefaultCarModel { get => defaultCarModel; }

    public void LoadPlayerItems()
    {
        CharacterModelSO characterItem;
        CarColorSO carColorItem;
        CarModelSO carModelItem;

        if (YandexGame.savesData.currentCharacterItem != null)
            characterItem = (CharacterModelSO)characterItems.Find(item => item.Name == YandexGame.savesData.currentCharacterItem);
        else
            characterItem = defaultCharacter;

        if (YandexGame.savesData.currentCarColorItem != null)
            carColorItem = (CarColorSO)carColorsItems.Find(item => item.Name == YandexGame.savesData.currentCarColorItem);
        else
            carColorItem = defaultCarColor;

        if (YandexGame.savesData.currentCarModelItem != null)
            carModelItem = (CarModelSO)carModelsItems.Find(item => item.Name == YandexGame.savesData.currentCarModelItem);
        else
            carModelItem = defaultCarModel;

        currentCharacter = characterItem.Prefab;
        currentCarColor.material.mainTexture = carColorItem.Texture;
        currentCarModel.mesh = carModelItem.Prefab.GetComponent<MeshFilter>().mesh;
    }
}
