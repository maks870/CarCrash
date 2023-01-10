using System.Collections.Generic;
using UnityEngine;
using YG;

[System.Serializable]
public struct MapAward
{
    public int gems;
    public int coins;
}

public class AwardPresenter : MonoBehaviour
{
    [SerializeField] private List<MapAward> awards = new List<MapAward>();
    [SerializeField] private CarModelSO car;
    public void GetAward()
    {
        int pos = ChkManager.posMax;

        if (pos > 4)
            OpenAwards(awards[0]);
        else
            OpenAwards(awards[pos]);
    }

    private void OpenAwards(MapAward award)
    {
        //if (≈сли это первый заезд и машина не равна нулю)
        YandexGame.savesData.playerWrapper.collectibles.Add(car.Name);

        EarningManager.AddCoin(award.coins);
        EarningManager.AddGem(award.gems);
        YandexGame.SaveProgress();
    }
}
