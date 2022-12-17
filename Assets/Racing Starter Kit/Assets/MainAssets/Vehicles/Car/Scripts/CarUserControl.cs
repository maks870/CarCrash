using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : CarControl
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private GameObject targetMark;
        [SerializeField] private UIPlayerManager uiManager;
        [SerializeField] private float targetMarkHeight = 1f;
        private BaseInput input;


        private void Awake()
        {
            targetMark.SetActive(false);
            carController = GetComponent<CarController>();
            uiManager = GameObject.Find("UIPlayerManager").GetComponent<UIPlayerManager>();
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
            ReportMissle();
        }

        private void ReportMissle()
        {
            if (abilityController.IsMissleWarning)
            {
                uiManager.WarningActivate();
            }
            else
                uiManager.WarningDeactivate();
        }

        private void SetTargetMark()
        {
            if (abilityController.Target != null)
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
