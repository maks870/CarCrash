using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBox : MonoBehaviour
{
    [SerializeField] private List<AbilityObj> ablitities = new List<AbilityObj>();
    [SerializeField] private MeshRenderer meshRenderer;
    private AudioSource audioSource;
    private BoxCollider boxCollider;
    private AbilityBoxSpawner spawner;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    public AbilityBoxSpawner Spawner { set => spawner = value; }

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
            AbilityController carAbility = other.GetComponent<ProjectileMissle>().Launcher;
            carAbility.AddAbility(GetRandomAbility());
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
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
    }

    public void Hide()
    {
        boxCollider.enabled = false;
        meshRenderer.enabled = false;
        audioSource.Play();
        // gameObject.SetActive(false);
    }
}
