using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        private CarController m_Car; // the car controller we want to use
        private BaseInput input;


        private void Awake()
        {
            m_Car = GetComponent<CarController>();
        }

        private void Start()
        {
            input = inputManager.CurrentInput;
            m_Car.RefreshAbilityEvent += input.SetAbilities;
            input.PressButtonAbilityEvent += m_Car.UseAbility;
        }

        private void FixedUpdate()
        {
            float h = input.HorizontalAxis;
            float v = input.VerticalAxis;
            float x = input.HandBrake;
            m_Car.Move(h, v, v, x);
        }
    }
}
