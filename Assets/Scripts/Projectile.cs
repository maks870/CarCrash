using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;
    private Animator animator;
    [SerializeField] private GameObject target;

    public GameObject Target { set => target = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
        animator = GetComponent<Animator>();
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
        //rb.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Force);
        rb.AddForce(dir * speed * Time.deltaTime, ForceMode.Force);
        transform.LookAt(target.transform.position);
    }
}
