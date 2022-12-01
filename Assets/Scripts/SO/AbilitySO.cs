using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

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
    public abstract void Use(Vector3 spawnPoint, AbilityController target);
}


