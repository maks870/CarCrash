using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRocket : Projectile
{
    [SerializeField] protected float speed;
    protected override void Start()
    {
        base.Start();
        rb.velocity = (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
        Launch();
    }
    private void FixedUpdate()
    {
        Fly();
    }
    private void Launch()
    {

    }
    private void Fly()
    {
        Vector3 dir;
        if (target != null)
            dir = (target.transform.position - transform.position);
        else
            dir = transform.forward;

        rb.AddForce(dir * speed * Time.deltaTime, ForceMode.Force);
        transform.LookAt(target.transform.position);
    }
}
