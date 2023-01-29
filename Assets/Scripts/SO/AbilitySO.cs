using UnityEngine;

public enum AbilityType
{
    Missle = 0,
    Shield = 1,
    Mine = 2

}

public abstract class AbilitySO : ScriptableObject
{
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected Sprite icon;

    abstract public AbilityType Type { get; }
    public Sprite Icon => icon;
    public abstract void Use(Transform spawnPoint, AbilityController target);
}


