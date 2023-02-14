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

    private void OnEnable()
    {
        SOLoader.OnLoadingEvent += (scriptableObj) =>
        {
            if (scriptableObj.GetType() == typeof(CharacterModelSO))
                characterModels.Add((CharacterModelSO)scriptableObj);

            if (scriptableObj.GetType() == typeof(CarColorSO))
                carColors.Add((CarColorSO)scriptableObj);
        };

        SOLoader.EndLoadingEvent += SOLoaderInitialize;
    }

    void Start()
    {
        botInitializers.AddRange(FindObjectsOfType<BotSettings>());
        SOLoader.LoadAll();
    }

    private void InitializePlayerPrefab()
    {
        List<CharacterModelSO> charList = (List<CharacterModelSO>)SOLoader.characterHandle.Result;
        CharacterModelSO characterSO = charList.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCharacterItem);
        SOLoader.LoadAssetReference<GameObject>(character.AssetReference, (result) =>
        {
            Instantiate(result, characterModel.transform.parent);
            Destroy(characterModel.gameObject);
        });

        List<CarColorSO> carColorList = (List<CarColorSO>)SOLoader.carColorHandle.Result;
        CarColorSO carColorSO = carColorList.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarColorItem);
        carRenderer.material.mainTexture = carColorSO.Texture;

        List<CarModelSO> carModelList = (List<CarModelSO>)SOLoader.carModelHandle.Result;
        CarModelSO carModelSO = carModelList.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarModelItem);
        carModel = carModelSO;
        carObj.GetComponent<CarController>().m_FullTorqueOverAllWheels = carModel.Acceleration;
        carObj.GetComponent<CarController>().Handability = carModel.Handleability;
        SOLoader.LoadAssetReference<Mesh>(carModel.MeshAsset, (mesh) => carFilter.mesh = mesh);


        //SOLoader.LoadSO<CharacterModelSO>(YandexGame.savesData.playerWrapper.currentCharacterItem, (result) =>
        //{
        //    character = result;
        //    SOLoader.LoadAssetReference<GameObject>(character.AssetReference, (result) =>
        //    {
        //        Instantiate(result, characterModel.transform.parent);
        //        Destroy(characterModel.gameObject);
        //    });
        //});

        //SOLoader.LoadSO<CarColorSO>(YandexGame.savesData.playerWrapper.currentCarColorItem, (result) =>
        //{
        //    carColor = result;
        //    carRenderer.material.mainTexture = carColor.Texture;
        //});

        //SOLoader.LoadSO<CarModelSO>(YandexGame.savesData.playerWrapper.currentCarModelItem, (result) =>
        //{
        //    carModel = result;
        //    carObj.GetComponent<CarController>().m_FullTorqueOverAllWheels = carModel.Acceleration;
        //    carObj.GetComponent<CarController>().Handability = carModel.Handleability;
        //    SOLoader.LoadAssetReference<Mesh>(carModel.MeshAsset, (mesh) => carFilter.mesh = mesh);
        //});

        soundController.Initialize();
    }

    private void SOLoaderInitialize()
    {
        characterModels = (List<CharacterModelSO>)SOLoader.characterHandle.Result;
        carColors = (List<CarColorSO>)SOLoader.carColorHandle.Result;
        InitializePrefabs();
        //SOLoader.LoadAllSO<CharacterModelSO>((result) =>
        //{
        //    characterModels = result;
        //    SOLoader.LoadAllSO<CarColorSO>((result) =>
        //    {
        //        carColors = result;
        //        InitializePrefabs();
        //    });
        //});
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
