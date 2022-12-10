using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetFinder : MonoBehaviour
{
    [SerializeField] AbilityController abilityController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbilityController>() != null)
            abilityController.AddTarget(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<AbilityController>() != null)
            abilityController.RemoveTarget(other.gameObject);
    }
}
