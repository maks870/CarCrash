using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMissle : Projectile
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float angleSpeedChange;
    [SerializeField] private float stopSpeedMultiplier;


    WheelCollider wheel;
    private Vector3 rotationDir = Vector3.zero;
    protected override void Start()
    {
        base.Start();
        wheel = GetComponentInChildren<WheelCollider>();
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
        //float currentSpeed = speed;

        //Vector3 dir = target.transform.position - transform.position;

        //float rotationAngle = Vector3.Angle(rb.velocity, transform.forward);

        //if (rotationAngle > angleSpeedChange)
        //    rb.AddForce(-rb.velocity.normalized * currentSpeed * stopSpeedMultiplier * Time.deltaTime, ForceMode.Force);

        //Debug.Log(rotationAngle);

        //if ()
        //{

        //}
        //rb.AddForce(transform.forward * currentSpeed * Time.deltaTime, ForceMode.Force);
        //transform.LookAt(target.transform.position);

        Vector3 dir = target.transform.position - transform.position;

        float rotationAngle = Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(dir.x, dir.z));
        wheel.motorTorque = speed;
        wheel.steerAngle = rotationAngle;
    }
}
