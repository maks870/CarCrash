using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LeaderboardWriter : MonoBehaviour
{
    public static void WriteLeaderboardPoints()
    {
        int currentPoints = 0;
        foreach (MapInfo mapInfo in YandexGame.savesData.playerWrapper.maps)
        {
            int curMapPoints = mapInfo.maxPoints - (mapInfo.fastestTime * 10) + mapInfo.fastestTimeMiliSec;
            curMapPoints = curMapPoints >= 0 ? curMapPoints : 0;
            currentPoints += curMapPoints;
        }

        YandexGame.NewLeaderboardScores("GlobalLeaderboard", currentPoints);
    }
}
