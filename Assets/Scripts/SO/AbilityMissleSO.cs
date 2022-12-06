using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[CreateAssetMenu(fileName = "AbilityMissle", menuName = "ScriptableObject/Ability/AbilityMissle")]
public class AbilityMissleSO : AbilitySO
{
    public override AbilityType Type => AbilityType.Missle;

    public override void Use(Vector3 spawnPoint, AbilityController target)
    {
        GameObject rocket = Instantiate(projectile, spawnPoint, Quaternion.identity);
        if (target != null)
            rocket.GetComponent<Projectile>().Target = target.gameObject;
        else
            rocket.GetComponent<Projectile>().Target = null;
    }
}
