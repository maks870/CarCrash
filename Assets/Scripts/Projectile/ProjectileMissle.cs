using UnityEngine;

public class ProjectileMissle : Projectile
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        if (target != null)
            RotateRocket();
    }

    private void RotateRocket()
    {
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
    }
}