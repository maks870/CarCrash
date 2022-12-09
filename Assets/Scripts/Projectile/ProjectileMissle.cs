using UnityEngine;

public class ProjectileMissle : Projectile
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float speed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AbilityController>() != null)
        {
            Debug.Log("BOOM");
            collision.gameObject.GetComponent<AbilityController>().TakeDamage();
        }

        if (target != null)
            target.GetComponent<AbilityController>().IsMissleWarning = false;

        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        
    }

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