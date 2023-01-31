using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public abstract class CarControl : MonoBehaviour
{
    [SerializeField] protected AbilityController abilityController;
    [SerializeField] protected CarController carController;
    [SerializeField] protected ParticleSystem accelerationEffect;
    protected CarTrack carTrack;

    public CarTrack CarTrack { set => carTrack = value; }

    protected virtual void Update()
    {
        if (carController.IsAccelerated && !accelerationEffect.isPlaying)
            accelerationEffect.Play();

        if (!carController.IsAccelerated)
            accelerationEffect.Stop();

    }

    protected void ControlMove(float steering, float accel, float footbrake, float handbrake)
    {
        if (abilityController.IsDamaged)
        {
            handbrake = 1;
            footbrake = -1;
        }

        carController.Move(steering, accel, footbrake, handbrake);
    }
}
