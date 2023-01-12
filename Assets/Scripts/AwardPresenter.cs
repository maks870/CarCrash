using System.Collections.Generic;
using UnityEngine;
using YG;

public class AwardPresenter : MonoBehaviour
{
    [SerializeField] private AwardUI awardUI;

    private CarModelSO carSO;
    private MapSO mapSO;

    public CarModelSO CarSO => carSO;
    public MapSO MapSO => mapSO;

    public void GetAward()
    {
        carSO = null;
        mapSO = null;
        MapAward mapAward;

        string mapName = YandexGame.savesData.playerWrapper.lastMap;
        MapSO map = SOLoader.LoadMapByName(mapName);

        int pos = YandexGame.savesData.playerWrapper.lastPlayedPlace;
        MapInfo mapInfo = YandexGame.savesData.playerWrapper.GetMapInfo(mapName);

        mapAward = pos > 4
            ? map.Awards[0]
            : map.Awards[pos];

        if (mapInfo.isPassed == false)
        {
            carSO = map.Car;
            mapSO = map.NextMap;
            MapInfo newMapInfo = new MapInfo(map.NextMap.Name);
            mapInfo.isPassed = true;

            YandexGame.savesData.playerWrapper.collectibles.Add(map.Car.Name);
            YandexGame.savesData.playerWrapper.maps.Add(newMapInfo);
            //Debug.Log(YandexGame.savesData.playerWrapper.GetMapInfo(mapName).isPassed);

            awardUI.ShowAwards(mapAward.coins, mapAward.gems, carSO, mapSO);
        }
        else
        {
            awardUI.ShowAwards(mapAward.coins, mapAward.gems, carSO);
        }

        OpenAwards(mapAward);
    }

    private void OpenAwards(MapAward award)
    {
        EarningManager.AddCoin(award.coins);
        EarningManager.AddGem(award.gems);
    }
}
