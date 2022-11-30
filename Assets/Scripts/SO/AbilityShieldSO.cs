using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AbilityShield", menuName = "ScriptableObject/Ability/AbilityShield")]
public class AbilityShieldSO : AbilitySO
{
    [SerializeField] private GameObject projectile;

    public override void Use(Car car)
    {
        Use(car, null);
    }

    public override void Use(Car car, Car target)
    {
        Instantiate(projectile, car.transform.position, Quaternion.identity);
    }
}
