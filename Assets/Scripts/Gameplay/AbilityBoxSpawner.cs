using System.Collections;
using UnityEngine;

public class AbilityBoxSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject prefab;
    private AbilityBox abilityBox;

    void Start()
    {
        abilityBox = Instantiate(prefab, transform).GetComponent<AbilityBox>();
        abilityBox.Spawner = this;
    }

    public void PickUpLootbox()
    {
        StartCoroutine(SpawnTimer(spawnDelay));
    }

    IEnumerator SpawnTimer(float time)
    {
        abilityBox.Hide();
        yield return new WaitForSeconds(time);
        abilityBox.Show();
    }
}
