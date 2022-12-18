using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<LootBox> lootboxes = new List<LootBox>();
    [SerializeField] private LootBoxAwardUI lootboxAwardUI;



    public void OpenLootbox(int lootboxIndex)
    {
        int coinValue;
        int gemValue;
        ÑollectibleSO collectibleItem;

        lootboxes[lootboxIndex].GetReward(out coinValue, out gemValue, out collectibleItem);

        if (coinValue != 0)
            YandexGame.savesData.coins += coinValue;

        if (gemValue != 0)
            YandexGame.savesData.gems += gemValue;

        YandexGame.savesData.collectedItems.Add(collectibleItem.Sprite.name);
        YandexGame.SaveProgress();

        lootboxAwardUI.ShowAwards(coinValue, gemValue, collectibleItem);
    }
}
