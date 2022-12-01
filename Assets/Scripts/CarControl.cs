using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public abstract class CarControl : MonoBehaviour
{
    [SerializeField] protected AbilityController abilityController;
    [SerializeField] protected CarController carController;
    protected void ControlMove(float steering, float accel, float footbrake, float handbrake)
    {
        if (abilityController.IsDamaged)
            handbrake = 1;

        carController.Move(steering, accel, footbrake, handbrake);
    }
}
