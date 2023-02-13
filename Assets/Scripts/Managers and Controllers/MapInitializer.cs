using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using YG;

public class MapInitializer : MonoBehaviour
{
    [SerializeField] private GameObject carObj;
    [SerializeField] private GameObject characterModel;
    [SerializeField] private MeshRenderer carRenderer;
    [SerializeField] private MeshFilter carFilter;
    [SerializeField] private List<CarColorSO> carColors = new List<CarColorSO>();
    [SerializeField] private List<CharacterModelSO> characterModels = new List<CharacterModelSO>();
    [SerializeField] private SoundController soundController;

    private CharacterModelSO character;
    private CarColorSO carColor;
    private CarModelSO carModel;

    public List<BotSettings> botInitializers = new List<BotSettings>();

    void Start()
    {
        botInitializers.AddRange(FindObjectsOfType<BotSettings>());
        InitializePrefabs();
    }

    private void InitializePlayerPrefab()
    {
        character = SOLoader.LoadSO<CharacterModelSO>(YandexGame.savesData.playerWrapper.currentCharacterItem);
        carColor = SOLoader.LoadSO<CarColorSO>(YandexGame.savesData.playerWrapper.currentCarColorItem);
        carModel = SOLoader.LoadSO<CarModelSO>(YandexGame.savesData.playerWrapper.currentCarModelItem);

        SOLoader.LoadAndCreate(character.AssetReference, characterModel.transform.parent);
        Destroy(characterModel.gameObject);

        carObj.GetComponent<CarController>().m_FullTorqueOverAllWheels = carModel.Acceleration;
        carObj.GetComponent<CarController>().Handability = carModel.Handleability;
        carRenderer.material.mainTexture = carColor.Texture;
        carFilter.mesh = SOLoader.LoadComponent<Mesh>(carModel.MeshAsset);
        soundController.Initialize();
    }

    private void InitializePrefabs()
    {
        InitializePlayerPrefab();

        foreach (BotSettings bot in botInitializers)
        {
            CharacterModelSO characterBot = GetRandomItem(characterModels, character);
            CarColorSO carColorBot = GetRandomItem(carColors, carColor);
            bot.InitializeBot(characterBot, carColorBot);
        }
    }

    private T GetRandomItem<T>(List<T> items, T existingItem)
    {
        T result = default(T);

        do
        {
            result = items[Random.Range(0, items.Count)];
        }
        while (ReferenceEquals(result, existingItem));

        return result;
    }


}
