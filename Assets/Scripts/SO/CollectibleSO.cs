using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
