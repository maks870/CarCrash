using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarTrack : MonoBehaviour
{
    private int currentPoint;
    private List<Transform> points;
    private BoxCollider boxCollider;
    private CarControl carControl;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        carControl.CarTrack = this;
        GameObject pointsObj = GameObject.Find("Points");

        foreach (Transform child in pointsObj.transform)
        {
            points.Add(child);
        }

        boxCollider.enabled = false;
    }

    private void Start()
    {
        transform.parent = null;
        NextPointMove();
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<CarControl>() == carControl)
            NextPointMove();
    }

    private void NextPointMove()
    {
        transform.position = points[currentPoint].position;
        currentPoint++;
    }
}