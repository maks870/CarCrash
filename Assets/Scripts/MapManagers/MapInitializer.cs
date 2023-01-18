using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MapInitializer : MonoBehaviour
{
    [SerializeField] private GameObject characterObj;
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

        Instantiate(character.Prefab, characterObj.transform.parent);
        Destroy(characterObj.gameObject);

        //characterRenderer.materials = character.Prefab.GetComponent<MeshRenderer>().materials;
        //characterFilter.mesh = character.Prefab.GetComponent<MeshFilter>().mesh;

        carRenderer.material.mainTexture = carColor.Texture;
        carFilter.mesh = carModel.Mesh;
        soundController.Initialize();
    }
}
