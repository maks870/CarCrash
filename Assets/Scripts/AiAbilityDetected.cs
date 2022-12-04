using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class AiAbilityDetected : MonoBehaviour
{
    [SerializeField] CarAIControl carAIControl; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbilityObj>() != null) 
        {
            carAIControl.DetectAbility(other.transform);
        }
    }
}
