using UnityEngine;

[CreateAssetMenu(fileName = "AbilityShield", menuName = "ScriptableObject/Ability/AbilityShield")]
public class AbilityShieldSO : AbilitySO
{
    [SerializeField] private float shieldLifetime = 3f;
    public override AbilityType Type => AbilityType.Shield;

    public override void Use(Transform spawnPoint, AbilityController target)
    {
        target.Shield.Lifetime = shieldLifetime;
        target.ActivateShield();
    }
}
