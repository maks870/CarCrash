using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using YG;

public class PlayerLoad : MonoBehaviour
{
    [SerializeField] private AssetReference defaultCharacter;
    [SerializeField] private AssetReference defaultCarColor;
    [SerializeField] private AssetReference defaultCarModel;
    [SerializeField] private AssetReference defaultMap;
    private CharacterModelSO currentCharacter;
    private CarColorSO currentCarColor;
    private CarModelSO currentCarModel;


    private List<CharacterModelSO> characters = new List<CharacterModelSO>();
    private List<CarColorSO> carColors = new List<CarColorSO>();
    private List<CarModelSO> carModels = new List<CarModelSO>();

    public CharacterModelSO CurrentCharacter => currentCharacter;
    public CarColorSO CurrentCarColor => currentCarColor;
    public CarModelSO CurrentCarModel => currentCarModel;
    public AssetReference DefaultCharacter => defaultCharacter;
    public AssetReference DefaultCarColor => defaultCarColor;
    public AssetReference DefaultCarModel => defaultCarModel;

    public void SOLoaderSubscribe()
    {
        SOLoader.instance.EndLoadingEvent += () =>
        {
            characters.AddRange(SOLoader.instance.GetSOList<CharacterModelSO>());
            carColors.AddRange(SOLoader.instance.GetSOList<CarColorSO>());
            carModels.AddRange(SOLoader.instance.GetSOList<CarModelSO>());
        };
    }

    public void LoadPlayerItems()
    {
        if (characters.Count != 0)
            currentCharacter = characters.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCharacterItem);

        if (carColors.Count != 0)
            currentCarColor = carColors.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarColorItem);

        if (carModels.Count != 0)
            currentCarModel = carModels.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarModelItem);

        Debug.Log(YandexGame.savesData.playerWrapper.currentCharacterItem);
        Debug.Log(characters.Count);
        Debug.Log(characters[0].Name);
        Debug.Log(currentCharacter.Name);
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
