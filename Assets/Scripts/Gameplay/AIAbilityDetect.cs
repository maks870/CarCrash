using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class AIAbilityDetect : MonoBehaviour
{
    [SerializeField] CarAIControl carAIControl; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbilityBox>() != null) 
        {
            carAIControl.DetectAbility(other.transform);
        }
    }
}
