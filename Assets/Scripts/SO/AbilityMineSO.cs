using UnityEngine;

[CreateAssetMenu(fileName = "AbilityMine", menuName = "ScriptableObject/Ability/AbilityMine")]
public class AbilityMineSO : AbilitySO
{
    public override AbilityType Type => AbilityType.Mine;

    public override void Use(Transform spawnPoint, AbilityController target)
    {
        GameObject mine = Instantiate(projectile, spawnPoint);
        mine.transform.parent = null;
    }
}
