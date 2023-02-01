using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class BotInitializer : MonoBehaviour
{
    [SerializeField] private CarModelSO carModel;
    [SerializeField] private GameObject carObj;
    [SerializeField] private GameObject characterModel;
    [SerializeField] private MeshRenderer carRenderer;
    [SerializeField] private MeshFilter carFilter;

    public void InitializeBot(CharacterModelSO character, CarColorSO carColor)
    {
        Instantiate(character.Prefab, characterModel.transform.parent);
        Destroy(characterModel.gameObject);

        carObj.GetComponent<CarController>().m_FullTorqueOverAllWheels = carModel.Acceleration;
        carRenderer.material.mainTexture = carColor.Texture;
        carFilter.mesh = carModel.Mesh;
    }
}
