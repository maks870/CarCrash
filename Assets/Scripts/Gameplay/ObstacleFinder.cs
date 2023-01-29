using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class ObstacleFinder : MonoBehaviour
{
    [SerializeField] float carToAvoidMinSpeed = 5f;
    [SerializeField] CarAIControl carAIControl;
    [SerializeField] AbilityController abilityController;
    [SerializeField] List<CarController> carControllers = new List<CarController>();



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ProjectileMine>() != null)
        {
            other.GetComponent<ProjectileMine>().warningCars.Add(abilityController);
            abilityController.IsMineWarning = true;
            carAIControl.AvoidMineAction(other.gameObject);//œ≈–≈»Ã≈ÕÕŒ¬¿“‹
        }
        if (other.GetComponent<CarController>() != null)
        {
            carControllers.Add(other.GetComponent<CarController>());
        }
        else if (other.GetComponentInChildren<CarController>() != null)
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
        else if (other.GetComponentInChildren<CarController>() != null)
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
        foreach (CarController car in carControllers)//Õ≈  ŒÕ“–ŒÀÀ≈– ¿ Àﬁ¡Œ≈ œ–≈œﬂ“—“¬»ﬂ
        {
            if (car != null && car.CurrentSpeed < carToAvoidMinSpeed)
            {
                carAIControl.AvoidMineAction(car.gameObject);
                carControllers.Remove(car);
                break;
            }
        }
    }

}
