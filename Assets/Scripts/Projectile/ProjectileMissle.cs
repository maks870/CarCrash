using UnityEngine;

public class ProjectileMissle : Projectile
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float maxTimePrediction;
    [SerializeField] private float speed;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        RotateRocket();
    }

    private void RotateRocket()
    {
        var rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
    }
}