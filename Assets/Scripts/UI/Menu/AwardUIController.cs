using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwardUIController : MonoBehaviour
{

    [SerializeField] private GameObject coinAwardUI;
    [SerializeField] private GameObject gemAwardUI;
    [SerializeField] private GameObject collectbileAwardUI;
    [SerializeField] private GameObject mapAwardUI;
    [SerializeField] private GameObject AwardPresenterUI;

    private List<GameObject> awards = new List<GameObject>();
    private int currentAward = 0;

    public Action OnAwardsEnd;

    private void SwitchLootboxAward(bool isLootboxAward)
    {
        foreach (GameObject awardUI in awards)
        {
            awardUI.transform.parent.GetComponent<TargetPointer>().enabled = isLootboxAward;
        }
    }

    public void CloseAwards()
    {
        foreach (GameObject award in awards)
        {
            award.SetActive(false);
        }

        currentAward = 0;
        awards.Clear();
    }

    public void SwitchAward()
    {
        if (currentAward != 0)
            awards[currentAward - 1].SetActive(false);

        if (currentAward >= awards.Count)
        {
            CloseAwards();
            OnAwardsEnd?.Invoke();
            return;
        }

        awards[currentAward].SetActive(true);
        currentAward++;
    }

    public void ShowAwards(int coinValue, int gemValue, CollectibleSO collectibleItem)
    {
        awards.Clear();

        if (coinValue != 0)
        {
            coinAwardUI.GetComponentInChildren<Text>().text = coinValue.ToString();
            awards.Add(coinAwardUI);
        }

        if (gemValue != 0)
        {
            gemAwardUI.GetComponentInChildren<Text>().text = gemValue.ToString();
            awards.Add(gemAwardUI);
        }

        if (collectibleItem != null)
        {
            CharacterModelSO characterCollectible = (CharacterModelSO)collectibleItem;
            collectbileAwardUI.GetComponent<Image>().sprite = characterCollectible.Sprite;
            awards.Add(collectbileAwardUI);
        }

        currentAward = 0;
        SwitchLootboxAward(true);
        SwitchAward();
    }

    public void ShowAwards(int coinValue, int gemValue, CarModelSO carSO, MapSO mapSO)
    {
        awards.Clear();

        if (coinValue != 0)
        {
            coinAwardUI.GetComponentInChildren<Text>().text = coinValue.ToString();
            awards.Add(coinAwardUI);
        }

        if (gemValue != 0)
        {
            gemAwardUI.GetComponentInChildren<Text>().text = gemValue.ToString();
            awards.Add(gemAwardUI);
        }

        if (carSO != null)
        {
            collectbileAwardUI.GetComponent<Image>().sprite = carSO.Sprite;
            awards.Add(collectbileAwardUI);
        }

        if (mapSO != null)
        {
            mapAwardUI.GetComponent<Image>().sprite = mapSO.Sprite;
            awards.Add(mapAwardUI);
        }

        currentAward = 0;
        SwitchLootboxAward(false);
        SwitchAward();
    }
}
