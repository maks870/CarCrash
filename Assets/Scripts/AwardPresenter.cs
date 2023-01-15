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
        int pos;
        MapAward mapAward = new MapAward();

        string mapName = YandexGame.savesData.playerWrapper.lastMap;
        int mapIndex = YandexGame.savesData.playerWrapper.GetMapInfoIndex(mapName);
        MapSO map = SOLoader.LoadMapByName(mapName);


        for (int i = 0; i < YandexGame.savesData.playerWrapper.lastMapPlaces.Count; i++)
        {
            pos = YandexGame.savesData.playerWrapper.lastMapPlaces[i];

            mapAward = pos > 3
                ? mapAward.AddAward(map.Awards[0])
                : mapAward.AddAward(map.Awards[pos]);
        }

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
