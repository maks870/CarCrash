using UnityEngine;

public enum Quality
{
    rare,
    epic,
    legendary

}
 
public abstract class CollectibleSO : ScriptableObject
{
    [SerializeField] private Quality quality;

    public virtual string Name { get; }
    public Quality Quality => quality;
}
