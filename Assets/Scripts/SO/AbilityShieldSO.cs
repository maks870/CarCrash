using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[CreateAssetMenu(fileName = "AbilityShield", menuName = "ScriptableObject/Ability/AbilityShield")]
public class AbilityShieldSO : AbilitySO
{
    public override AbilityType Type => AbilityType.Shield;

    public override void Use(Transform spawnPoint, AbilityController target)
    {
        target.ActivateShield();
    }
}
