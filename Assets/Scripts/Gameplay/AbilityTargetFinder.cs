using UnityEngine;

public class AbilityTargetFinder : MonoBehaviour
{
    [SerializeField] AbilityController abilityController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbilityController>() != null)
            abilityController.AddTarget(other.gameObject);

        if (other.GetComponent<ProjectileMine>() != null)
            abilityController.ForwardMines.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<AbilityController>() != null)
            abilityController.RemoveTarget(other.gameObject);

        if (other.GetComponent<ProjectileMine>() != null)
            abilityController.ForwardMines.Remove(other.gameObject);
    }
}
