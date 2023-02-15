using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(AbilityAIInput))]
[RequireComponent(typeof(CarController))]
public class BotSettings : MonoBehaviour
{
    [SerializeField] private AssetReference meshAsset;
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
        SOLoader.instance.LoadAssetReference<GameObject>(character.AssetReference, (result) =>
        {
            Instantiate(result, characterModel.transform.parent);
            Destroy(characterModel.gameObject);
        });

        SOLoader.instance.LoadAssetReference<Mesh>(meshAsset, (result) => carFilter.mesh = result);
        carRenderer.material.mainTexture = carColor.Texture;
    }
}
