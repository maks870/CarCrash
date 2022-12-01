using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[CreateAssetMenu(fileName = "AbilityMine", menuName = "ScriptableObject/Ability/AbilityMine")]
public class AbilityMineSO : AbilitySO
{
    public override AbilityType Type => AbilityType.Mine;

    public override void Use(Vector3 spawnPoint, AbilityController target)
    {
        GameObject rocket = Instantiate(projectile, spawnPoint, Quaternion.identity);
    }
}
