using UnityEngine;

public class ProjectileMissle : Projectile
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float differenceSpeed;
    [SerializeField] private float minimumSpeed;
    [SerializeField] private float noTargetSpeed = 15f;

    [SerializeField] private AbilityController launcher;
    private float maxSpeed;
    private Rigidbody rbTarget;

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

        Destruct();
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            if (rbTarget == null)
                rbTarget = target.transform.parent.GetComponent<Rigidbody>();

            maxSpeed = rbTarget.velocity.magnitude < minimumSpeed
                ? minimumSpeed
                : rbTarget.velocity.magnitude;

            maxSpeed += differenceSpeed;
        }
        else
        {
            maxSpeed = noTargetSpeed;
        }

        rb.velocity = transform.forward * maxSpeed;

        //if (rb.velocity.magnitude < maxSpeed)
        //    rb.AddForce(transform.forward * missleAccel, ForceMode.Force);

        if (target != null)
            RotateRocket();
    }

    private void RotateRocket()
    {
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
    }
    protected override void Destruct()
    {
        if (target != null)
            target.GetComponent<AbilityController>().IsMissleWarning = false;

        base.Destruct();
    }

}