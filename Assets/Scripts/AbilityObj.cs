using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityObj : MonoBehaviour
{
    [SerializeField] private AbilitySO abilitySO;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NewCarController>() != null)
        {
            other.GetComponent<NewCarController>().AddAbility(abilitySO);
            Destroy(gameObject);
        }
    }
}
