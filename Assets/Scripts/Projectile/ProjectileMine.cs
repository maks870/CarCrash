using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class ProjectileMine : Projectile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb.useGravity = true;
    }
}
