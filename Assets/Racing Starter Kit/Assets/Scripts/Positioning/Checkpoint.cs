using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
//this script is attached to all the checkpoints in the race and measures the car position to the checkpoint
public class Checkpoint : MonoBehaviour
{
    private float nDistanceX, nDistanceY, nDistanceZ;
    private List<double> nDistChk = new List<double>();
    private int currentChkPoint;
    private int nextChkPoint;

    public int CurrentChkPoint { get => currentChkPoint; set => currentChkPoint = value; }
    public int NextChkPoint { get => nextChkPoint; set => nextChkPoint = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (currentChkPoint == 1 && other.GetComponentInChildren<CarUserControl>())
        {
            if (!LapTimeManager.isTimerActive)
                LapTimeManager.isTimerActive = true;
        }
    }
    private void Start()
    {
        nDistChk.Clear();

        for (int i = 0; i <= ChkManager.nBots; i++)
        {
            nDistChk.Add(0);
            nDistChk.Add(0);
        }
    }

    void Update()
    {
        for (int i = 0; i <= ChkManager.nBots; i++)
        {
            if (ChkManager.nChk[i] == currentChkPoint)
            {
                //from here we can get the distance of the car position to the checkpoint position
                nDistanceZ = transform.position.z - ChkManager.CarPosList[i].transform.position.z;
                nDistanceY = transform.position.y - ChkManager.CarPosList[i].transform.position.y;
                nDistanceX = transform.position.x - ChkManager.CarPosList[i].transform.position.x;
                nDistChk[i] = Math.Sqrt(Math.Pow(nDistanceX, 2) + Math.Pow(nDistanceY, 2) + Math.Pow(nDistanceZ, 2));
                ChkManager.nDistP[i] = nDistChk[i];//and we send the information to the ChkManager.cs script (checkpoint manager)
                //checkpoint manager will compare the distance, checkpoints passed and laps done of each car of the race to obtain real time positioning
            }
        }
    }
}
