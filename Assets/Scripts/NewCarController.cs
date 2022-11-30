using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class NewCarController : CarController
{
    [SerializeField] GameObject target;

    public void AddAbility(AbilitySO ability)
    {
        ability.Use(this, target.GetComponent<NewCarController>());
    }
}
