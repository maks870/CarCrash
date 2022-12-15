using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : CarControl
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private GameObject targetMark;
        [SerializeField] private float targetMarkHeight = 1f;
        private BaseInput input;


        private void Awake()
        {
            targetMark.SetActive(false);
            carController = GetComponent<CarController>();
        }

        private void Start()
        {
            input = inputManager.CurrentInput;
            abilityController.RefreshAbilityEvent += input.SetAbilities;
            input.PressButtonAbilityEvent += abilityController.UseAbility;
        }

        private void FixedUpdate()
        {
            ControlMove(input.HorizontalAxis, input.VerticalAxis, input.VerticalAxis, input.HandBrake);
        }

        private void Update()
        {
            SetTargetMark();
        }

        private void SetTargetMark()
        {
            if (abilityController.Target != null && abilityController.HaveTargetWeapon)
            {
                targetMark.SetActive(true);
                targetMark.transform.position = abilityController.Target.transform.position + Vector3.up * targetMarkHeight;
            }
            else
            {
                targetMark.SetActive(false);
            }
        }
    }
}
