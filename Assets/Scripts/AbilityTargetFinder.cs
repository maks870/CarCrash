using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetFinder : MonoBehaviour
{
    [SerializeField] AbilityController ablityController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<AbilityController>() != null)
        {
            ablityController.AddTarget(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ablityController.RemoveTarget(other.gameObject);
    }
}
