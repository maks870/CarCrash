using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(Collider))]
public class ActionZone : MonoBehaviour
{
    [SerializeField] private float zoneEventDelay = 1;
    private Collider userCollider;
    private float time;
    private bool isActive = false;
    public UnityEvent StayZoneEvent = new UnityEvent();


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarUserControl>() != null)
        {
            time = 0;
            userCollider = other;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (userCollider != null && userCollider == other)
            time += Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (userCollider != null && userCollider == other)
        {
            isActive = false;
            time = 0;
            userCollider = null;
        }
    }

    void Update()
    {
        if (time >= zoneEventDelay && !isActive)
        {
            isActive = true;
            StayZoneEvent.Invoke();
        }
    }
}
