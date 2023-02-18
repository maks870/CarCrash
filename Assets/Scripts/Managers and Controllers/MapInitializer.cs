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
        //SOLoader.instance.OnLoadingEvent += (scriptableObj) =>
        //{
        //    if (scriptableObj.GetType() == typeof(CharacterModelSO))
        //        characterModels.Add((CharacterModelSO)scriptableObj);

        //    if (scriptableObj.GetType() == typeof(CarColorSO))
        //        carColors.Add((CarColorSO)scriptableObj);
        //};

        //SOLoader.instance.EndLoadingEvent += SOLoaderInitialize;
    }

    void Start()
    {
        botInitializers.AddRange(FindObjectsOfType<BotSettings>());
        SOLoaderInitialize();
    }

    private void InitializePlayerPrefab()
    {
        character = characterModels.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCharacterItem);
        //SOLoader.instance.LoadAssetReference<GameObject>(character.Prefab, (result) =>
        //{
        //    Instantiate(result, characterModel.transform.parent);
        //    Destroy(characterModel.gameObject);
        //});

        Instantiate(character.Prefab, characterModel.transform.parent);
        Destroy(characterModel.gameObject);
        carColor = carColors.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarColorItem);
        carRenderer.material.mainTexture = carColor.Texture;

        List<CarModelSO> carModelList = SOLoader.instance.GetSOList<CarModelSO>();
        carModel = carModelList.Find(item => item.Name == YandexGame.savesData.playerWrapper.currentCarModelItem);
        carObj.GetComponent<CarController>().m_FullTorqueOverAllWheels = carModel.Acceleration;
        carObj.GetComponent<CarController>().Handability = carModel.Handleability;
        carFilter.sharedMesh = carModel.Prefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        //SOLoader.instance.LoadAssetReference<Mesh>(carModel.Prefab, (mesh) => carFilter.mesh = mesh);


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
        characterModels = SOLoader.instance.GetSOList<CharacterModelSO>();
        carColors = SOLoader.instance.GetSOList<CarColorSO>();
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

        List<CharacterModelSO> existingCharacters = new List<CharacterModelSO> { character };
        List<CarColorSO> existingCarColors = new List<CarColorSO> { carColor };

        foreach (BotSettings bot in botInitializers)
        {
            CharacterModelSO characterBot = GetRandomItem(characterModels, existingCharacters);
            CarColorSO carColorBot = GetRandomItem(carColors, existingCarColors);
            bot.InitializeBot(characterBot, carColorBot);
        }
    }

    private T GetRandomItem<T>(List<T> items, List<T> existingItem) where T : CollectibleSO
    {
        T result = default(T);
        bool gotItem = false;

        while (!gotItem)
        {
            result = items[Random.Range(0, items.Count)];

            T checkResult = existingItem.Find((item) => item.Name == result.Name);

            if (checkResult == null)
                gotItem = true;
            //if (ReferenceEquals(result, existingItem))
            //    gotItem = true;
        }

        existingItem.Add(result);
        return result;
    }


}
