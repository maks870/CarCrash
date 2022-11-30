using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[CreateAssetMenu(fileName = "AbilityShield", menuName = "ScriptableObject/Ability/AbilityShield")]
public class AbilityShieldSO : AbilitySO
{
    [SerializeField] private GameObject projectile;

    public override void Use(CarController car)
    {
        Use(car, null);
    }

    public override void Use(CarController car, CarController target)
    {
        Instantiate(projectile, car.transform.position, Quaternion.identity);
    }
}
