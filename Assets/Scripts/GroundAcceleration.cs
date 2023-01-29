using System.Collections;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class GroundAcceleration : MonoBehaviour
{
    [SerializeField] private float acelerationTime;
    private void OnTriggerEnter(Collider collider)
    {
        CarController carController = collider.GetComponent<CarController>();

        if (carController != null)
            StartCoroutine(AcelerationTimer(carController));

    }

    IEnumerator AcelerationTimer(CarController carController)
    {
        carController.IsAccelerated = true;
        yield return new WaitForSeconds(acelerationTime);
        carController.IsAccelerated = false;
    }

}
