using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootboxSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject prefab;
    private GameObject lootbox;

    void Start()
    {
        lootbox = Instantiate(prefab, transform);
        lootbox.GetComponent<LootBox>().Spawner = this;
    }

    public void PickUpLootbox()
    {
        StartCoroutine(SpawnTimer(spawnDelay));
    }

    IEnumerator SpawnTimer(float time)
    {
        lootbox.SetActive(false);
        yield return new WaitForSeconds(time);
        lootbox.SetActive(true);
    }
}
