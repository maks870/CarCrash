using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField] private List<AbilityObj> ablitities = new List<AbilityObj>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbilityController>() != null)
        {
            other.GetComponent<AbilityController>().AddAbility(GetRandomAbility());
            Destroy(gameObject);
        }
    }

    private AbilitySO GetRandomAbility()
    {
        int totalChance = 0;
        int stepChance = 0;

        for (int i = 0; i < ablitities.Count; i++)
        {
            totalChance += ablitities[i].DropChance;
        }

        int rand = Random.Range(0, totalChance);
        AbilitySO randAbility = ablitities[0].AbilitySO;
        for (int i = 0; i < ablitities.Count; i++)
        {
            stepChance += ablitities[i].DropChance;
            if (rand <= stepChance)
            {
                randAbility = ablitities[i].AbilitySO;
                break;
            }
        }
        return randAbility;
    }
}
