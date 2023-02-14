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
    [SerializeField] private SoundController soundController;

    private CharacterModelSO character;
    private CarColorSO carColor;
    private CarModelSO carModel;

    private List<CarColorSO> carColors = new List<CarColorSO>();
    private List<CharacterModelSO> characterModels = new List<CharacterModelSO>();

    public List<BotSettings> botInitializers = new List<BotSettings>();

    void Start()
    {
        botInitializers.AddRange(FindObjectsOfType<BotSettings>());
        SOLoaderInitialize();
    }

    private void InitializePlayerPrefab()
    {
        SOLoader.LoadSO<CharacterModelSO>(YandexGame.savesData.playerWrapper.currentCharacterItem, (result) =>
        {
            character = result;
            SOLoader.LoadAsset<GameObject>(character.AssetReference, (result) =>
            {
                Debug.Log(result);
                Debug.Log(characterModel.transform.parent);
                Instantiate(result, characterModel.transform.parent);
                Destroy(characterModel.gameObject);
            });
        });

        SOLoader.LoadSO<CarColorSO>(YandexGame.savesData.playerWrapper.currentCarColorItem, (result) =>
        {
            carColor = result;
            carRenderer.material.mainTexture = carColor.Texture;
        });

        SOLoader.LoadSO<CarModelSO>(YandexGame.savesData.playerWrapper.currentCarModelItem, (result) =>
        {
            carModel = result;
            carObj.GetComponent<CarController>().m_FullTorqueOverAllWheels = carModel.Acceleration;
            carObj.GetComponent<CarController>().Handability = carModel.Handleability;
            SOLoader.LoadAsset<Mesh>(carModel.MeshAsset, (mesh) => carFilter.mesh = mesh);
        });

        soundController.Initialize();
    }

    private void SOLoaderInitialize()
    {
        SOLoader.LoadAllSO<CharacterModelSO>((result) =>
        {
            characterModels = result;
            SOLoader.LoadAllSO<CarColorSO>((result) =>
            {
                carColors = result;
                InitializePrefabs();
            });
        });
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
        bool gotItem = false;

        while (!gotItem)
        {
            result = items[Random.Range(0, items.Count)];

            if (ReferenceEquals(result, existingItem))
                gotItem = true;
        }

        return result;
    }


}
