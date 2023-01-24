using System;
using UnityEngine;

public class ChkTrigger : MonoBehaviour
{
    //this script takes the count of the checkpoints and laps passed of each player in the race
    //and then calculates it as score with the distance from the last passed checkpoint
    //at the end, the information is processed in ChkManager script (distance, chks and laps passed)
    public static bool startDis;
    private int currentCheckpoint, nextChk;
    public int CarPosListNumber;

    [HideInInspector] public Transform lastCheckpoint;

    private void Start()
    {
        if (transform.parent.name.Length == 6)//one-digit number name has 6 characters (for example: AICar4)
        {
            CarPosListNumber = int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 1));//get the last character (4)
        }
        if (transform.parent.name.Length == 7)//two-digit numbers name has 7 characters (for example: AICar17)
        {
            CarPosListNumber = int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 2));//get the last 2 characters (17)
        }
        if (CarPosListNumber == 0)
        {
            gameObject.name = "CarPos" + CarPosListNumber;
        }
        else
        {
            if (transform.parent.name.Length == 6)//one-digit number name has 9 characters (for example: CarPosAI4)
            {
                gameObject.name = "CarPosAI0" + CarPosListNumber;
            }
            if (transform.parent.name.Length == 7)//one-digit number name has 9 characters (for example: CarPosAI4)
            {
                gameObject.name = "CarPosAI" + CarPosListNumber;
            }
        }
        nextChk = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "chk")
        {
            ChkManager.nDistP[CarPosListNumber] = 0;

            currentCheckpoint = other.GetComponent<Checkpoint>().CurrentChkPoint;
            if (nextChk == currentCheckpoint)
            {
                nextChk = other.GetComponent<Checkpoint>().NextChkPoint;
                lastCheckpoint = other.gameObject.transform;
                lastCheckpoint = other.gameObject.transform;
                ChkManager.nChk[CarPosListNumber] = currentCheckpoint;

                if (currentCheckpoint == 1)
                {
                    startDis = true;
                    ChkManager.nLapsP[CarPosListNumber] += 1;
                }
            }
        }
    }
}