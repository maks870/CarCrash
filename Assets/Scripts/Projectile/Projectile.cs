using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected GameObject effect;
    protected Rigidbody rb;
    protected Animator animator;
    protected GameObject target;

    public GameObject Target { set => target = value; }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbilityController>() != null)
        {
            other.GetComponent<AbilityController>().TakeDamage();
            Destruct();
        }
    }
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Destruct()
    {
       GameObject effectObject = Instantiate(effect, transform.position, Quaternion.identity);
        effectObject.transform.position = effectObject.transform.position + new Vector3(0, 0.5f, 0);
        Destroy(gameObject);
    }
}
