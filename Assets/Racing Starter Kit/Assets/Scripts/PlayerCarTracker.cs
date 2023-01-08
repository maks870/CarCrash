using System.Collections;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class PlayerCarTracker : MonoBehaviour
{
    private GameObject point;
    private int CurrentPoint;

    private void Start()
    {
        CurrentPoint = 1;
        point = GameObject.Find("Point" + CurrentPoint);
        transform.position = point.transform.position;
        CurrentPoint += 1;
    }

    IEnumerator OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<CarUserControl>())
        {
            point = GameObject.Find("Point" + CurrentPoint);
            transform.position = point.transform.position;
            GetComponent<BoxCollider>().enabled = false;
            CurrentPoint += 1;
            if (GameObject.Find("Point" + CurrentPoint) == null)
            {
                CurrentPoint = 1;
            }
            yield return new WaitForSeconds(0.1f);
            GetComponent<BoxCollider>().enabled = true;
        }
    }
}
