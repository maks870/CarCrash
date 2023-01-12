using System.Collections.Generic;

[System.Serializable]
public class PlayerWrapper
{
    public int lastPlayedPlace;
    public string lastMap;
    public string currentCharacterItem;
    public string currentCarColorItem;
    public string currentCarModelItem;
    public List<string> collectibles = new List<string>();
    public List<MapInfo> maps = new List<MapInfo>();

    public MapInfo GetMapInfo(string mapName)
    {
        MapInfo map = maps[0];
        for (int i = 0; i < maps.Count; i++)
        {
            if (maps[i].mapName == mapName)
                map = maps[i];
        }

        return map;
    }

    public float GetFastestTime(string mapName)
    {
        float fastestTime = maps[0].fastestTime;

        for (int i = 0; i < maps.Count; i++)
        {
            if (maps[i].mapName == mapName)
                fastestTime = maps[i].fastestTime;
        }

        return fastestTime;
    }

    public int GetHighestPlace(string mapName)
    {
        int highestPlace = maps[0].highestPlace;

        for (int i = 0; i < maps.Count; i++)
        {
            if (maps[i].mapName == mapName)
                highestPlace = maps[i].highestPlace;
        }

        return highestPlace;
    }


}
