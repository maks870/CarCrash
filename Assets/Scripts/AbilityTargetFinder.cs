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

        if (other.GetComponentInChildren<ProjectileMine>() != null)
        {
            ablityController.IsMineWarning = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        ablityController.RemoveTarget(other.gameObject);

        if (other.GetComponentInChildren<ProjectileMine>() != null)
        {
            ablityController.IsMineWarning = false;
        }
    }
}
