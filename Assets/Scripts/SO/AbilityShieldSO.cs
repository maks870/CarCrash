using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[CreateAssetMenu(fileName = "AbilityShield", menuName = "ScriptableObject/Ability/AbilityShield")]
public class AbilityShieldSO : AbilitySO
{
    public override SpawnPlace SpawnPlace => SpawnPlace.middle;

    public override void Use(Vector3 spawnPoint, CarController target)
    {
        Instantiate(projectile, target.transform.position, Quaternion.identity);
    }
}
