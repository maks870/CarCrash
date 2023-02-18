using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityStandardAssets.Vehicles.Car;

enum CarModel
{
    Car1 = 1,
    Car2 = 2,
    Car3 = 3,
    Car4 = 4,

}

[RequireComponent(typeof(AbilityAIInput))]
[RequireComponent(typeof(CarController))]
public class BotSettings : MonoBehaviour
{
    [SerializeField] private CarModel carModelSOName;
    [SerializeField] private GameObject characterModel;
    [SerializeField] private MeshRenderer carRenderer;
    [SerializeField] private MeshFilter carFilter;
    [SerializeField] [Range(0, 100)] private int complexity;
    [SerializeField] [Range(200, 350)] private float accelerationSpeed;


    private AbilityAIInput abilityAIInput;
    private CarController carController;

    private void Awake()
    {
        abilityAIInput = GetComponent<AbilityAIInput>();
        carController = GetComponent<CarController>();
        abilityAIInput.Complexity = complexity;
        carController.m_FullTorqueOverAllWheels = accelerationSpeed;
    }

    public void InitializeBot(CharacterModelSO character, CarColorSO carColor)
    {
        //SOLoader.instance.LoadAssetReference<GameObject>(character.Prefab, (result) =>
        //{
        //    Instantiate(result, characterModel.transform.parent);
        //    Destroy(characterModel.gameObject);
        //});

        Instantiate(character.Prefab, characterModel.transform.parent);
        Destroy(characterModel.gameObject);

        CarModelSO carModel = SOLoader.instance.GetSOList<CarModelSO>().Find((item) => item.Name == carModelSOName.ToString());
        Debug.Log(carModel.Name);
        carFilter.mesh = carModel.Prefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        //SOLoader.instance.LoadAssetReference<Mesh>(carPrefab, (result) => carFilter.mesh = result);
        carRenderer.material.mainTexture = carColor.Texture;
    }
}
