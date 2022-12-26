using System.Collections.Generic;

[System.Serializable]
public class PlayerWrapper
{
    public List<string> collectibles = new List<string>();
    public List<MapInfo> maps = new List<MapInfo>();
    public string currentCharacterItem;
    public string currentCarColorItem;
    public string currentCarModelItem;

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
