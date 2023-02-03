using UnityEngine;

public class ChkTrigger : MonoBehaviour
{
    //this script takes the count of the checkpoints and laps passed of each player in the race
    //and then calculates it as score with the distance from the last passed checkpoint
    //at the end, the information is processed in ChkManager script (distance, chks and laps passed)
    private int currentCheckpoint;
    private int nextChk = 1;
    public static bool startDis;
    public int CarPosListNumber;

    [HideInInspector] public Transform lastCheckpoint;

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