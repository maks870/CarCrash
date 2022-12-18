using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LootBox : MonoBehaviour
{
    [SerializeField] private List<ÑollectibleSO> items = new List<ÑollectibleSO>();

    [Range(0, 100)] private int coinDropChance = 30;
    [Range(0, 100)] private int gemDropChance = 20;

    [SerializeField] private int minDropCoin = 5;
    [SerializeField] private int maxDropCoin = 40;

    [SerializeField] private int minDropGem = 1;
    [SerializeField] private int maxDropGem = 10;


    private void Start()
    {
    }

    public void GetReward(out int coinValue, out int gemValue, out ÑollectibleSO collectibleItem)
    {
        int rand = Random.Range(0, 100);
        List<string> collectedItems = YandexGame.savesData.collectedItems;
        List<ÑollectibleSO> tempItems = new List<ÑollectibleSO>();

        gemValue = 0;
        coinValue = 0;
        tempItems.AddRange(items);

        foreach (string itemName in collectedItems)
        {
            tempItems.RemoveAll(item => item.Sprite.name == itemName);
        }

        int randItemIndex = Random.Range(0, tempItems.Count);
        collectibleItem = tempItems[randItemIndex];

        if (rand <= gemDropChance)
            gemValue = Random.Range(minDropGem, maxDropGem);

        rand = Random.Range(0, 100);

        if (rand <= coinDropChance)
            coinValue = Random.Range(minDropCoin, maxDropCoin);
    }



}
