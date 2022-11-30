using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AbilityShooting", menuName = "ScriptableObject/Ability/AbilityShooting")]
public class AbilityShootingSO : AbilitySO
{
    [SerializeField] private GameObject projectile;

    public override void Use(NewCarController car)
    {
        Use(car, null);
    }

    public override void Use(NewCarController car, NewCarController target)
    {
        Instantiate(projectile, car.transform.position, Quaternion.identity);
    }
}
