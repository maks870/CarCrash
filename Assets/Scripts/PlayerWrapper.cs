using System.Collections.Generic;

[System.Serializable]
public class PlayerWrapper
{
    public string lastMap;
    public string currentCharacterItem;
    public string currentCarColorItem;
    public string currentCarModelItem;
    public List<int> lastMapPlaces = new List<int>();
    public List<string> collectibles = new List<string>();
    public List<MapInfo> maps = new List<MapInfo>();

    public int GetMapInfoIndex(string mapName)
    {
        int index = 0;

        for (int i = 0; i < maps.Count; i++)
        {
            if (maps[i].mapName == mapName)
            {
                index = i;
                return index;
            }
        }

        return index;
    }

    public float GetFastestTime(string mapName)
    {
        float fastestTime = maps[0].fastestTime;

        for (int i = 0; i < maps.Count; i++)
        {
            if (maps[i].mapName == mapName)
            {
                fastestTime = maps[i].fastestTime;
                return fastestTime;
            }
        }

        return fastestTime;
    }

    public int GetHighestPlace(string mapName)
    {
        int highestPlace = maps[0].highestPlace;

        for (int i = 0; i < maps.Count; i++)
        {
            if (maps[i].mapName == mapName)
            {
                highestPlace = maps[i].highestPlace;
                return highestPlace;
            }

        }

        return highestPlace;
    }


}
