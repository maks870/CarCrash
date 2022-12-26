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
    [SerializeField] private MapSO defaultMap;

    private List<CharacterModelSO> characters;
    private List<CarColorSO> carColors;
    private List<CarModelSO> carModels;

    public CharacterModelSO CurrentCharacter => currentCharacter;
    public CarColorSO CurrentCarColor => currentCarColor;
    public CarModelSO CurrentCarModel => currentCarModel;
    public CharacterModelSO DefaultCharacter => defaultCharacter;
    public CarColorSO DefaultCarColor => defaultCarColor;
    public CarModelSO DefaultCarModel => defaultCarModel;
    public MapSO DefaultMap => defaultMap;

    private void Awake()
    {
        List<CharacterModelSO> charactersSO = SOLoader.LoadSOByType<CharacterModelSO>();
        List<CarColorSO> carColorsSO = SOLoader.LoadSOByType<CarColorSO>();
        List<CarModelSO> carModelsSO = SOLoader.LoadSOByType<CarModelSO>();

        characters.AddRange(charactersSO);
        carColors.AddRange(carColorsSO);
        carModels.AddRange(carModelsSO);
    }
    public void LoadPlayerItems()
    {


        currentCharacter = characters.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCharacterItem);



        currentCarColor = carColors.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarColorItem);



        currentCarModel = carModels.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarModelItem);

    }

    private void OnDestroy()
    {
        currentCharacter = null;
        currentCarColor = null;
        currentCarModel = null;

        defaultCharacter = null;
        defaultCarColor = null;
        defaultCarModel = null;

        characters = null;
        carColors = null;
        carModels = null;
    }
}
