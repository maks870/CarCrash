using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChkTrigger))]
public class TrackReturner : MonoBehaviour
{
    [SerializeField] private float timeToReturn = 1;
    [SerializeField] private float returnHeight = 1;
    private ChkTrigger chkTrigger;
    private bool onTrack = true;
    private bool justReturned;
    private float timeOffTrack = 0;
    private Rigidbody rbCar;

    public bool JustReturned { get => justReturned; set => justReturned = value; }

    private void Start()
    {
        chkTrigger = transform.GetComponent<ChkTrigger>();
        rbCar = transform.parent.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (rbCar.freezeRotation)
            rbCar.freezeRotation = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            onTrack = true;
            timeOffTrack = 0;
        }
        else
        {
            onTrack = false;
        }
    }

    private void Update()
    {
        CountTimeToReturn();
    }

    private void ReturnToTrack()
    {

        Quaternion carOffsetAngleY = Quaternion.Euler(0, 90, 0);
        Quaternion angleReturn = chkTrigger.lastCheckpoint.GetChild(0).rotation * carOffsetAngleY;

        rbCar.velocity = Vector3.zero;

        transform.parent.position = new Vector3(chkTrigger.lastCheckpoint.position.x, 0, chkTrigger.lastCheckpoint.position.z) + Vector3.up * returnHeight;
        transform.parent.rotation = angleReturn;

        rbCar.freezeRotation = true;
        timeOffTrack = 0;
        justReturned = true;
    }

    private void CountTimeToReturn()
    {
        if (!onTrack)
            timeOffTrack += Time.deltaTime;
        else
            timeOffTrack = 0;

        if (timeOffTrack > timeToReturn)
            ReturnToTrack();
    }

}
