using System.Collections.Generic;
using UnityEngine;

public class ProjectileMine : Projectile
{
    [HideInInspector] public List<AbilityController> warningCars = new List<AbilityController>();

    protected override void Start()
    {
        base.Start();
        rb.useGravity = true;
    }

    protected override void Destruct()
    {
        for (int i = 0; i < warningCars.Count; i++)
        {
            Debug.Log("Нет минной тревоги");
            warningCars[i].IsMineWarning = false;
        }
        base.Destruct();
    }
}
