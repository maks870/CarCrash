using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private GameObject target;

    public GameObject Target { set => target = value; }

    private void Start()
    {
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
