using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MapInitializer : MonoBehaviour
{
    [SerializeField] private GameObject characterObj;
    [SerializeField] private MeshRenderer carRenderer;
    [SerializeField] private MeshFilter carFilter;

    void Start()
    {
        InitializePlayerPrefab();
    }

    private void InitializePlayerPrefab()
    {
        CharacterModelSO character = (CharacterModelSO)SOLoader.LoadCollectibleByName(YandexGame.savesData.playerWrapper.currentCharacterItem);
        CarColorSO carColor = (CarColorSO)SOLoader.LoadCollectibleByName(YandexGame.savesData.playerWrapper.currentCarColorItem);
        CarModelSO carModel = (CarModelSO)SOLoader.LoadCollectibleByName(YandexGame.savesData.playerWrapper.currentCarModelItem);

        Instantiate(character.Prefab, characterObj.transform.parent);
        Destroy(characterObj.gameObject);

        //characterRenderer.materials = character.Prefab.GetComponent<MeshRenderer>().materials;
        //characterFilter.mesh = character.Prefab.GetComponent<MeshFilter>().mesh;

        carRenderer.material.mainTexture = carColor.Texture;
        carFilter.mesh = carModel.Mesh;
    }
}
