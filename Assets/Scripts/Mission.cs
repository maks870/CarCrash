using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    [SerializeField] private StartingTraining startingTraining;
    [SerializeField] private ActionZone missionZone;
    public Transform MissionZoine => missionZone.transform;

    private void Start()
    {
        startingTraining = GetComponent<StartingTraining>();
        missionZone = GetComponentInChildren<ActionZone>();
        missionZone.StayZoneEvent.AddListener(startingTraining.StartTraining);
    }
}