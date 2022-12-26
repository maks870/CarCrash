using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapInfo
{
    public string mapName;
    public int highestPlace;
    public float fastestTime;

    public MapInfo(string mapName)
    {
        this.mapName = mapName;
        highestPlace = 0;
        fastestTime = 0;
    }
}
