using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LootBox : MonoBehaviour, ITrainingWaiter
{
    [Range(0, 100)] private int coinDropChance = 30;
    [Range(0, 100)] private int gemDropChance = 20;

    [SerializeField] private int minDropCoin = 5;
    [SerializeField] private int maxDropCoin = 40;

    [SerializeField] private int minDropGem = 1;
    [SerializeField] private int maxDropGem = 10;

    [SerializeField] private GameObject roomUI;
    private Animator animator;
    private List<CollectibleSO> items = new List<CollectibleSO>();

    public Action ActionEndOpen;
    public Action ActionEndClose;



    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SubscribeWaitAction(Action endWaitAction)
    {
        ActionEndClose += endWaitAction;
    }

    public void UnsubscribeWaitAction(Action endWaitAction)
    {
        ActionEndClose -= endWaitAction;
    }

    public void CollectibledLoadSubscribe()
    {
        SOLoader.instance.EndLoadingEvent += () =>
        {
            items.AddRange(SOLoader.instance.GetSOList<CharacterModelSO>());
        };
    }

    public void GetReward(out int coinValue, out int gemValue, out CollectibleSO collectibleItem)
    {
        int rand = UnityEngine.Random.Range(0, 100);
        List<string> collectedItems = YandexGame.savesData.playerWrapper.collectibles;
        List<CollectibleSO> tempItems = new List<CollectibleSO>();


        gemValue = 0;
        coinValue = 0;
        tempItems.AddRange(items);

        foreach (string itemName in collectedItems)
        {
            tempItems.RemoveAll(item => item.Name == itemName);
        }

        if (tempItems.Count != 0)
        {
            int randItemIndex = UnityEngine.Random.Range(0, tempItems.Count);
            collectibleItem = tempItems[randItemIndex];
        }
        else
        {
            collectibleItem = null;
        }

        if (rand <= gemDropChance)
            gemValue = UnityEngine.Random.Range(minDropGem, maxDropGem);

        if (collectibleItem != null)
        {
            rand = UnityEngine.Random.Range(0, 100);

            if (rand <= coinDropChance)
                coinValue = UnityEngine.Random.Range(minDropCoin, maxDropCoin);
        }
        else
        {
            coinValue = UnityEngine.Random.Range(minDropCoin * 3, maxDropCoin * 3);
        }
    }

    public void Open()
    {
        animator.SetTrigger("Open");
    }

    public void Close()
    {
        animator.SetTrigger("Close");
    }

    public void EndOpenAnimation()
    {
        ActionEndOpen.Invoke();
    }

    public void EndCloseAnimation()
    {
        ActionEndClose.Invoke();
        roomUI.SetActive(true);
    }
}
