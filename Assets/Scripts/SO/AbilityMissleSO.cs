using UnityEngine;

[CreateAssetMenu(fileName = "AbilityMissle", menuName = "ScriptableObject/Ability/AbilityMissle")]
public class AbilityMissleSO : AbilitySO
{
    public override AbilityType Type => AbilityType.Missle;

    public override void Use(Transform spawnPoint, AbilityController target)
    {
        GameObject missle = Instantiate(projectile, spawnPoint);

        missle.transform.parent = null;
        ProjectileMissle projectileMissle = missle.GetComponent<ProjectileMissle>();
        projectileMissle.Launcher = spawnPoint.parent.GetComponent<AbilityController>();
        projectileMissle.Target = target != null ? target.gameObject : null;

        if (target != null)
            target.IsMissleWarning = true;
    }
}
