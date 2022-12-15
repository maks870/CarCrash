using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField] private List<AbilityObj> ablitities = new List<AbilityObj>();
    private LootboxSpawner spawner;

    public LootboxSpawner Spawner { set => spawner = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AbilityController>() != null)
        {
            AbilityController carAbility = other.GetComponent<AbilityController>();
            carAbility.AddAbility(GetRandomAbility());
            spawner.PickUpLootbox();
        }
        if (other.GetComponent<ProjectileMissle>() != null)
        {
            spawner.PickUpLootbox();
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

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
