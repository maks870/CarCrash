using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[CreateAssetMenu(fileName = "AbilityShooting", menuName = "ScriptableObject/Ability/AbilityShooting")]
public class AbilityShootingSO : AbilitySO
{
    public override SpawnPlace SpawnPlace => SpawnPlace.front;

    public override void Use(Vector3 spawnPoint, CarController target)
    {
        GameObject rocket = Instantiate(projectile, spawnPoint, Quaternion.identity);
        rocket.GetComponent<Projectile>().Target = target.gameObject;
    }
}
