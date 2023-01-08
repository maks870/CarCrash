using System.Collections.Generic;
using UnityEngine;

public class CarTrack : MonoBehaviour
{
    [SerializeField] private CarControl carControl;
    private int currentPoint;
    private List<Transform> points = new List<Transform>();
    private BoxCollider boxCollider;

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
        if (currentPoint >= points.Count)
            currentPoint = 0;

        transform.position = points[currentPoint].position;
        currentPoint++;
    }
}