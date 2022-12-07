using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class AiAbilityDetected : MonoBehaviour
{
    [SerializeField] CarAIControl carAIControl; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LootBox>() != null) 
        {
            carAIControl.DetectAbility(other.transform);
        }
    }
}
