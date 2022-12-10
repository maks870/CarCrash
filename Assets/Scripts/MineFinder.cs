using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class MineFinder : MonoBehaviour
{
    [SerializeField] float avoidMineDistance = 5f;
    [SerializeField] CarAIControl carAIControl;
    [SerializeField] AbilityController abilityController;



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ProjectileMine>() != null)
        {
            abilityController.IsMineWarning = true;
            carAIControl.AvoidMineAction(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ProjectileMine>() != null)
        {
            abilityController.IsMineWarning = false;
        }
    }

    private void Start()
    {
        carAIControl.AvoidMineDistance = avoidMineDistance;
    }
}
