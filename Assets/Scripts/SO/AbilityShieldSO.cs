using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AbilityShield", menuName = "ScriptableObject/Ability/AbilityShield")]
public class AbilityShieldSO : AbilitySO
{
    [SerializeField] private GameObject projectile;

    public override void Use(NewCarController car)
    {
        Use(car, null);
    }

    public override void Use(NewCarController car, NewCarController target)
    {
        GameObject rocket = Instantiate(projectile, car.transform.position, Quaternion.identity);
        rocket.GetComponent<Projectile>().Target = target.gameObject;
    }
}
