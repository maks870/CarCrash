using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : CarControl
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private GameObject targetMark;
        [SerializeField] private UIPlayerManager uiManager;
        [SerializeField] [Range(0, 1)] private float taxiingHelper;
        [SerializeField] private float m_SteerSensitivity = 0.05f;
        [SerializeField] PlayerCarTracker taxiingCarTracker;
        [SerializeField] private float targetMarkHeight = 1f;
        private float taxiingSteer;
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
            CalculateTaxiingSteer();
            float currentSteer = Mathf.Lerp(input.HorizontalAxis, taxiingSteer, taxiingHelper);
            ControlMove(currentSteer, input.VerticalAxis, input.VerticalAxis, input.HandBrake);
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

        private void CalculateTaxiingSteer()
        {
            Vector3 localTarget = transform.InverseTransformPoint(taxiingCarTracker.transform.position);
            float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
            float steer = Mathf.Clamp(targetAngle * m_SteerSensitivity, -1, 1) * Mathf.Sign(carController.CurrentSpeed);

            taxiingSteer = steer;
        }
    }
}
