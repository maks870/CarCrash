using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class MineFinder : MonoBehaviour
{
    [SerializeField] float avoidMineDistance = 5f;
    [SerializeField] CarAIControl carAIControl;
    [SerializeField] AbilityController abilityController;
    [SerializeField] List<CarController> carControllers = new List<CarController>();



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ProjectileMine>() != null)
        {
            abilityController.IsMineWarning = true;
            carAIControl.AvoidMineAction(other.gameObject);
        }
        if (other.GetComponent<CarController>()!=null )
        {
            carControllers.Add(other.GetComponent<CarController>());
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

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CarController>() != null)
        {
            carControllers.Remove(other.GetComponent<CarController>());
        }
        if (other.GetComponent<ProjectileMine>() != null)
        {
            abilityController.IsMineWarning = false;
        }
    }
}
