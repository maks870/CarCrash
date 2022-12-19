using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<LootBox> lootboxes = new List<LootBox>();
    [SerializeField] private LootBoxAwardUI lootboxAwardUI;
    [SerializeField] private EarningManagerUI earningManagerUI;

    private void Start()
    {

    }

    public void OpenLootbox(int lootboxIndex)
    {
        LootBox lootbox = lootboxes[lootboxIndex];

        if (!EarningManager.SpendGem(lootbox.Cost))
        {
            earningManagerUI.ShowLackGemsWarning();
            return;
        }

        int coinValue;
        int gemValue;
        —ollectibleSO collectibleItem;

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
