using System.Collections;
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

    void Start()
    {
        InitializePlayerPrefab();
    }

    private void InitializePlayerPrefab()
    {
        CharacterModelSO character = SOLoader.LoadCollectibleByName<CharacterModelSO>(YandexGame.savesData.playerWrapper.currentCharacterItem);
        CarColorSO carColor = SOLoader.LoadCollectibleByName<CarColorSO>(YandexGame.savesData.playerWrapper.currentCarColorItem);
        CarModelSO carModel = SOLoader.LoadCollectibleByName<CarModelSO>(YandexGame.savesData.playerWrapper.currentCarModelItem);

        Instantiate(character.Prefab, characterModel.transform.parent);
        Destroy(characterModel.gameObject);

        carObj.GetComponent<CarController>().m_FullTorqueOverAllWheels = carModel.Acceleration;
        carObj.GetComponent<CarUserControl>().TurnSteerAngle = carModel.Handleability;
        carRenderer.material.mainTexture = carColor.Texture;
        carFilter.mesh = carModel.Mesh;
        soundController.Initialize();
    }
}
