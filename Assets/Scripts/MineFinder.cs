using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class MineFinder : MonoBehaviour
{
    [SerializeField] float avoidMineDistance = 5f;
    [SerializeField] CarAIControl carAIControl;
    [SerializeField] AbilityController abilityController;


    private List<GameObject> mines = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ProjectileMine>() != null)
        {
            mines.Add(other.gameObject);
            abilityController.IsMineWarning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ProjectileMine>() != null)
        {
            mines.Remove(other.gameObject);
            abilityController.IsMineWarning = false;
        }
    }

    private void Start()
    {
        carAIControl.AvoidMineDistance = avoidMineDistance;
    }

    void Update()
    {
        CheckCurrentMine();

        if (mines.Count != 0)
            carAIControl.MineTarget = mines[0];
    }

    private void CheckCurrentMine()
    {
        if (mines.Count == 0)
        {
            carAIControl.MineTarget = null;
            return;
        }

        if (mines[0] != null)
        {
            if (Vector3.Angle(carAIControl.transform.forward, mines[0].transform.position - carAIControl.transform.position) >= 90)
                mines.RemoveAt(0);
        }
        else
        {
            mines.RemoveAt(0);
        }
    }
}
