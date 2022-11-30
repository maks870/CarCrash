using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public enum SpawnPlace
{
    front = 1,
    middle = 0,
    back = -1

}

public abstract class AbilitySO : ScriptableObject
{
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected Sprite icon;

    abstract public SpawnPlace SpawnPlace { get; }
    public Sprite Icon => icon;
    public abstract void Use(Vector3 spawnPoint, CarController target);
}


