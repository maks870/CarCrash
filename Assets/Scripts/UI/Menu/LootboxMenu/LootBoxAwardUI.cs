using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxAwardUI : MonoBehaviour
{

    [SerializeField] private GameObject coinAward;
    [SerializeField] private GameObject gemAward;
    [SerializeField] private GameObject collectbileAward;

    private List<GameObject> awards = new List<GameObject>();
    private int currentAward = 0;

    public void SwitchAward()
    {
        if (currentAward != 0)
            awards[currentAward - 1].SetActive(false);

        if (currentAward >= awards.Count)
            return;

        awards[currentAward].SetActive(true);
    }

    public void ShowAwards(int coinValue, int gemValue, ÑollectibleSO collectibleItem)
    {
        awards.Clear();

        if (coinValue != 0)
        {
            coinAward.GetComponent<Text>().text = coinValue.ToString();
            awards.Add(coinAward);
        }

        if (gemValue != 0)
        {
            gemAward.GetComponent<Text>().text = gemValue.ToString();
            awards.Add(gemAward);
        }

        collectbileAward.GetComponent<Image>().sprite = collectibleItem.Sprite;
        awards.Add(collectbileAward);
         
        currentAward = 0;

        SwitchAward();
    }
}
