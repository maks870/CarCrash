using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[CreateAssetMenu(fileName = "AbilityMissle", menuName = "ScriptableObject/Ability/AbilityMissle")]
public class AbilityMissleSO : AbilitySO
{
    public override AbilityType Type => AbilityType.Missle;

    public override void Use(Transform spawnPoint, AbilityController target)
    {
        GameObject rocket = Instantiate(projectile, spawnPoint);
        rocket.transform.parent = null;
        rocket.GetComponent<Projectile>().Target = target != null ? target.gameObject : null;

        if (target != null)
            target.IsMissleWarning = true;
    }
}
