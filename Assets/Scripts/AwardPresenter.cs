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
        int pos = YandexGame.savesData.playerWrapper.lastPlayedPlace;
        int mapIndex = YandexGame.savesData.playerWrapper.GetMapInfoIndex(mapName);
        MapSO map = SOLoader.LoadMapByName(mapName);

        mapAward = pos > 4
            ? map.Awards[0]
            : map.Awards[pos];

        if (YandexGame.savesData.playerWrapper.maps[mapIndex].isPassed == false)
        {
            carSO = map.Car;
            mapSO = map.NextMap;
            MapInfo newMapInfo = new MapInfo(map.NextMap.Name);

            YandexGame.savesData.playerWrapper.collectibles.Add(map.Car.Name);
            YandexGame.savesData.playerWrapper.maps.Add(newMapInfo);
            YandexGame.savesData.playerWrapper.maps[mapIndex].isPassed = true;

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
