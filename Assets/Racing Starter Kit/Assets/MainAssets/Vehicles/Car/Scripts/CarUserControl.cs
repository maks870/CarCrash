using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : CarControl
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private UIPlayerManager uiManager;
        [SerializeField] private bool simpleSteerHelper = false;
        [SerializeField] private bool hardSteerHelper = false;
        [SerializeField] [Range(0, 1)] private float taxiingHelper;
        [SerializeField] private float m_SteerSensitivity = 0.05f;
        [SerializeField] private float targetMarkHeight = 1f;

        [SerializeField] private float normalSteerAngle = 15f;
        [SerializeField] private float turnSteerAngle = 25f;
        [SerializeField] private float normalFOV = 80f;
        [SerializeField] private float acceleratedFOV = 120f;
        [SerializeField] private float changeFOVStep;
        private bool isStopped = false;
        private float taxiingSteer;
        private BaseInput input;
        private TargetPointer targetPointer;
        private Camera cam;

        public bool IsStopped { set => isStopped = value; }
        public float TurnSteerAngle { get => turnSteerAngle; set => turnSteerAngle = value; }

        private void Awake()
        {
            targetPointer = GetComponent<TargetPointer>();
            carController = GetComponent<CarController>();
            uiManager = GameObject.Find("UIPlayerManager")?.GetComponent<UIPlayerManager>();
        }

        private void Start()
        {
            cam = Camera.main;
            input = inputManager.CurrentInput;
            abilityController.RefreshAbilityEvent += input.SetAbilities;
            input.PressButtonAbilityEvent += abilityController.UseAbility;
        }

        private void FixedUpdate()
        {
            if (hardSteerHelper)// 2È ‚‡Ë‡ÌÚ œŒÃŒÿÕ» ¿ œŒ¬Œ–Œ“¿
            {
                float currentSteer = input.HorizontalAxis;

                if (input.HorizontalAxis != 0 && abilityController.IsMineWarning == false)
                {
                    CalculateTaxiingSteer();
                    currentSteer = Mathf.Lerp(input.HorizontalAxis, taxiingSteer, taxiingHelper);
                }

                ControlMove(currentSteer, input.VerticalAxis, input.VerticalAxis, input.HandBrake);

            }
            else // Œ—ÕŒ¬Õ¿ﬂ ÀŒ√» ¿ ≈«ƒ€
            {
                if (simpleSteerHelper)
                    SteerChanger();

                if (!isStopped)
                    ControlMove(input.HorizontalAxis, input.VerticalAxis, input.VerticalAxis, input.HandBrake);
                else
                {
                    carController.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    ControlMove(0, 0, -1, 1);
                }
            }
        }

        protected override void Update()
        {
            base.Update();
            SetTargetMark();
            ReportMissle();
            SetCameraFOV();
        }

        private void SetCameraFOV()
        {
            if (carController.IsAccelerated)
            {
                if (cam.fieldOfView < acceleratedFOV)
                    cam.fieldOfView += changeFOVStep;

            }
            else
            {
                if (cam.fieldOfView > normalFOV)
                    cam.fieldOfView -= changeFOVStep;
            }
        }

        private void ReportMissle()
        {
            if (uiManager == null)
                return;

            if (abilityController.IsMissleWarning)
            {
                uiManager.WarningActivate();
            }
            else
                uiManager.WarningDeactivate();
        }

        private void SetTargetMark()
        {
            bool hasMissle = false;

            if (abilityController.Abilities.Count != 0 && abilityController.Abilities[0].Type == AbilityType.Missle)
                hasMissle = true;

            if (abilityController.Target != null && hasMissle)
                targetPointer.Target = abilityController.Target.transform;
            else
                targetPointer.Target = null;
        }

        private void CalculateTaxiingSteer()
        {
            Vector3 localTarget = transform.InverseTransformPoint(carTrack.transform.position);
            float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
            float steer = Mathf.Clamp(targetAngle * m_SteerSensitivity, -1, 1) * Mathf.Sign(carController.CurrentSpeed);

            taxiingSteer = steer;
        }

        private void SteerChanger()
        {
            Vector3 localTarget = transform.InverseTransformPoint(carTrack.transform.position);
            float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
            float steer = Mathf.Clamp(targetAngle * m_SteerSensitivity, -1, 1) * Mathf.Sign(carController.CurrentSpeed);

            if (steer > 0 && input.HorizontalAxis > 0)
            {
                carController.m_MaximumSteerAngle = turnSteerAngle;
            }
            else if (steer < 0 && input.HorizontalAxis < 0)
            {
                carController.m_MaximumSteerAngle = turnSteerAngle;
            }
            else
            {
                carController.m_MaximumSteerAngle = normalSteerAngle;
            }
        }
    }
}
