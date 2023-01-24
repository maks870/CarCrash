using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInitializer : MonoBehaviour
{
    [SerializeField] private static List<Checkpoint> checkpoints = new List<Checkpoint>();

    public static List<Checkpoint> Checkpoints => checkpoints;

    void Start()
    {
        GameObject checkpointsObj = GameObject.Find("Checkpoints");
        for (int i = 0; i < checkpointsObj.transform.childCount; i++)
        {
            Checkpoint checkpoint = checkpointsObj.transform.GetChild(i).GetComponent<Checkpoint>();
            checkpoint.CurrentChkPoint = i + 1;

            if (i == checkpointsObj.transform.childCount - 1)
                checkpoint.NextChkPoint = 1;
            else
                checkpoint.NextChkPoint = i + 2;

            checkpoints.Add(checkpoint);
        }
    }
}
