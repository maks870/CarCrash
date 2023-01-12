using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<LootBox> lootboxes = new List<LootBox>();
    [SerializeField] private AwardUI lootboxAwardUI;
    [SerializeField] private EarningManagerUI earningManagerUI;

    private void Start()
    {

    }

    public void OpenLootbox(int lootboxIndex)
    {
        if (YandexGame.savesData.lootboxes == 0)
            return;

        LootBox lootbox = lootboxes[lootboxIndex];

        if (!EarningManager.SpendGem(lootbox.Cost))
            return;

        int coinValue;
        int gemValue;
        CollectibleSO collectibleItem;

        lootboxes[lootboxIndex].GetReward(out coinValue, out gemValue, out collectibleItem);

        if (coinValue != 0)
            YandexGame.savesData.coins += coinValue;

        if (gemValue != 0)
            YandexGame.savesData.gems += gemValue;

        YandexGame.savesData.playerWrapper.collectibles.Add(collectibleItem.Name);
        YandexGame.SaveProgress();

        lootboxAwardUI.ShowAwards(coinValue, gemValue, collectibleItem);
    }
}
