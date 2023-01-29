using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class AIMovingTargetDetect : MonoBehaviour
{
    [SerializeField] CarAIControl carAIControl;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbilityBox>() != null || other.GetComponent<GroundAcceleration>() != null)
            carAIControl.DetectAbility(other.transform);
    }
}
