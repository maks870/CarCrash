using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPresenter : MonoBehaviour
{
    [SerializeField] private int exchangeCoinCost = 50;
    [SerializeField] private int exchangeGemReward = 1;
    [SerializeField] private int smallAdReward = 1;
    [SerializeField] private int mediumAdReward = 2;
    [SerializeField] private int bigAdReward = 3;
    [SerializeField] private GemPresenterUI gemPresenterUI;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Exchange()
    {
        if (EarningManager.SpendCoin(exchangeCoinCost))
            EarningManager.AddGem(exchangeGemReward);
    }

    private void ShowSmallAd()
    {
        //���5������� ������� ����� ���� ������ ������
    }

    private void ShowMediumAd()
    {

    }

    private void ShowBigAd()
    {

    }


}
