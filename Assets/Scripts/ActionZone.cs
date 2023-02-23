using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(Collider))]
public class ActionZone : MonoBehaviour
{
    [SerializeField] private float zoneEventDelay = 1;
    private float time;
    private bool isActive = false;
    private CarUserControl carUserControl;
    public UnityEvent StayZoneEvent = new UnityEvent();

    private void Awake()
    {
        StayZoneEvent.AddListener(() => SwitchCarControl(true));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarUserControl>() != null)
        {
            carUserControl = other.GetComponent<CarUserControl>();
            time = 0;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CarUserControl>() != null)
            time += Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CarUserControl>() != null)
        {
            carUserControl = null;
            isActive = false;
            time = 0;
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

    public void SwitchCarControl(bool isStopped)
    {
        carUserControl.IsStopped = isStopped;
    }
}
