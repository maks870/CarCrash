using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[CreateAssetMenu(fileName = "AbilityShooting", menuName = "ScriptableObject/Ability/AbilityShooting")]
public class AbilityShootingSO : AbilitySO
{
    [SerializeField] private GameObject projectile;

    public override void Use(CarController car)
    {
        Use(car, null);
    }

    public override void Use(CarController car, CarController target)
    {

        GameObject rocket = Instantiate(projectile, car.transform.position, Quaternion.identity);
        rocket.GetComponent<Projectile>().Target = target.gameObject;
    }
}
