using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class AwardPresenter : MonoBehaviour
{
    [SerializeField] private AwardUIController awardUI;

    private CarModelSO carSO;
    private MapSO mapSO;
    public CarModelSO CarSO => carSO;
    public MapSO MapSO => mapSO;

    private void OpenEarnings(MapAward award)
    {
        EarningManager.AddCoin(award.coins);
        EarningManager.AddGem(award.gems);
    }

    public void GetAward()
    {
        int pos;
        MapAward mapAward = new MapAward();
        List<CollectibleSO> awardCollectibles = new List<CollectibleSO>();

        string mapName = YandexGame.savesData.playerWrapper.lastMap;
        int mapIndex = YandexGame.savesData.playerWrapper.GetMapInfoIndex(mapName);
        List<MapSO> maps = SOLoader.instance.GetSOList<MapSO>();
        MapSO map = maps.Find(item => item.Name == mapName);

        carSO = null;
        MapInfo mapInfo = YandexGame.savesData.playerWrapper.maps[mapIndex];

        for (int i = 0; i < YandexGame.savesData.playerWrapper.lastMapPlaces.Count; i++)
        {
            pos = YandexGame.savesData.playerWrapper.lastMapPlaces[i];
            mapAward = pos > 3
                ? mapAward.AddAward(map.Awards[0])
                : mapAward.AddAward(map.Awards[pos]);
        }

        if (mapInfo.isPassed == false)
        {
            if (mapInfo.highestPlace < 4 && mapInfo.highestPlace != 0)
            {
                if (map.Car != null)
                {
                    carSO = map.Car;
                    YandexGame.savesData.playerWrapper.collectibles.Add(map.Car.Name);
                    YandexGame.savesData.playerWrapper.newCollectibles.Add(map.Car.Name);
                    awardCollectibles.Add(map.Car);
                }
                YandexGame.savesData.currentMission += 1;
                YandexGame.savesData.playerWrapper.newMission = true;
                mapInfo.isPassed = true;
            }
        }

        awardUI.ShowAwards(mapAward.coins, mapAward.gems, carSO, null);
        OpenEarnings(mapAward);

        YandexGame.savesData.playerWrapper.lastMap = "";
        YandexGame.savesData.playerWrapper.lastMapPlaces.Clear();
    }
}
