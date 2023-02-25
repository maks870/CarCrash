using UnityEngine;
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
    [SerializeField][Range(0, 100)] private int complexity;
    [SerializeField][Range(200, 900)] private float accelerationSpeed;
    private float maxSpeed;


    private AbilityAIInput abilityAIInput;
    private CarController carController;

    private void Awake()
    {
        abilityAIInput = GetComponent<AbilityAIInput>();
        carController = GetComponent<CarController>();
        abilityAIInput.Complexity = complexity;
        carController.m_FullTorqueOverAllWheels = accelerationSpeed;
        maxSpeed = carController.MaxSpeed;
    }

    public void InitializeBot(CharacterModelSO character, CarColorSO carColor)
    {

        Instantiate(character.Prefab, characterModel.transform.parent);
        Destroy(characterModel.gameObject);

        CarModelSO carModel = SOLoader.instance.GetSOList<CarModelSO>().Find((item) => item.Name == carModelSOName.ToString());
        carFilter.mesh = carModel.Prefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        carRenderer.material.mainTexture = carColor.Texture;
    }

    private void Update()
    {
        if (ChkManager.posBot(gameObject) < ChkManager.posMax)
        {
            carController.MaxSpeed = maxSpeed - maxSpeed * 0.1f;
        }

        if (ChkManager.posBot(gameObject) > ChkManager.posMax)
        {
            carController.MaxSpeed = maxSpeed + maxSpeed * 0.2f;
        }
    }
}
