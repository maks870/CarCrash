using UnityEngine;

public class ProjectileMissle : Projectile
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float speed;
    [SerializeField] private AbilityController launcher;

    public AbilityController Launcher { get => launcher; set => launcher = value; }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Detected"))
            return;

        if (other.GetComponent<AbilityController>() != null)
        {
            if (other.GetComponent<AbilityController>() == launcher)
                return;
            else
                other.GetComponent<AbilityController>().TakeDamage();
        }

        if (target != null)
            target.GetComponent<AbilityController>().IsMissleWarning = false;

        Destruct();
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