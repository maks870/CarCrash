using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMissle : Projectile
{
    [SerializeField] private float speed;
    [SerializeField] private float angleSpeedChange;
    [SerializeField] private float stopSpeedMultiplier;
    private Vector3 rotationDir = Vector3.zero;
    protected override void Start()
    {
        base.Start();
        //rb.velocity = (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
        //startVelocity = rb.velocity.magnitude;
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
        float currentSpeed = speed;

        Vector3 dir = target.transform.position - transform.position;

        float rotationAngle = Vector3.Angle(transform.forward, dir);

        if (rotationAngle > angleSpeedChange)
            rb.AddForce(-rb.velocity.normalized * currentSpeed * stopSpeedMultiplier * Time.deltaTime, ForceMode.Force);

        Debug.Log(rotationAngle);

        rb.AddForce(transform.forward * currentSpeed * Time.deltaTime, ForceMode.Force);
        transform.LookAt(target.transform.position);
    }
}
