using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class ObstacleFinder : MonoBehaviour
{
    [SerializeField] float avoidMineDistance = 5f;
    [SerializeField] CarAIControl carAIControl;
    [SerializeField] AbilityController abilityController;
    [SerializeField] List<CarController> carControllers = new List<CarController>();



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ProjectileMine>() != null)
        {
            other.GetComponent<ProjectileMine>().warningCars.Add(abilityController);
            abilityController.IsMineWarning = true;
            carAIControl.AvoidMineAction(other.gameObject);
        }
        if (other.GetComponent<CarController>() != null)
        {
            carControllers.Add(other.GetComponent<CarController>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CarController>() != null)
        {
            carControllers.Remove(other.GetComponent<CarController>());
        }
        if (other.GetComponent<ProjectileMine>() != null)
        {
            other.GetComponent<ProjectileMine>().warningCars.Remove(abilityController);
            abilityController.IsMineWarning = false;
        }
    }

    private void Update()
    {
        foreach (CarController car in carControllers)
        {
            if (car != null && car.CurrentSpeed < 2)
            {
                carAIControl.AvoidMineAction(car.gameObject);
                carControllers.Remove(car);
                break;
            }
        }
    }

}
