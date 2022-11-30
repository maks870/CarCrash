using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private GameObject target;

    public GameObject Target { set => target = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Launch();
    }
    private void Update()
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
            dir = (target.transform.position - transform.position).normalized;
        else
            dir = transform.forward;

        rb.AddForce(dir, ForceMode.Force);
    }
}
