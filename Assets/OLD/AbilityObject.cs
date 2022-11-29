using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityObject : MonoBehaviour
{
    public Ability ability;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
            ability.Use(other.gameObject);
    }
}
