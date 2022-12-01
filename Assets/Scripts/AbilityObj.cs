using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class AbilityObj : MonoBehaviour
{
    [SerializeField] private AbilitySO abilitySO;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbilityController>() != null)
        {
            other.GetComponent<AbilityController>().AddAbility(abilitySO);
            Destroy(gameObject);
        }
    }
}
