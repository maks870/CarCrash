using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    protected Rigidbody rb;
    protected Animator animator;
    protected GameObject target;

    public GameObject Target { set => target = value; }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarController>() != null)
        {
            other.GetComponent<CarController>().TakeDamage();
        }
    }
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
}
