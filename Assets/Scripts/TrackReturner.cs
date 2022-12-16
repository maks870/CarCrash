using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackReturner : MonoBehaviour
{
    [SerializeField] private float timeToReturn = 1;
    [SerializeField] private float returnHeight = 1;
    [SerializeField] private ChkTrigger chkTrigger;
    private float timeOffTrack = 0;
    private bool onTrack = true;

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
        transform.parent.transform.position = chkTrigger.lastCheckpoint.position + Vector3.up * returnHeight;
        timeOffTrack = 0;
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
