using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootboxSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject prefab;
    private LootBox lootbox;

    void Start()
    {
        lootbox = Instantiate(prefab, transform).GetComponent<LootBox>();
        lootbox.Spawner = this;
    }

    public void PickUpLootbox()
    {
        StartCoroutine(SpawnTimer(spawnDelay));
    }

    IEnumerator SpawnTimer(float time)
    {
        lootbox.Hide();
        yield return new WaitForSeconds(time);
        lootbox.Show();
    }
}
