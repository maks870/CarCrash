using System.Collections.Generic;
using UnityEngine;

public class ProjectileMine : Projectile
{
    public List<AbilityController> warningCars = new List<AbilityController>();

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbilityController>() != null)
        {
            Debug.Log("BOOM");
            other.GetComponent<AbilityController>().TakeDamage();
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();
        rb.useGravity = true;
    }

    protected override void Destruct()
    {
        for (int i = 0; i < warningCars.Count; i++)
        {
            warningCars[i].IsMineWarning = false;
        }
        base.Destruct();
    }
}
