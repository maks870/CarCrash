using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using YG;

public class PlayerLoad : MonoBehaviour
{
    [SerializeField] private CharacterModelSO defaultCharacter;
    [SerializeField] private CarColorSO defaultCarColor;
    [SerializeField] private CarModelSO defaultCarModel;
    [SerializeField] private MapSO defaultMap;
    private CharacterModelSO currentCharacter;
    private CarColorSO currentCarColor;
    private CarModelSO currentCarModel;


    private List<CharacterModelSO> characters = new List<CharacterModelSO>();
    private List<CarColorSO> carColors = new List<CarColorSO>();
    private List<CarModelSO> carModels = new List<CarModelSO>();

    public CharacterModelSO CurrentCharacter => currentCharacter;
    public CarColorSO CurrentCarColor => currentCarColor;
    public CarModelSO CurrentCarModel => currentCarModel;
    public CharacterModelSO DefaultCharacter => defaultCharacter;
    public CarColorSO DefaultCarColor => defaultCarColor;
    public CarModelSO DefaultCarModel => defaultCarModel;
    public MapSO DefaultMap => defaultMap;

    private void Awake()
    {
        List<CharacterModelSO> charactersSO = SOLoader.LoadAllSO<CharacterModelSO>();
        List<CarColorSO> carColorsSO = SOLoader.LoadAllSO<CarColorSO>();
        List<CarModelSO> carModelsSO = SOLoader.LoadAllSO<CarModelSO>();

        characters.AddRange(charactersSO);
        carColors.AddRange(carColorsSO);
        carModels.AddRange(carModelsSO);
    }

    public void LoadPlayerItems()
    {

        if (characters.Count != 0)
            currentCharacter = characters.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCharacterItem);

        if (carColors.Count != 0)
            currentCarColor = carColors.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarColorItem);

        if (carModels.Count != 0)
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
