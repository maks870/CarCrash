using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public abstract class AbilitySO : ScriptableObject
{
    public abstract void Use(Vector3 spawnPoint, CarController target);
}


